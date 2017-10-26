using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;
using System.Xml.Linq;
using DataModel;
using System.Xml;
using System.Xml.Serialization;
using System.IO;
using System.Timers;
using System.Data.Entity.Validation;
using System.Configuration;


namespace GoalAlert
{
    public class FixturePuller
    {
        static GoalAlertEntities wdb = new GoalAlertEntities(), db = new GoalAlertEntities();
        static int _allLeague = GetAllEnabledLeage();
        static string qry = "";
        static TimeSpan PastActiveMinutes = new TimeSpan(0, AppUtil.MinutesAfterFinish, 0);

        static int AllEnabledLeague
        {
            get { return _allLeague; }
            set { _allLeague = value; }
        }
        static int ReturnLeague = 0;
        static int GetAllEnabledLeage()
        {
            return (wdb.Leagues.Where(f => f.IsEnabled == 1).ToList() != null) ? wdb.Leagues.Where(f => f.IsEnabled == 1).ToList().Count() : 0;
        }
        public static void StartPull(ref int workingTimerInterval)
        {
            ConfigurationManager.RefreshSection("appSettings");
            int canUpdate = 0, insertSkip = 1;
            List<string> goalEvents = new List<string> { "og", "pen", "goal" }; string xmlDocInnerXml = "";
            DateTime feedTime = DateTime.Now;
            db = new GoalAlertEntities();
            try
            {
                if (AllEnabledLeague > 0)
                {
                    ReturnLeague = AllEnabledLeague;
                    foreach (League _league in wdb.Leagues.Where(f => f.IsEnabled == 1).ToList())
                    {
                        XmlDocument xmlDoc = new XmlDocument();
                        xmlDoc.Load(AppUtil.RequestUrl(_league.LeagueName));
                        Fixture fx = new Fixture();
                        FixtureEvent matchfx_event = new FixtureEvent();
                        XmlSerializer serializer = new XmlSerializer(typeof(LiveScoreFeed));
                        MemoryStream memStream = new MemoryStream(Encoding.UTF8.GetBytes(xmlDoc.InnerXml));
                        //if (xmlDoc != null) AppUtil.LogFileWrite(xmlDoc.InnerXml);
                        if (xmlDoc != null) xmlDocInnerXml = xmlDoc.InnerXml;
                        LiveScoreFeed resultingFeeds = (LiveScoreFeed)serializer.Deserialize(memStream);

                        if (resultingFeeds != null && resultingFeeds.matcheslist != null && resultingFeeds.matcheslist.totalcount > 0)
                        {
                            foreach (Match match in resultingFeeds.matcheslist.match)
                            {
                                fx = new Fixture();
                                fx = db.Fixtures.FirstOrDefault(f => f.MatchID.ToLower() == match.id.ToLower());

                                if (fx == null || fx.ID <= 0)
                                {
                                    if (match.status.ToLower() != "finished")
                                    {
                                        fx = new Fixture();
                                        fx.Guest = match.teams.guests;
                                        fx.Host = match.teams.hosts;
                                        fx.IsDailySummSent = 0;
                                        fx.IsMatchSummSent = 0;
                                        fx.IsPreKickOffSent = 0;
                                        fx.IsScoreAET = false;
                                        fx.LeaugeID = _league.LeagueID;
                                        fx.Local_KickOff = Convert.ToDateTime(AppUtil.ConvertUNIXToDateTime(match.timestampstarts)).AddHours(AppUtil.GMTValue);
                                        fx.MatchID = match.id;
                                        fx.Pre_Local_KickOff = fx.Local_KickOff.GetValueOrDefault().Subtract(new TimeSpan(0, AppUtil.MinutesBeforeKickoff, 0));
                                        fx.Score = match.score;
                                        fx.Status = match.status;
                                        fx.Unix_KickOff = match.timestampstarts.ToString();

                                        //if (match.status.ToLower() == "finished")
                                        //    fx.Local_Finish = Convert.ToDateTime(AppUtil.ConvertUNIXToDateTime(Convert.ToInt32(resultingFeeds.timestamp_created))).AddHours(AppUtil.GMTValue);
                                        //else
                                        //    fx.Local_Finish = fx.Local_Finish;

                                        db.Fixtures.Add(fx);
                                        db.SaveChanges();
                                        insertSkip = 0;
                                    }
                                    else
                                    {
                                        insertSkip = 1;
                                    }
                                }
                                else
                                {
                                    //fx.IsDailySummSent = false;
                                    //fx.IsMatchSummSent = false;
                                    //fx.IsPreKickOffSent = false;
                                    //fx.IsScoreAET = false;

                                    if (fx.Status == match.status & (fx.Status.ToLower() == "not_started" | fx.Status.ToLower() == "postponed") & fx.Unix_KickOff != match.timestampstarts.ToString()) //in case of change of start time
                                    {
                                        fx.Local_KickOff = Convert.ToDateTime(AppUtil.ConvertUNIXToDateTime(match.timestampstarts)).AddHours(AppUtil.GMTValue);
                                        fx.Pre_Local_KickOff = fx.Local_KickOff.GetValueOrDefault().Subtract(new TimeSpan(0, AppUtil.MinutesBeforeKickoff, 0));
                                        fx.Status = match.status;
                                        fx.Unix_KickOff = match.timestampstarts.ToString();
                                        db.SaveChanges();
                                    }
                                    if (fx.Status != match.status & fx.Unix_KickOff != match.timestampstarts.ToString()) //in case of postponement
                                    {
                                        fx.Local_KickOff = Convert.ToDateTime(AppUtil.ConvertUNIXToDateTime(match.timestampstarts)).AddHours(AppUtil.GMTValue);
                                        fx.Pre_Local_KickOff = fx.Local_KickOff.GetValueOrDefault().Subtract(new TimeSpan(0, AppUtil.MinutesBeforeKickoff, 0));
                                        fx.Status = match.status;
                                        fx.Unix_KickOff = match.timestampstarts.ToString();
                                        canUpdate = 1;
                                    }

                                    if (fx.Status.ToLower() != match.status.ToLower() & fx.Unix_KickOff == match.timestampstarts.ToString())
                                    {
                                        if (match.status.ToLower() == "finished" & fx.Status.ToLower() != "finished")
                                            fx.Local_Finish = Convert.ToDateTime(AppUtil.ConvertUNIXToDateTime(Convert.ToInt32(resultingFeeds.timestamp_created))).AddHours(AppUtil.GMTValue);
                                        fx.Status = match.status;
                                        canUpdate = 1;
                                    }
                                    if (fx.Score != match.score)
                                    {
                                        fx.Score = match.score; //XX - XX,
                                        canUpdate = 1;
                                    }
                                    if (canUpdate == 1) db.SaveChanges();
                                }

                                //goal|own_goal|penalty==red_card|yellow_card==ps==
                                //goal - goal
                                //pen - goal scored from penalty
                                //og - own goal
                                //yc - yellow card
                                //rc - red card
                                //ps - penalty shootout kick*

                                if (match.matchevents != null && match.matchevents.Length > 0)
                                    if (canUpdate == 1 | insertSkip == 0)
                                    {
                                        MatchEvent aetMatch = match.matchevents.Where(w => goalEvents.Contains(w.type) == true).OrderByDescending(m => m.minute).FirstOrDefault();
                                        if (aetMatch != null)
                                            if (fx.IsScoreAET.GetValueOrDefault() != true & Convert.ToInt32(match.matchevents.Where(w => goalEvents.Contains(w.type) == true).OrderByDescending(m => m.minute).FirstOrDefault().minute) > 90)
                                            {
                                                fx.IsScoreAET = true;
                                                db.SaveChanges();
                                            }

                                        foreach (MatchEvent fx_event in match.matchevents)
                                        {
                                            int thisEventID = (wdb.Notifications.FirstOrDefault(f => f.Name.ToLower() == fx_event.type) != null) ?
                                                    wdb.Notifications.FirstOrDefault(f => f.Name.ToLower() == fx_event.type).ID : 7;
                                            matchfx_event = new FixtureEvent();
                                            matchfx_event = db.FixtureEvents.FirstOrDefault(f => (f.MatchID == match.id & f.ScoreLine == fx_event.score & goalEvents.Contains(fx_event.type)) |
                                               (f.MatchID == match.id & f.NotificationID == thisEventID & f.Team == fx_event.team.Value & f.Player == fx_event.player & goalEvents.Contains(fx_event.type) == false)); //f.Minute == fx_event.minute &

                                            if (matchfx_event == null || matchfx_event.EventID <= 0)
                                            {
                                                matchfx_event = new FixtureEvent();
                                                matchfx_event.IsPushed = 0;
                                                matchfx_event.MatchID = fx.MatchID;
                                                matchfx_event.Minute = fx_event.minute;
                                                matchfx_event.NotificationID = thisEventID;
                                                if (matchfx_event.NotificationID == 7)
                                                {
                                                    AppUtil.LogFileWrite(fx_event.type);
                                                    AppUtil.LogFileWrite(xmlDoc.InnerXml);
                                                }
                                                matchfx_event.Player = fx_event.player;
                                                matchfx_event.ScoreLine = fx_event.score;
                                                matchfx_event.Team = fx_event.team.Value; //value="guests"/"hosts"
                                                matchfx_event.Timestamped = DateTime.Now;
                                                db.FixtureEvents.Add(matchfx_event);
                                                db.SaveChanges();

                                                //HttpContext.Current.Request.ServerVariables("LOGON_USER")
                                            }
                                            else //Change in event mostly Goal Scorer
                                            {
                                                if (matchfx_event.Notification.Name.ToLower() != fx_event.type && matchfx_event.Player.ToLower() != fx_event.player.ToLower() && matchfx_event.ScoreLine != fx_event.score)
                                                {
                                                    matchfx_event.NotificationID = wdb.Notifications.FirstOrDefault(f => f.Name.ToLower() == fx_event.type).ID;
                                                    matchfx_event.Player = fx_event.player;
                                                    //matchfx_event.Timestamped = DateTime.Now;
                                                    matchfx_event.ScoreLine = fx_event.score;
                                                    matchfx_event.Team = fx_event.team.Value; //value="guests"/"hosts"
                                                    db.SaveChanges();
                                                }

                                                else if (goalEvents.Contains(fx_event.type) & (matchfx_event.ScoreLine != fx_event.score | matchfx_event.Notification.Name.ToLower() != fx_event.type |
                                                    matchfx_event.Player.ToLower() != fx_event.player.ToLower() | matchfx_event.Minute != fx_event.minute))
                                                {
                                                    matchfx_event.NotificationID = wdb.Notifications.FirstOrDefault(f => f.Name.ToLower() == fx_event.type).ID;
                                                    matchfx_event.Player = fx_event.player;
                                                    //matchfx_event.Timestamped = DateTime.Now;
                                                    matchfx_event.ScoreLine = fx_event.score;
                                                    matchfx_event.Minute = fx_event.minute;
                                                    matchfx_event.Team = fx_event.team.Value; //value="guests"/"hosts"
                                                    db.SaveChanges();
                                                }

                                            }
                                        }

                                    }

                            }

                        }
                        else //there is no feed at all
                        {
                            ReturnLeague -= 1;
                        }

                        if (ReturnLeague == 0)
                        {
                            workingTimerInterval = AppUtil.NoFeedSecondInterval * 1000; //wait for 1 minute
                            AppUtil.LogFileWrite(string.Format("No Feed Return For All Enabled League! Live Feed Service Waiting for {0} minute......", (AppUtil.NoFeedSecondInterval / 60).ToString()));
                        }
                        else
                            workingTimerInterval = AppUtil.RequestSecondInterval * 1000;

                    }
                    // "http://pi.xmlscores.com/feed.html?f=xml&type=matches&contest=eng_pl&t1=1412509500&t2=1414237500&s=1&l=7&events=1&open=b4d88044654d82c4eccf19f8ae9c024f";

                    //string cmdText = "Insert into Fixture(MatchId,LeagueId,Host,Guest,Status,Unix_KickOff,IsPreKickOffSent,IsMatchSummSent,IsDailySummSent])VALUES(@matchid,@leagueid,@host,@guest,@status,@kickoff,@sent1,@sent2,@sent3)";
                }
                else
                {
                    workingTimerInterval = AppUtil.NoLeagueSecondInterval * 1000; //wait for 1 hour
                    AppUtil.LogFileWrite(string.Format("No League Enabled Yet! Live Feed Service Waiting for {0} minute......", (AppUtil.NoLeagueSecondInterval / 60).ToString()));
                }
                db = new GoalAlertEntities();
                qry ="delete from PastFixture where MatchID in (select MatchID from Fixture)";
                db.Database.ExecuteSqlCommand(qry);
                qry = "delete PastFixtureEvent where MatchID in (select MatchID from FixtureEvent)";
                db.Database.ExecuteSqlCommand(qry);
                foreach (Fixture finishedMatch in db.Fixtures.ToList())
                {
                    switch (finishedMatch.Status.ToLower()) //not_started, finished, active, postponed;
                    {
                        case "active":
                            //update/ or add events
                            if (feedTime.Subtract(finishedMatch.Local_KickOff.GetValueOrDefault()).TotalMinutes > new TimeSpan(AppUtil.LastActiveHour, 0, 0).TotalMinutes)
                            {
                                qry = @"INSERT INTO [dbo].[PastFixtureEvent] ([MatchID],[NotificationID],[Team],[Player],[Minute],[ScoreLine],[IsPushed],[Timestamped])
                                              SELECT [MatchID],[NotificationID],[Team],[Player],[Minute],[ScoreLine],[IsPushed],[Timestamped] FROM [dbo].[FixtureEvent] FE WHERE [MatchID] ={0}
                                              AND NOT EXISTS(SELECT ID FROM [dbo].[PastFixtureEvent] PE WHERE PE.[MatchID] =FE.[MatchID] AND PE.[NotificationID]=FE.[NotificationID] AND PE.[Team]=FE.[Team] 
                                              AND PE.[Player]=FE.[Player] AND PE.[Minute]=FE.[Minute] AND PE.[ScoreLine]=FE.[ScoreLine])";
                                db.Database.ExecuteSqlCommand(qry, finishedMatch.MatchID);

                                qry = @"INSERT INTO PastFixture ([MatchID],[LeaugeID],[Host],[Guest],[Unix_KickOff],[Status],[Local_KickOff],[Pre_Local_KickOff],[Local_Finish],[IsPreKickOffSent],[IsMatchSummSent],[IsDailySummSent],[Score],[IsScoreAET])
                                            SELECT [MatchID],[LeaugeID],[Host],[Guest],[Unix_KickOff],[Status],[Local_KickOff],[Pre_Local_KickOff],[Local_Finish],[IsPreKickOffSent],[IsMatchSummSent],[IsDailySummSent],[Score],[IsScoreAET] FROM [dbo].[Fixture]
                                            WHERE [MatchID] ={0} AND [Status] ={1} AND NOT EXISTS(SELECT ID FROM [dbo].[FixtureEvent] WHERE [MatchID] ={0}
											AND NOT EXISTS(SELECT ID FROM [dbo].[PastFixture] P WHERE P.[MatchID] ={0}))";
                                db.Database.ExecuteSqlCommand(qry, finishedMatch.MatchID, finishedMatch.Status);

                                qry = @"DELETE FROM [dbo].[FixtureEvent] WHERE [MatchID] ={0} AND EXISTS(SELECT ID FROM [dbo].[PastFixtureEvent] PE WHERE PE.[MatchID] =[MatchID] AND PE.[NotificationID]=[NotificationID] AND PE.[Team]=[Team] 
                                            AND PE.[Player]=[Player] AND PE.[Minute]=[Minute] AND PE.[ScoreLine]=[ScoreLine])";
                                db.Database.ExecuteSqlCommand(qry, finishedMatch.MatchID);

                                qry = @"DELETE FROM [dbo].[Fixture] WHERE [MatchID] ={0} AND [Status] ={1} AND EXISTS(SELECT ID FROM [dbo].[PastFixture] P WHERE p.[MatchID] ={0})";
                                db.Database.ExecuteSqlCommand(qry, finishedMatch.MatchID, finishedMatch.Status);

                                qry = @"DELETE FROM [dbo].[FixtureEvent] WHERE [MatchID] ={0}";
                                db.Database.ExecuteSqlCommand(qry, finishedMatch.MatchID);

                                qry = @"DELETE FROM [dbo].[Fixture] WHERE [MatchID] ={0} AND [Status] ={1} AND NOT EXISTS(SELECT ID FROM [dbo].[FixtureEvent] WHERE [MatchID] ={0})";
                                db.Database.ExecuteSqlCommand(qry, finishedMatch.MatchID, finishedMatch.Status);

                                //db.Database.ExecuteSqlCommand(qry, finishedMatch.MatchID, finishedMatch.Status);
                            }

                            break;
                        case "finished":
                            //update or add events
                            //prepare for archive
                            if (feedTime.Subtract(finishedMatch.Local_Finish.GetValueOrDefault()).TotalMinutes > PastActiveMinutes.TotalMinutes)
                            {
                                qry = @"INSERT INTO [dbo].[PastFixtureEvent] ([MatchID],[NotificationID],[Team],[Player],[Minute],[ScoreLine],[IsPushed],[Timestamped])
                                              SELECT [MatchID],[NotificationID],[Team],[Player],[Minute],[ScoreLine],[IsPushed],[Timestamped] FROM [dbo].[FixtureEvent] FE WHERE [IsPushed] >0 AND [MatchID] ={0}
                                              AND NOT EXISTS(SELECT ID FROM [dbo].[PastFixtureEvent] PE WHERE PE.[MatchID] =FE.[MatchID] AND PE.[NotificationID]=FE.[NotificationID] AND PE.[Team]=FE.[Team] 
                                              AND PE.[Player]=FE.[Player] AND PE.[Minute]=FE.[Minute] AND PE.[ScoreLine]=FE.[ScoreLine])";
                                db.Database.ExecuteSqlCommand(qry, finishedMatch.MatchID);

                                qry = @"INSERT INTO PastFixture ([MatchID],[LeaugeID],[Host],[Guest],[Unix_KickOff],[Status],[Local_KickOff],[Pre_Local_KickOff],[Local_Finish],[IsPreKickOffSent],[IsMatchSummSent],[IsDailySummSent],[Score],[IsScoreAET])
                                            SELECT [MatchID],[LeaugeID],[Host],[Guest],[Unix_KickOff],[Status],[Local_KickOff],[Pre_Local_KickOff],[Local_Finish],[IsPreKickOffSent],[IsMatchSummSent],[IsDailySummSent],[Score],[IsScoreAET] FROM [dbo].[Fixture]
                                            WHERE [MatchID] ={0} AND [Status] ={1} AND NOT EXISTS(SELECT ID FROM [dbo].[FixtureEvent] WHERE [IsPushed] =0 AND [MatchID] ={0}
											AND NOT EXISTS(SELECT ID FROM [dbo].[PastFixture] P WHERE P.[MatchID] ={0}))";
                                db.Database.ExecuteSqlCommand(qry, finishedMatch.MatchID, finishedMatch.Status);

                                qry = @"DELETE FROM [dbo].[FixtureEvent] WHERE [MatchID] ={0} AND EXISTS(SELECT ID FROM [dbo].[PastFixtureEvent] PE WHERE PE.[MatchID] =[MatchID] AND PE.[NotificationID]=[NotificationID] AND PE.[Team]=[Team] 
                                            AND PE.[Player]=[Player] AND PE.[Minute]=[Minute] AND PE.[ScoreLine]=[ScoreLine])";
                                db.Database.ExecuteSqlCommand(qry, finishedMatch.MatchID);

                                qry = @"DELETE FROM [dbo].[Fixture] WHERE [MatchID] ={0} AND [Status] ={1} AND EXISTS(SELECT ID FROM [dbo].[PastFixture] P WHERE p.[MatchID] ={0})";
                                db.Database.ExecuteSqlCommand(qry, finishedMatch.MatchID, finishedMatch.Status);

                                qry = @"DELETE FROM [dbo].[FixtureEvent] WHERE [IsPushed] >0 AND [MatchID] ={0}";
                                db.Database.ExecuteSqlCommand(qry, finishedMatch.MatchID);

                                qry = @"DELETE FROM [dbo].[Fixture] WHERE [MatchID] ={0} AND [Status] ={1} AND NOT EXISTS(SELECT ID FROM [dbo].[FixtureEvent] WHERE [MatchID] ={0})";
                                db.Database.ExecuteSqlCommand(qry, finishedMatch.MatchID, finishedMatch.Status);

                                //db.Database.ExecuteSqlCommand(qry, finishedMatch.MatchID, finishedMatch.Status);
                            }
                            db.SaveChanges();
                            break;
                        case "not_started":
                            //do nothing for now
                            if (feedTime.Subtract(finishedMatch.Local_KickOff.GetValueOrDefault()).TotalMinutes > new TimeSpan(AppUtil.LastActiveHour, 0, 0).TotalMinutes)
                            {
                                qry = @"INSERT INTO [dbo].[PastFixtureEvent] ([MatchID],[NotificationID],[Team],[Player],[Minute],[ScoreLine],[IsPushed],[Timestamped])
                                              SELECT [MatchID],[NotificationID],[Team],[Player],[Minute],[ScoreLine],[IsPushed],[Timestamped] FROM [dbo].[FixtureEvent] FE WHERE [MatchID] ={0}
                                              AND NOT EXISTS(SELECT ID FROM [dbo].[PastFixtureEvent] PE WHERE PE.[MatchID] =FE.[MatchID] AND PE.[NotificationID]=FE.[NotificationID] AND PE.[Team]=FE.[Team] 
                                              AND PE.[Player]=FE.[Player] AND PE.[Minute]=FE.[Minute] AND PE.[ScoreLine]=FE.[ScoreLine])";
                                db.Database.ExecuteSqlCommand(qry, finishedMatch.MatchID);

                                qry = @"INSERT INTO PastFixture ([MatchID],[LeaugeID],[Host],[Guest],[Unix_KickOff],[Status],[Local_KickOff],[Pre_Local_KickOff],[Local_Finish],[IsPreKickOffSent],[IsMatchSummSent],[IsDailySummSent],[Score],[IsScoreAET])
                                            SELECT [MatchID],[LeaugeID],[Host],[Guest],[Unix_KickOff],[Status],[Local_KickOff],[Pre_Local_KickOff],[Local_Finish],[IsPreKickOffSent],[IsMatchSummSent],[IsDailySummSent],[Score],[IsScoreAET] FROM [dbo].[Fixture]
                                            WHERE [MatchID] ={0} AND [Status] ={1} AND NOT EXISTS(SELECT ID FROM [dbo].[FixtureEvent] WHERE [MatchID] ={0}
											AND NOT EXISTS(SELECT ID FROM [dbo].[PastFixture] P WHERE P.[MatchID] ={0}))";
                                db.Database.ExecuteSqlCommand(qry, finishedMatch.MatchID, finishedMatch.Status);

                                qry = @"DELETE FROM [dbo].[FixtureEvent] WHERE [MatchID] ={0} AND EXISTS(SELECT ID FROM [dbo].[PastFixtureEvent] PE WHERE PE.[MatchID] =[MatchID] AND PE.[NotificationID]=[NotificationID] AND PE.[Team]=[Team] 
                                            AND PE.[Player]=[Player] AND PE.[Minute]=[Minute] AND PE.[ScoreLine]=[ScoreLine])";
                                db.Database.ExecuteSqlCommand(qry, finishedMatch.MatchID);

                                qry = @"DELETE FROM [dbo].[Fixture] WHERE [MatchID] ={0} AND [Status] ={1} AND EXISTS(SELECT ID FROM [dbo].[PastFixture] P WHERE p.[MatchID] ={0})";
                                db.Database.ExecuteSqlCommand(qry, finishedMatch.MatchID, finishedMatch.Status);

                                qry = @"DELETE FROM [dbo].[FixtureEvent] WHERE [MatchID] ={0}";
                                db.Database.ExecuteSqlCommand(qry, finishedMatch.MatchID);

                                qry = @"DELETE FROM [dbo].[Fixture] WHERE [MatchID] ={0} AND [Status] ={1} AND NOT EXISTS(SELECT ID FROM [dbo].[FixtureEvent] WHERE [MatchID] ={0})";
                                db.Database.ExecuteSqlCommand(qry, finishedMatch.MatchID, finishedMatch.Status);

                                //db.Database.ExecuteSqlCommand(qry, finishedMatch.MatchID, finishedMatch.Status);
                            }

                            break;
                        case "postponed":
                            //update or add events
                            if (feedTime.Subtract(finishedMatch.Local_KickOff.GetValueOrDefault()).TotalMinutes > new TimeSpan(AppUtil.LastActiveHour, 0, 0).TotalMinutes)
                            {
                                qry = @"INSERT INTO [dbo].[PastFixtureEvent] ([MatchID],[NotificationID],[Team],[Player],[Minute],[ScoreLine],[IsPushed],[Timestamped])
                                              SELECT [MatchID],[NotificationID],[Team],[Player],[Minute],[ScoreLine],[IsPushed],[Timestamped] FROM [dbo].[FixtureEvent] FE WHERE [MatchID] ={0}
                                              AND NOT EXISTS(SELECT ID FROM [dbo].[PastFixtureEvent] PE WHERE PE.[MatchID] =FE.[MatchID] AND PE.[NotificationID]=FE.[NotificationID] AND PE.[Team]=FE.[Team] 
                                              AND PE.[Player]=FE.[Player] AND PE.[Minute]=FE.[Minute] AND PE.[ScoreLine]=FE.[ScoreLine])";
                                db.Database.ExecuteSqlCommand(qry, finishedMatch.MatchID);

                                qry = @"INSERT INTO PastFixture ([MatchID],[LeaugeID],[Host],[Guest],[Unix_KickOff],[Status],[Local_KickOff],[Pre_Local_KickOff],[Local_Finish],[IsPreKickOffSent],[IsMatchSummSent],[IsDailySummSent],[Score],[IsScoreAET])
                                            SELECT [MatchID],[LeaugeID],[Host],[Guest],[Unix_KickOff],[Status],[Local_KickOff],[Pre_Local_KickOff],[Local_Finish],[IsPreKickOffSent],[IsMatchSummSent],[IsDailySummSent],[Score],[IsScoreAET] FROM [dbo].[Fixture]
                                            WHERE [MatchID] ={0} AND [Status] ={1} AND NOT EXISTS(SELECT ID FROM [dbo].[FixtureEvent] WHERE [MatchID] ={0}
											AND NOT EXISTS(SELECT ID FROM [dbo].[PastFixture] P WHERE P.[MatchID] ={0}))";
                                db.Database.ExecuteSqlCommand(qry, finishedMatch.MatchID, finishedMatch.Status);

                                qry = @"DELETE FROM [dbo].[FixtureEvent] WHERE [MatchID] ={0} AND EXISTS(SELECT ID FROM [dbo].[PastFixtureEvent] PE WHERE PE.[MatchID] =[MatchID] AND PE.[NotificationID]=[NotificationID] AND PE.[Team]=[Team] 
                                            AND PE.[Player]=[Player] AND PE.[Minute]=[Minute] AND PE.[ScoreLine]=[ScoreLine])";
                                db.Database.ExecuteSqlCommand(qry, finishedMatch.MatchID);

                                qry = @"DELETE FROM [dbo].[Fixture] WHERE [MatchID] ={0} AND [Status] ={1} AND EXISTS(SELECT ID FROM [dbo].[PastFixture] P WHERE p.[MatchID] ={0})";
                                db.Database.ExecuteSqlCommand(qry, finishedMatch.MatchID, finishedMatch.Status);

                                qry = @"DELETE FROM [dbo].[FixtureEvent] WHERE [MatchID] ={0}";
                                db.Database.ExecuteSqlCommand(qry, finishedMatch.MatchID);

                                qry = @"DELETE FROM [dbo].[Fixture] WHERE [MatchID] ={0} AND [Status] ={1} AND NOT EXISTS(SELECT ID FROM [dbo].[FixtureEvent] WHERE [MatchID] ={0})";
                                db.Database.ExecuteSqlCommand(qry, finishedMatch.MatchID, finishedMatch.Status);

                                //db.Database.ExecuteSqlCommand(qry, finishedMatch.MatchID, finishedMatch.Status);
                            }

                            break;
                        default:
                            break;
                    }
                }


            }
            catch (DbEntityValidationException exception)
            {
                foreach (DbValidationError validationError in exception.EntityValidationErrors.SelectMany(error => error.ValidationErrors))
                {
                    AppUtil.LogFileWrite(string.Format("{0} - {1}", validationError.PropertyName, validationError.ErrorMessage));
                }
                if (!string.IsNullOrEmpty(xmlDocInnerXml)) AppUtil.LogFileWrite(xmlDocInnerXml);
                workingTimerInterval = AppUtil.NoFeedSecondInterval * 1000;

            }
            catch (Exception rex)
            {
                AppUtil.LogFileWrite("Live Feed Service Run Error: " + rex.ToString());
                //AppUtil.LogFileWrite("Live Feed Service Run Error(StackTrace): " + rex.StackTrace);
                //if (rex.InnerException != null) AppUtil.LogFileWrite("Live Feed Service Run Error:(InnerException) " + rex.InnerException.ToString());
                if (!string.IsNullOrEmpty(xmlDocInnerXml)) AppUtil.LogFileWrite(xmlDocInnerXml);
                //Log detail to db
                workingTimerInterval = AppUtil.RequestSecondInterval * 1000;
            }
            finally
            {
                db.Dispose();
            }
        }


    }
}
