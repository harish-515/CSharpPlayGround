using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace CSharpPlayGrond.Multithreading.Tasks
{
    public class BankAccount{
        public int Balance { get; set; }

        public void Deposit(int amt)
        {

            //+=
            // op1 = temp <= get_balance() + amt
            // op2 = set_balance <= temp 
            // b/w these 2 ops another thread can execute

            Balance += amt;
        }

        public void Withdraw(int amt)
        {
            Balance -= amt;
        }

        public void Transfer(BankAccount ba, int v)
        {
            this.Withdraw(v);
            ba.Deposit(v);
        }
    }

    public class BankAccountTaskSafe
    {
        private object padlock = new object();
        public int Balance { get; set; }

        public void Deposit(int amt)
        {
            // critical section 1
            lock (padlock) { 
            Balance += amt;
            }
        }

        public void Withdraw(int amt)
        {
            // critical section 2
            lock (padlock) { 
            Balance -= amt;
            }
        }
    }

    public class BankAccountInterlock
    {
        // Interlocked Offers some built in functions 
        // to run few non atomic operations in atomic way

        private int balance;
        public int Balance { get; set; }

        public void Deposit(int amt)
        {
            // critical section 1
            // does the operations in a atomic way
            Interlocked.Add(ref balance,amt);
        }

        public void Withdraw(int amt)
        {
            // critical section 2
            Interlocked.Add(ref balance, -amt);
        }
    }

    class TaskDataSharing
    {
        public static void BankAccountDemo()
        {
            var tasks = new List<Task>();
            var ba = new BankAccountTaskSafe();

            for(int i = 0; i < 10; i++)
            {
                tasks.Add(Task.Factory.StartNew(() => { 
                    for(int j = 0; j < 1000; j++)
                    {
                        ba.Deposit(100);
                    }
                }));
                tasks.Add(Task.Factory.StartNew(() => {
                    for (int j = 0; j < 1000; j++)
                    {
                        ba.Withdraw(100);
                    }
                }));
            }

            Task.WaitAll(tasks.ToArray());

            Console.WriteLine($" balance in account : {ba.Balance} ");
        }
        public static void BankAccountSpinLock()
        {
            var tasks = new List<Task>();
            var ba = new BankAccount();

            SpinLock sl = new SpinLock();

            // Spinlock if lock aquired will run the critical section
            // if not wait (spinwait) unitl the critical section is run for that 
            // task


            for (int i = 0; i < 10; i++)
            {
                tasks.Add(Task.Factory.StartNew(() => {
                    for (int j = 0; j < 1000; j++)
                    {
                        var locktaken = false;
                        try
                        {
                            sl.Enter(ref locktaken);
                            ba.Deposit(100);
                        }
                        finally
                        {
                            if (locktaken) 
                                sl.Exit();
                        }
                    }
                }));
                tasks.Add(Task.Factory.StartNew(() => {
                    for (int j = 0; j < 1000; j++)
                    {
                        var locktaken = false;
                        try
                        {
                            sl.Enter(ref locktaken);
                            ba.Withdraw(100);
                        }
                        finally
                        {
                            if (locktaken)
                                sl.Exit();
                        }
                    }
                }));
            }

            Task.WaitAll(tasks.ToArray());

            Console.WriteLine($" balance in account : {ba.Balance} ");
        }

        // problems with spinlock
        // System.Threading.SpinLock is a low-level mutual exclusion lock that you
        // can use for scenarios that have very short wait times.
        public static void LockRecurssion(int x)
        {
            bool lockTaken = false;
            var sl = new SpinLock(true);
            // without the enablethreadownerlocking set we wont be able to identify
            // if we are having a deadlock beacuse of recurssion.
            // by setting emablethreadownerlocking an exceptions is thrown whn we try
            // to obtain locks recursively
            try
            {
                sl.Enter(ref lockTaken);
            }
            catch(LockRecursionException ex)
            {
                // this exception is thrown when we try to acquire the lock recursivelly
                Console.WriteLine(ex.Message);
                throw;
            }
            finally
            {
                if (lockTaken)
                {
                    Console.WriteLine($"took a lock,x ={x}");
                    LockRecurssion(x - 1);
                    sl.Exit();
                }
                else
                {
                    Console.WriteLine($"Unable to get a lock,x ={x}");
                }
            }
        }

        public static void LockRecurssionDemo()
        {
            LockRecurssion(5);
        }

        public static void MutexDemo1()
        {
            Mutex mtx = new Mutex();
            Mutex mtx1 = new Mutex();
            BankAccount ba = new BankAccount();
            BankAccount ba2 = new BankAccount();
            ba2.Balance = 1000;
            List<Task> tasks = new List<Task>();

            Console.WriteLine($"Balance in account ba   Before: {ba.Balance}");
            Console.WriteLine($"Balance in account ba 2 Before: {ba2.Balance}");


            for (int i = 0; i < 1000; i++)
            {
                tasks.Add(Task.Factory.StartNew(() => {
                    bool haveControl = mtx.WaitOne();
                    try
                    {
                        ba.Deposit(1);
                    }
                    finally
                    {
                        if (haveControl)
                            mtx.ReleaseMutex();
                    }
                }));
                tasks.Add(Task.Factory.StartNew(() => {
                    bool haveControl = mtx.WaitOne();
                    try
                    {
                        ba.Withdraw(1);
                    }
                    finally
                    {
                        if (haveControl)
                            mtx.ReleaseMutex();
                    }
                }));

                tasks.Add(Task.Factory.StartNew(() => {
                    bool haveControl = Mutex.WaitAll(new[] { mtx, mtx1 });
                    try
                    {
                        ba2.Transfer(ba, 1);
                    }
                    finally
                    {
                        if (haveControl)
                        {
                            mtx.ReleaseMutex();
                            mtx1.ReleaseMutex();
                        }
                    }
                }));


            }

            Console.WriteLine(tasks.Count());
            Task.WaitAll(tasks.ToArray());

            Console.WriteLine($"Balance in account ba   : {ba.Balance}");
            Console.WriteLine($"Balance in account ba 2 : {ba2.Balance}");
        }

        static int num = 0;
        public static void ReadWriteLockDemo()
        {
            ThreadPool.SetMinThreads(200, 200);
            int x = 0;
            Random rn = new Random();
            ReaderWriterLockSlim rwl = new ReaderWriterLockSlim();
            List<Task> tasks = new List<Task>();

            for (int i = 0; i < 10; i++)
            {
                tasks.Add(Task.Factory.StartNew(() => 
                {
                    //rwl.EnterReadLock();
                    rwl.EnterUpgradeableReadLock();

                    if(rn.Next() %2 == 0)
                    {
                        rwl.EnterWriteLock(); 
                        x *= 10;
                        rwl.ExitWriteLock();
                    }
                    Console.WriteLine($"Read lock acquired,value of x = {x}");
                    Thread.Sleep(2000);
                    rwl.ExitUpgradeableReadLock();
                    //rwl.ExitReadLock();
                }));

                tasks.Add(Task.Factory.StartNew(() =>
                {
                    rwl.EnterWriteLock();

                    x = ++num;
                    Console.WriteLine($"Write lock acquired,value of x = {x}");
                    Thread.Sleep(2000);

                    rwl.ExitWriteLock();
                }));

            }

            try
            {
                Task.WaitAll(tasks.ToArray());
            }catch(AggregateException ae)
            {
                ae.Handle(e =>
                {
                    Console.WriteLine(e.Message);
                    return true;
                });
            }

        }
    }
}
