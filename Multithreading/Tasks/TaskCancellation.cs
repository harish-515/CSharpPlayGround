using System;
using System.Threading;
using System.Threading.Tasks;

namespace CSharpPlayGrond.Multithreading
{
    public static class CompositeCancellationDemo
    {
        public static void Demo()
        {
            var planned = new CancellationTokenSource();
            var preservative = new CancellationTokenSource();
            var emergency = new CancellationTokenSource();

            var paranoid = CancellationTokenSource.CreateLinkedTokenSource(
                planned.Token, preservative.Token, emergency.Token);

            Task.Factory.StartNew(() =>
            {
                int i = 0;
                while (true)
                {
                    paranoid.Token.ThrowIfCancellationRequested();
                    Console.WriteLine($"{i++} \t");
                    Thread.Sleep(1000);
                }
            });

            Console.ReadKey();
            // can cancel either of the planned/preservative/emergency
            // will trigger the cancellation on paranoid token
            emergency.Cancel();
        }
    }

    internal class TaskCancellationDemo
    {
        public static void Demo()
        {
            var cts = new CancellationTokenSource();
            var token = cts.Token;

            // Knowing that the task has been cancelled -- 1
            token.Register(() =>
            {
                Console.WriteLine("Cancellation has been requested.");
            });

            var t = new Task(() =>
            {
                int i = 0;
                while (true)
                {
                    // Soft exit
                    //if (token.IsCancellationRequested)
                    //    break;

                    // best practise is to throw an cancellation execption
                    //if (token.IsCancellationRequested)
                    //{
                    //    throw new OperationCanceledException();
                    //}

                    // same as above but encapulated into token
                    token.ThrowIfCancellationRequested();

                    Console.WriteLine($"{i++} \t");
                };
            }, token);
            t.Start();

            // Knowing that the task has been cancelled -- 2
            Task.Factory.StartNew(() =>
            {
                token.WaitHandle.WaitOne();

                Console.WriteLine("Wait handler released, As cancellation has been requested.");
            });

            Console.ReadKey();
            cts.Cancel();
        }
    }
}