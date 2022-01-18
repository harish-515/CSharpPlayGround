using Autofac;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;

namespace CSharpPlayGrond.DesignPatterns.Structural.Adapter
{
    /// <summary>
    /// A construct whihc adapts an exisitng interface X
    /// to conform to the required interface Y
    /// </summary>

    // in many cases adapter pattern tends to create lots of temporary data.
    // and this gets create over and over again for the same set of conversion
    // when done multiple times.

    #region " Example 1 "

    public class Point
    {
        public int x, y;

        public Point(int x, int y)
        {
            this.x = x;
            this.y = y;
        }

        public override bool Equals(object obj)
        {
            return obj is Point point &&
                   x == point.x &&
                   y == point.y;
        }

        public override int GetHashCode()
        {
            int hashCode = 1502939027;
            hashCode = hashCode * -1521134295 + x.GetHashCode();
            hashCode = hashCode * -1521134295 + y.GetHashCode();
            return hashCode;
        }

        public override string ToString()
        {
            return $"({x},{y})";
        }
    }

    public class Line
    {
        public Point Start, End;

        public Line(Point start, Point end)
        {
            this.Start = start;
            this.End = end;
        }

        public override bool Equals(object obj)
        {
            return obj is Line line &&
                   EqualityComparer<Point>.Default.Equals(Start, line.Start) &&
                   EqualityComparer<Point>.Default.Equals(End, line.End);
        }

        public override int GetHashCode()
        {
            int hashCode = -1676728671;
            hashCode = hashCode * -1521134295 + EqualityComparer<Point>.Default.GetHashCode(Start);
            hashCode = hashCode * -1521134295 + EqualityComparer<Point>.Default.GetHashCode(End);
            return hashCode;
        }

        public override string ToString()
        {
            return $"Start: {Start.ToString()}, End : {End.ToString()}";
        }
    }

    public class VectorObject : Collection<Line>
    {
    }

    public class Rectangle : VectorObject
    {
        public Rectangle(int x, int y, int width, int height)
        {
            Add(new Line(new Point(x, y), new Point(x + width, y)));
            Add(new Line(new Point(x + width, y), new Point(x + width, y + height)));
            Add(new Line(new Point(x, y), new Point(x, y + height)));
            Add(new Line(new Point(x, y + height), new Point(x + width, y + height)));
        }
    }

    public class LineToPointAdapter : IEnumerable<Point>
    {
        private static int count = 0;
        private static Dictionary<int, IEnumerable<Point>> cache = new Dictionary<int, IEnumerable<Point>>();

        public LineToPointAdapter(Line line)
        {
            // use cacheing to minimize the temporary data generation
            var hash = line.GetHashCode();
            if (cache.ContainsKey(hash))
                return;
            else
            {
                var points = new List<Point>();

                Console.WriteLine($"Count : {++count}");
                Console.WriteLine(line.ToString());
                int left = Math.Min(line.Start.x, line.End.x);
                int right = Math.Min(line.Start.x, line.End.x);
                int top = Math.Min(line.Start.y, line.End.y);
                int bottom = Math.Min(line.Start.x, line.End.x);

                if (right - left > 0)
                {
                    for (int y = top; y <= bottom; y++)
                    {
                        points.Add(new Point(left, y));
                    }
                }
                else if (bottom - top > 0)
                {
                    for (int x = left; x <= right; x++)
                    {
                        points.Add(new Point(x, top));
                    }
                }
            }
        }

        public IEnumerator<Point> GetEnumerator()
        {
            return cache.Values.SelectMany(x => x).GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
    }

    public class AdapterDemo
    {
        private static readonly List<VectorObject> vectorObjects
            = new List<VectorObject>() {
                new Rectangle(1,1,10,10),
                new Rectangle(3,3,6,6)
            };

        public static void DrawPoint(Point p)
        {
            Console.Write(".");
        }

        // Line To Point adapter

        public static void Linedemo()
        {
            foreach (var vo in vectorObjects)
            {
                foreach (var line in vo)
                {
                    var adapter = new LineToPointAdapter(line);
                    adapter.ToList().ForEach(DrawPoint);
                }
            }
        }

        public static void Demo()
        {
            Linedemo();
            Linedemo();
        }
    }

    #endregion " Example 1 "

    #region " Generic Value Adapter "

    public interface IInteger
    {
        int Value { get; }
    }

    public static class Dimenssions
    {
        public class Two : IInteger
        {
            public int Value => 2;
        }

        public class Three : IInteger
        {
            public int Value => 3;
        }
    }

    //Vector2f,Vector3i

    // cannot be used where D is a literal
    // as C# dosent allow to do so.
    //public class Vector<T,D>
    //{
    //    protected T[] data;
    //    public Vector()
    //    {
    //        data = new T[D];
    //    }
    //}

    public class Vector<Tself, T, D>
        where D : IInteger, new()
        where Tself : Vector<Tself, T, D>, new()
    {
        protected T[] data;

        public Vector()
        {
            data = new T[new D().Value];
        }

        public Vector(params T[] values)
        {
            var reqSize = new D().Value;
            var proSize = values.Length;

            for (int i = 0; i < Math.Min(reqSize, proSize); ++i)
            {
                data[i] = values[i];
            }
        }

        public static Tself Create(params T[] values)
        {
            var res = new Tself();
            var reqSize = new D().Value;
            var proSize = values.Length;

            for (int i = 0; i < Math.Min(reqSize, proSize); ++i)
            {
                res[i] = values[i];
            }

            return res;
        }

        public T this[int index]
        {
            get => data[index];
            set => data[index] = value;
        }
    }

    public class VectorOfInt<Tself, D> : Vector<Tself, int, D>
        where D : IInteger, new()
        where Tself : Vector<Tself, int, D>, new()
    {
        public VectorOfInt()
        {
        }

        public VectorOfInt(params int[] values) : base(values)
        {
        }

        //public static Tself operator+(Tself left,
        //                              Tself right)
        //{
        //    var res = new Tself();
        //    var dim = new D().Value;
        //    for (int i = 0; i < dim; i++)
        //    {
        //        res[i] = left[i] + right[i];
        //    }

        //    return res;
        //}
    }

    public class Vector2i : VectorOfInt<Vector2i, Dimenssions.Two>
    {
        public Vector2i()
        { }

        public Vector2i(params int[] values) : base(values)
        {
        }
    }

    public class VectorOfFloat<Tself, D>
        : Vector<Tself, float, D>
        where D : IInteger, new()
        where Tself : Vector<Tself, float, D>, new()
    {
    }

    public class Vector3f
        : VectorOfFloat<Vector3f, Dimenssions.Three>
    {
    }

    public class GenericValueAdapterDemo
    {
        public static void Demo()
        {
            var v = new Vector2i();
            v[0] = 1;
            v[0] = 2;
            var vv = new Vector2i(2, 3);

            Vector3f v3f = Vector3f.Create(3.3f, 2.4f, 1.1f);
            // vf is not of type vector3f its just vector

            Vector2i v2i = Vector2i.Create(1, 4);
        }
    }

    #endregion " Generic Value Adapter "

    #region " Dependency Injection -- Adapter "

    public interface ICommand
    {
        void Execute();
    }

    public class SaveCommand : ICommand
    {
        public void Execute()
        {
            Console.WriteLine("Saves the Files");
        }
    }

    public class Button
    {
        public ICommand command;

        public Button(ICommand cmd)
        {
            this.command = cmd;
        }

        public void Click()
        {
            command.Execute();
        }
    }

    public class OpenCommand : ICommand
    {
        public void Execute()
        {
            Console.WriteLine("Opens the Files");
        }
    }

    public class Editor
    {
        public IEnumerable<Button> buttons;

        public Editor(IEnumerable<Button> buttons)
        {
            this.buttons = buttons;
        }

        public void ClickAll()
        {
            foreach (Button btn in buttons)
                btn.Click();
        }
    }

    public static class AdapterDIDemo
    {
        public static void Demo()
        {
            var cb = new ContainerBuilder();
            cb.RegisterType<SaveCommand>().As<ICommand>();
            cb.RegisterType<OpenCommand>().As<ICommand>();
            //cb.RegisterType<Button>();
            cb.RegisterAdapter<ICommand, Button>(cmd => new Button(cmd));

            cb.RegisterType<Editor>();
            using (var c = cb.Build())
            {
                var editor = c.Resolve<Editor>();
                editor.ClickAll();
            }
        }
    }

    #endregion " Dependency Injection -- Adapter "
}