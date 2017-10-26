using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Linq;
using System.ServiceProcess;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using DataModel;

namespace GoalAlert
{
    partial class FixtureGetter : ServiceBase
    {
        private static string license = string.Empty;
        private static string context = string.Empty;
        private static string team1 = string.Empty;
        private static string team2 = string.Empty;
        private static string uri = string.Empty;
        private static string score = string.Empty;
        private static string type = string.Empty;
        public FixtureGetter()
        {
            InitializeComponent();
        }
        protected static void PullFixtures()
        {
            uri = "http://pi.xmlscores.com/feed.html?f=xml&type=matches&contest=eng_pl&t1=1412509500&t2=1414237500&s=1&l=7&events=1&open=b4d88044654d82c4eccf19f8ae9c024f";
            XDocument xDocument = XDocument.Load(uri);
            using(GoalAlertEntities db = new GoalAlertEntities())
            {
                Fixture fx = new Fixture();
                var enumerable =
                from tra in xDocument.Descendants("matches-list")
                where tra.Element("match").Attribute("status").ToString() == "not_started"
                select new
                {
                    matchid=tra.Attribute("id").Value,
                    host = tra.Element("hosts").Value,
                    guest=tra.Element("guest").Value,
                    status=tra.Element("match").Attribute("status").Value,
                    unixkickoff = ConvertDateTimeToUNIX_Timestamp(DateTime.Parse(tra.Element("match").Attribute("time-start").Value)),
                    leagueid=1
                    //Text = customClass.removeIllegal(tra.Element("text").Value),
                    //From = tra.Element("groupFrom").Value,
                    //To = tra.Element("groupTo").Value,
                    //Status = tra.Element("roadStatus").Value,
                    //Cause = tra.Element("causeBy").Value,
                    //DateCreated = DateTime.ParseExact(tra.Element("created_at").Value, "ddd MMM dd HH:mm:ss zzz yyyy", CultureInfo.InvariantCulture),
                    //TrafficId = int.Parse(tra.Element("id").Value),
                    //TimeStamped = DateTime.Now,
                    //Truncated = tra.Element("truncated").Valu

                };
                foreach(var fixtures in enumerable)
                {
                    fx.MatchID = fixtures.matchid;
                    fx.Host = fixtures.host;
                    fx.Guest = fixtures.guest;
                    fx.LeaugeID = fixtures.leagueid;
                    fx.Status = fixtures.status;
                    fx.Unix_KickOff = fixtures.unixkickoff.ToString();
                    fx.IsDailySummSent = false;
                    fx.IsMatchSummSent = false;
                    fx.IsPreKickOffSent = false;
                    db.Fixtures.Add(fx);
                    db.SaveChanges();
                } 
            }

        }

        protected override void OnStart(string[] args)
        {
            // TODO: Add code here to start your service.
        }

        protected override void OnStop()
        {
            // TODO: Add code here to perform any tear-down necessary to stop your service.
        }

        private static double ConvertDateTimeToUNIX_Timestamp(DateTime value)
        {
            TimeSpan span = (value - new DateTime(1970, 1, 1, 0, 0, 0, 0).ToLocalTime());

            return (double)span.TotalSeconds;
        }

        private static string ConvertUNIXToDateTime(double ut)
        {
            double timestamp = ut;

            System.DateTime dateTime = new System.DateTime(1970, 1, 1, 0, 0, 0, 0);

            dateTime = dateTime.AddSeconds(timestamp);

            string printDate = dateTime.ToShortDateString() + " " + dateTime.ToShortTimeString();

            return printDate;
        }
    }
}
