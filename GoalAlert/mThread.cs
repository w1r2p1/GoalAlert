using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;
 
namespace GoalAlert
{
    class mThread
    {
        static int _threadCounter = 0;
 
        static void JobStart(string[] args)
        {
            for (int i = 0; i < 10; i++)
            {
                CreateThread(i);
            }
 
            StopProcessing();
 
            Console.WriteLine("Finished");
        }
 
        private static void CreateThread(int threadId)
        {
            Console.WriteLine("Creating Thread {0}", threadId);
            MethodParameters newParameter = new MethodParameters();
            newParameter.ThreadId = threadId;
            newParameter.ThreadCreated = DateTime.Now;
            Interlocked.Increment(ref _threadCounter);
            ThreadPool.QueueUserWorkItem(new WaitCallback(LongRunningProcess), (object)newParameter);
        }
 
        private static void LongRunningProcess(object input)
        {
            MethodParameters inputParams = (MethodParameters)input;
            Random random = new Random(((MethodParameters)input).ThreadId);
            Thread.Sleep(random.Next(500, 5000));
            Console.WriteLine("Thread {0} complete in {1} seconds", inputParams.ThreadId,
                (DateTime.Now - inputParams.ThreadCreated).TotalSeconds);
 
            Interlocked.Decrement(ref _threadCounter);
        }
 
        private static void StopProcessing()
        {
            Console.WriteLine("Waiting for {0} threads to complete", _threadCounter);
            while (_threadCounter > 0)
                Thread.Sleep(500);
        }
 
        private class MethodParameters
        {
            public DateTime ThreadCreated { get; set; }
            public int ThreadId { get; set; }
        }
    }
}