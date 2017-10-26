using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Linq;
using System.Collections;
using System.ServiceProcess;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using DataModel;
using System.Timers;
using System.Configuration;


namespace GoalAlert
{
    partial class LiveFeedManager : ServiceBase
    {
        Timer FeedPullerTimer = new Timer();
        List<AppUtil.FeedTimer> MatchManagerTimer = new List<AppUtil.FeedTimer>();

        Timer FeedKeepAlive = new Timer();
        int LeagueAvailble = AppUtil.EnabledLeague;
        public LiveFeedManager()
        {
            InitializeComponent();
        }


        protected override void OnStart(string[] args)
        {
            try
            {
                // TODO: Add code here to start your service.
                AppUtil.LogFileWrite("Live Feed Starting.....");
                InitFeedPuller();
                //InitMatchManagerTimer();
                InitKeepAlive();
                AppUtil.LogFileWrite("Live Feed Started.....");
            }
            catch (Exception ex)
            {
                AppUtil.LogFileWrite("Live Feed Service Start Error: " + ex.Message);
                //Log detail to db
            }
        }


        public void RunService(ref int workingTimerInterval)
        {
            //try
            //{
            //    // Cast the obj argument to clsTime.
            //    if (obj is Timer)
            //    {
            //        Timer jobTimer = (Timer)obj;
            //        int workingTimerInterval = 0;
            //        FixturePuller.StartPull(ref workingTimerInterval);
            //        if (workingTimerInterval > 0)
            //            FeedPullerTimer.Interval = workingTimerInterval;
            //    }
            //}
            //catch (Exception rex)
            //{
            //    AppUtil.LogFileWrite("Live Feed Service Run Error: " + rex.ToString());
            //   // AppUtil.LogFileWrite("Live Feed Service Run Error: " + rex.StackTrace);
            //   //if (rex.InnerException !=null) AppUtil.LogFileWrite("Live Feed Service Run Error: " + rex.InnerException.ToString());
            //    //Log detail to db
            //   FeedPullerTimer.Interval = AppUtil.NoFeedSecondInterval * 1000;
            //}

            //if (obj is Timer)
            //{
            //    Timer jobTimer = (Timer)obj;
            //    //FeedPullerTimer.AutoReset = true;
            //    int workingTimerInterval = 0;
            FixturePuller.StartPull(ref workingTimerInterval);

            //}

        }
        protected override void OnStop()
        {
            // TODO: Add code here to perform any tear-down necessary to stop your service.
            try
            {
                AppUtil.LogFileWrite("Live Feed Stopping.....");
                FeedPullerTimer.Dispose();
                AppUtil.LogFileWrite("Live Feed Stopped.....");
            }
            catch (Exception ex)
            {
                AppUtil.LogFileWrite("Live Feed Service Stop Error: " + ex.Message);
                //Log detail to db
            }

        }

        private void FeedPullerTimer_Elapsed(object sender, ElapsedEventArgs e)
        {
            //FeedPullerTimer.Dispose();
            ConfigurationManager.RefreshSection("appSettings"); 
            int workingTimerInterval = 0;
            FeedPullerTimer.Stop();//if (FeedPullerTimer.Enabled)
            RunService(ref workingTimerInterval);
            if (workingTimerInterval > 0)
                FeedPullerTimer.Interval = workingTimerInterval;
            FeedPullerTimer.Start();
        }

        private void FeedKeepAlive_Elapsed(object sender, ElapsedEventArgs e)
        {
            FeedKeepAlive.Stop();
            FeedKeepAlive.Interval = AppUtil.KeepAliveMinute * 1000;
            AppUtil.LogFileWrite(string.Format("...Keep Alive at {0}......With working time at {1}....Running since....{2}",
                DateTime.Now.ToString(), DateTime.Now.Subtract(AppUtil.PastHourSpan).ToString(), Process.GetCurrentProcess().StartTime.ToString()));
            WakeUpIfIdle();
            FeedKeepAlive.Start();
        }

        private void WakeUpIfIdle()
        {
            if (FeedPullerTimer.Interval == AppUtil.NoLeagueSecondInterval | FeedPullerTimer.Interval == AppUtil.NoFeedSecondInterval)
            {
                AppUtil.LogFileWrite("Attempting to wake LiveFeed from sleep state...................");
                int workingTimerInterval = 0;
                FeedPullerTimer.Stop();//if (FeedPullerTimer.Enabled)
                RunService(ref workingTimerInterval);
                if (workingTimerInterval > 0)
                    FeedPullerTimer.Interval = workingTimerInterval;
                AppUtil.LogFileWrite("LiveFeed poke from sleep state...................");
                FeedPullerTimer.Start();
            }
        }
        //private void MatchManagerTimer_Elapsed(object sender, ElapsedEventArgs e, string League)
        //{
        //    throw new NotImplementedException();
        //}

        private void InitFeedPuller()
        {
            FeedPullerTimer.Interval = AppUtil.RequestSecondInterval * 1000;
            FeedPullerTimer.Interval = 60000;
            FeedPullerTimer.AutoReset = true;
            FeedPullerTimer.Elapsed += FeedPullerTimer_Elapsed;
            FeedPullerTimer.Start();
        }

        //private void InitMatchManagerTimer()
        //{
        //    LoadTimerTable();
        //    foreach (AppUtil.FeedTimer mngTim in MatchManagerTimer)
        //    {
        //        mngTim.Interval = AppUtil.RequestSecondInterval * 1000;
        //        mngTim.Interval = 60000;
        //        mngTim.AutoReset = true;
        //        mngTim.Elapsed += ((sender, e) => MatchManagerTimer_Elapsed(sender, e, mngTim.LeagueName));//((sender, e) => PlayMusicEvent(sender, e, musicNote));
        //        mngTim.Start();

        //    }
        //}

        //private void LoadTimerTable()
        //{
        //    AppUtil.FeedTimer cusTime = new AppUtil.FeedTimer();
        //    LeagueAvailble = AppUtil.EnabledLeague;
        //    foreach (League l in AppUtil.EnabledLeagues)
        //    {
        //        cusTime.LeagueName = l.LeagueName;
        //        MatchManagerTimer.Add(cusTime);
        //    }
        //}
        private void InitKeepAlive()
        {
            FeedKeepAlive.Interval = AppUtil.KeepAliveMinute * 1000;
            FeedKeepAlive.AutoReset = true;
            FeedKeepAlive.Elapsed += FeedKeepAlive_Elapsed;
            FeedKeepAlive.Start();
        }


    }
}
