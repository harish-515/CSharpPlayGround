using System;

namespace CSharpPlayGrond.DesignPatterns.SOLID_Design_Principles
{
    public class Rectangle
    {
        public virtual int Width { get; set; }
        public virtual int Height { get; set; }

        public Rectangle()
        {
        }

        public Rectangle(int w, int h)
        {
            this.Height = h;
            this.Width = w;
        }

        public override string ToString()
        {
            return $"Rectangle  Height :{Height} Width:{Width}";
        }
    }

    public class Square : Rectangle
    {
        public override int Width
        {
            set { base.Width = base.Height = value; }
        }

        public override int Height
        {
            set { base.Height = base.Width = value; }
        }
    }

    internal static class LiskovSubstituionPrincipleDemo
    {
        public static int Area(Rectangle r) => r.Width * r.Height;

        public static void Demo()
        {
            Rectangle rc = new Rectangle(2, 3);
            Console.WriteLine(rc.ToString());

            //Square sq = new Square();
            //sq.Width = 4;

            //Console.WriteLine($"Area of {sq.ToString()} : {Area(sq)}");

            Rectangle sq = new Square();
            sq.Width = 4;

            Console.WriteLine($"Area of {sq.ToString()} : {Area(sq)}");
        }
    }
}