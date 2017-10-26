using DataModel;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using System.Xml.Linq;
using System.Xml.Serialization;
using System.Timers;
using System.ServiceProcess;


namespace GoalAlert
{
    internal class Program
    {
        #region ManualRun
        
        static Timer FeedPullerTimer = new Timer();
        

        //protected static void Main(string[] args)
        //{
        //    Console.WriteLine("Welcome to Live Goal Alert Feed Manual Testing!");
        //    Console.WriteLine("You can start or stop the service at any point in time.");
        //    Console.WriteLine("Press 1 to START.....or........Press 2 to STOP........and........Press ESC to END..........");
        //    do
        //    {
        //        while (!Console.KeyAvailable)
        //        {
        //            // Do something
        //            ConsoleKeyInfo info = Console.ReadKey();
        //            if (info.Key == ConsoleKey.Escape)
        //            {
        //                Console.WriteLine("You pressed escape! Bye");
        //            }
        //            // Call ReadKey again and test for the letter a.
        //            info = Console.ReadKey();
        //            if (info.KeyChar == '1')
        //            {
        //                Console.WriteLine("You pressed 1.....The Job is starting");
        //                Start();
        //            }
        //            // Call ReadKey again and test for control-X.
        //            // ... This implements a shortcut sequence.
        //            info = Console.ReadKey();
        //            if (info.KeyChar =='2')
        //            {
        //                Console.WriteLine("You pressed 2.....The Job will stop");
        //                Stop();
        //            }
        //            Console.Read();
        //        }
        //    } while (Console.ReadKey(true).Key != ConsoleKey.Escape);
        //    Console.WriteLine("You pressed escape! Bye");


        //}

        //private Program()
        //{
        //    FeedPullerTimer.Elapsed += FeedPullerTimer_Elapsed;

        //}
        // private static void Start()
        //{
        //    try
        //    {
        //        AppUtil.LogFileWrite("Live Feed Starting.....");
        //        // TODO: Add code here to start your service.
        //        FeedPullerTimer.Interval = AppUtil.RequestSecondInterval * 1000;
        //        FeedPullerTimer.Start();
        //        AppUtil.LogFileWrite("Live Feed Started.....");
        //    }
        //    catch (Exception ex)
        //    {
        //        AppUtil.LogFileWrite("Live Feed Service Start Error: " + ex.Message);
        //        //Log detail to db
        //    }

        //}
        // private static void Stop()
        // {
        //     try
        //     {
        //         AppUtil.LogFileWrite("Live Feed Stopping.....");
        //         FeedPullerTimer.Dispose();
        //         AppUtil.LogFileWrite("Live Feed Stopped.....");
        //     }
        //     catch (Exception ex)
        //     {
        //         AppUtil.LogFileWrite("Live Feed Service Stop Error: " + ex.Message);
        //         //Log detail to db
        //     }

        // }

        //private static void RunService(object obj)
        //{
        //    try
        //    {
        //        // Cast the obj argument to clsTime.
        //        if (obj is Timer)
        //        {
        //            Timer jobTimer = (Timer)obj;
        //            FixturePuller.StartPull(jobTimer);
        //        }
        //    }
        //    catch (Exception rex)
        //    {
        //        AppUtil.LogFileWrite("Live Feed Service Run Error: " + rex.Message);
        //        //Log detail to db
        //    }
        //}
        //private void FeedPullerTimer_Elapsed(object sender, ElapsedEventArgs e)
        //{
        //    if (FeedPullerTimer.Enabled) FeedPullerTimer.Stop();
        //    RunService(FeedPullerTimer);
        //    FeedPullerTimer.Start();
        //} 
        #endregion
        private static void Main(string[] args)
        {
#if(!DEBUG)
            ServiceBase[] ServicesToRun;
            ServicesToRun = new ServiceBase[] 
                   { 
                        new LiveFeedManager() 
                   };
            ServiceBase.Run(ServicesToRun);
#else
            LiveFeedManager myServ = new LiveFeedManager();
            FeedPullerTimer.Interval = 60000;
            int inter = 1000;
            //ref int i = 1000;
            //myServ.RunService(FeedPullerTimer);
            myServ.RunService(ref inter);
            // here Process is my Service function
            // that will run when my service onstart is call
            // you need to call your own method or function name here instead of Process();
#endif

        }
    }

}
