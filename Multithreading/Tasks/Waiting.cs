using System;
using System.Threading;
using System.Threading.Tasks;

namespace CSharpPlayGrond.Multithreading
{
    internal class WaitingInTasks
    {
        public static void WaitingDemo()
        {
            // Pauses the current thread
            // -- Job Schedular can pick another thread for execution in this time
            // -- Context switching happens
            // -- No CPU cycles wasted
            // ***Thread.Sleep(1000);

            // Pauses the current thread
            // -- thread holds the place in the scheduler
            // -- No Context Switches
            // -- Wastage of CPU time
            // ***SpinWait.SpinUntil();

            var cts = new CancellationTokenSource();
            var token = cts.Token;
            var t = new Task(() =>
            {
                Console.WriteLine("Press any key to disarm : you have 5 seconds");
                bool canelled = token.WaitHandle.WaitOne(5000);
                Console.WriteLine(canelled ? "Disarmed" : "Boom");
            }, token);

            Console.ReadKey();
        }

        public static void TaskWaitingDemo()
        {
            var cts = new CancellationTokenSource();
            var token = cts.Token;

            var t = new Task(() =>
           {
               Console.WriteLine("Task runs for 5 secs");

               for (int i = 0; i < 5; i++)
               {
                   token.ThrowIfCancellationRequested();
                   Thread.Sleep(1000);
               }

               Console.WriteLine("Task is done");
           });

            t.Start();

            // wait on the task unti its complete or cancel
            //t.Wait(token);

            Task t2 = Task.Factory.StartNew(() =>
           {
               Console.WriteLine("Task 2 started");
               Thread.Sleep(3000);
               Console.WriteLine("Task 2 ended");
           }, token);

            // waits for completion of task t & t2
            Task.WaitAll(t, t2);

            // waits untill the completions of one task either t or t2
            // t2 is completed in 3 secs , So it waits till t2 completion
            //Task.WaitAny(t, t2);

            // will wait until 4 sec or till any one on them is completed or cancelled
            // this also throws an exception if cancelled WaitAll,WaitAny
            Task.WaitAny(new[] { t, t2 }, 4000, token);
        }
    }
}