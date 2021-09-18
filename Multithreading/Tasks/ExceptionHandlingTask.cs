using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace CSharpPlayGrond.Multithreading.Tasks
{
    class ExceptionHandlingTask
    {
        public static void ExceptionDemo()
        {
            try
            {
                ExceptionFromTask();
            }
            // Again An aggregated exception of the remaining
            // exceptions is sent for resolution here
            catch(AggregateException ae)
            {
                foreach (var ex in ae.InnerExceptions)
                {
                    Console.WriteLine($"Exception in parent catch : {ex.Message}");
                }
            }
        }

        private static void ExceptionFromTask()
        {
            var t1 = Task.Factory.StartNew(() => {
                throw new ArgumentNullException("Exception from t1");
            });

            var t2 = Task.Factory.StartNew(() => {
                throw new InvalidOperationException("Exception from t2");
            });

            // if we dont configure any waits what so ever we won't
            // even know that we have exceptions in the tasks . the Main thread would run fine
            var cts = new CancellationTokenSource();
            var token = cts.Token;

            var t3 = new Task(() =>
            {
                Console.WriteLine("Task 3 started");
                token.ThrowIfCancellationRequested();
                Thread.Sleep(2000);
                Console.WriteLine("Task 3 stopped");
            },token);

            Console.ReadKey();
            cts.Cancel();

            try
            {
                // With Wait configured the exceptions thrown by the 
                // tasks are aggregated and thrown collectively as AggregatedException
                Task.WaitAll(t1, t2,t3);
            }catch(AggregateException ae)
            {
                foreach (var ex in ae.InnerExceptions)
                {
                    Console.WriteLine(ex.Message);
                }

                // handle only part of exceptions in AggregateException
                ae.Handle(e =>
                {
                    // Only handeling ArgumentNullException and the rest are 
                    // propagated to the parent function
                    if (e is ArgumentNullException) {
                        Console.WriteLine("ArgumentNull Exception Handled");
                        return true;
                    }
                    return false;
                });
            }


        }


    }
}
