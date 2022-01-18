using System;
using System.Threading.Tasks;

namespace CSharpPlayGrond.Multithreading
{
    internal class TaskIntroduction
    {
        public static void Write(char c)
        {
            int i = 100;
            while (i-- > 0)
            {
                Console.Write(c);
            }
        }

        public static void Write(object o)
        {
            int i = 1000;
            while (i-- > 0)
            {
                Console.Write(o);
            }
        }

        public static int TextLength(object o)
        {
            Console.WriteLine($"Task with ID : {Task.CurrentId} processing object {o}..");
            return o.ToString().Length;
        }
    }

    internal class TaskIntroductionDemo
    {
        public static void Demo()
        {
            //Task.Factory.StartNew(() => TaskIntroduction.Write('.'));
            //var t = new Task(() =>
            //{
            //    TaskIntroduction.Write('?');
            //});
            //t.Start();
            //TaskIntroduction.Write('-');

            // passing the arguments into task action
            //Task t = new Task(TaskIntroduction.Write, "Hello");
            //t.Start();
            //Task.Factory.StartNew(TaskIntroduction.Write, 123);

            // Getting returns from task
            string text1 = "testing";
            string text2 = "this";

            var task1 = new Task<int>(TaskIntroduction.TextLength, text1);
            task1.Start();

            Task<int> task2 = Task.Factory.StartNew<int>(TaskIntroduction.TextLength, text2);

            Console.WriteLine($"Lenght of {text1} is : {task1.Result}");
            Console.WriteLine($"Lenght of {text2} is : {task2.Result}");
        }
    }
}