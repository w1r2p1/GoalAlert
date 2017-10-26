using DataModel;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace GoalAlert
{
    public class AppUtil
    {
        //http://host_ipAddress/feed.html?f=xml&type=matches&contest=&t1=&t2=&s=0&l=30&events=1&open=
        private static string FeedURI = ConfigurationManager.AppSettings["FeedURI"];
        private static string FeedParam = WebUtility.HtmlDecode(ConfigurationManager.AppSettings["FeedParam"]);
        private static string FeedParam1 = WebUtility.HtmlDecode(ConfigurationManager.AppSettings["FeedParam1"]);
        public static string LicenseKey = ConfigurationManager.AppSettings["LicenseKey"];
        private static int DaysAhead = Convert.ToInt32(ConfigurationManager.AppSettings["DaysAhead"]);
        public static int RequestSecondInterval = Convert.ToInt32(ConfigurationManager.AppSettings["RequestSecondInterval"]);
        public static int NoFeedSecondInterval = Convert.ToInt32(ConfigurationManager.AppSettings["NoFeedSecondInterval"]);
        public static int NoLeagueSecondInterval = Convert.ToInt32(ConfigurationManager.AppSettings["NoLeagueSecondInterval"]);
        public static int GMTValue = Convert.ToInt32(ConfigurationManager.AppSettings["GMTValue"]);//
        public static int MinutesBeforeKickoff = Convert.ToInt32(ConfigurationManager.AppSettings["MinutesBeforeKickoff"]);
        public static int MinutesAfterFinish = Convert.ToInt32(ConfigurationManager.AppSettings["MinutesAfterFinish"]);
        public static int KeepAliveMinute = Convert.ToInt32(ConfigurationManager.AppSettings["KeepAliveMinute"]);
        //protected static string contest;
       // protected static string param;
        public static int LastActiveHour
        {
            get { return Convert.ToInt32(ConfigurationManager.AppSettings["LastActiveHour"]); }
            set { LastActiveHour = value; }
        }
        private static string FileLog = ConfigurationManager.AppSettings["FileLog"] + "\\";
        public static TimeSpan PastHourSpan
        {
            get { return new TimeSpan(LastActiveHour, 0, 0); }
            set { PastHourSpan = value; }
        }
        private static DateTime RequestNow
        {
            get { return DateTime.Now.Subtract(PastHourSpan); }
            set { RequestNow = value; }
        }

        public static int EnabledLeague
        {
            get { return new GoalAlertEntities().Leagues.Where(f => f.IsEnabled == 1).ToList().Count(); }
            set { EnabledLeague = value; }
        }

        public static List<League> EnabledLeagues
        {
            get { return new GoalAlertEntities().Leagues.Where(f => f.IsEnabled == 1).ToList(); }
            set { EnabledLeagues = value; }
        }

        public class FeedTimer : System.Timers.Timer
        {
            private string _leagueName = "";
            public string LeagueName
            {
                get { return _leagueName; }
                set { _leagueName = value; }
            }
        }


        public AppUtil()
        {
            ConfigurationManager.RefreshSection("appSettings");
        }
        public static double ConvertDateTimeToUNIX_Timestamp(DateTime value)
        {
            TimeSpan span = (value - new DateTime(1970, 1, 1, 0, 0, 0, 0).ToLocalTime());

            return (double)span.TotalSeconds;
        }
        public static string ConvertUNIXToDateTime(double ut)
        {
            double timestamp = ut;

            System.DateTime dateTime = new System.DateTime(1970, 1, 1, 0, 0, 0, 0);

            dateTime = dateTime.AddSeconds(timestamp);

            string printDate = dateTime.ToShortDateString() + " " + dateTime.ToShortTimeString();

            return printDate;
        }

        public static string RequestUrl(string[] LeagueName)
        {
            string contest=string.Empty, param=string.Empty;
            if (LeagueName == null && LeagueName.Length < 1)
                return "";
            else
                //string league = LeagueName
                //for (int i = 0; i < LeagueName.Length; i++)
                //{
                //    string league = LeagueName[i];
                //    contest += "contest[]=" + league.ToString() + "&";
                //}
                foreach (var name in LeagueName)
                {
                    contest += string.Concat("c[]=", name.ToString(), WebUtility.HtmlDecode("&amp;"));
                }
                string Params = string.Format(FeedParam1, ConvertDateTimeToUNIX_Timestamp(RequestNow).ToString(), ConvertDateTimeToUNIX_Timestamp(RequestNow.AddDays(DaysAhead)), LicenseKey);
                param = string.Concat(FeedURI, FeedParam, contest, Params);
                return param;
        }

        public static void LogFileWrite(string message)
        {
            FileStream fileStream = null;
            StreamWriter streamWriter = null;
            try
            {
                string text = AppDomain.CurrentDomain.BaseDirectory + FileLog;
                text = text + "AppLog-" + DateTime.Today.ToString("yyyyMMdd") + ".txt";
                string str = DateTime.Now.ToShortDateString().ToString() + " " + DateTime.Now.ToLongTimeString().ToString() + " ==> ";
                if (!text.Equals(""))
                {
                    FileInfo fileInfo = new FileInfo(text);
                    DirectoryInfo directoryInfo = new DirectoryInfo(fileInfo.DirectoryName);
                    if (!directoryInfo.Exists)
                    {
                        directoryInfo.Create();
                    }
                    if (!fileInfo.Exists)
                    {
                        fileStream = fileInfo.Create();
                    }
                    else
                    {
                        fileStream = new FileStream(text, FileMode.Append);
                    }
                    streamWriter = new StreamWriter(fileStream);
                    streamWriter.WriteLine(str + message);
                }
            }
            finally
            {
                if (streamWriter != null)
                {
                    streamWriter.Close();
                }
                if (fileStream != null)
                {
                    fileStream.Close();
                }
            }
        }
        public static void LogDBWrite(string message, string Mode)
        {
            //SyncOrderEntities db = new SyncOrderEntities();
            //AppLog appLog = new AppLog();
            //try
            //{
            //    appLog.Details = message; appLog.Header = Mode; appLog.Timestamped = DateTime.Now;
            //    db.AppLogs.Add(appLog);

            //    db.SaveChanges();
            //}
            //catch (Exception ex)
            //{
            //    LogFileWrite(ex.ToString());
            //    LogFileWrite(message);
            //}
        }


    }
}
