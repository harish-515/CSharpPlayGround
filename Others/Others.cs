using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CSharpPlayGrond.Others
{
    public abstract class A
    {
        public abstract void JustImplement();
    }


    public class B : A
    {
        public override void JustImplement()
        {
            Console.WriteLine("This is from B");
        }
    }

    public class C : B
    {
        public new void JustImplement()
        {
            Console.WriteLine("This is from C");
        }
    }


    public class virtualA
    {
        public virtual void somemethod()
        {
            Console.WriteLine("this is from VA");
        }

        public void somethingnew()
        {
            Console.WriteLine("something new from VA");
        }
    }

    public class virtualB : virtualA
    {
        public int MyProperty { get; set; }

        public new void somemethod()
        {
            base.somethingnew();
            Console.WriteLine("this is from VB");
        }
    }



}
