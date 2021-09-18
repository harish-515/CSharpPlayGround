using Autofac;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace CSharpPlayGrond.DesignPatterns.Creational
{

#region " Example 1 "
    class SingletonDemo
    {
        public string Value { get; set; }

        private static SingletonDemo _instance;

        private static readonly object _threadlock = new object(); 
        private SingletonDemo()
        {
                
        }

        public static SingletonDemo GetInstance()
        {
            if(_instance == null)
            {
                lock (_threadlock) { 
                if(_instance ==null) 
                        _instance = new SingletonDemo();
                }
            }
        return _instance;
        }
    }

    sealed class PerThreadSingletonDemo
    {
        public string Value { get; set; }

        private static ThreadLocal<PerThreadSingletonDemo> _threadInstance
            => new ThreadLocal<PerThreadSingletonDemo>(()=> new PerThreadSingletonDemo());

        private PerThreadSingletonDemo()
        {

        }

        public static PerThreadSingletonDemo GetThreadInstance()
        {
            return _threadInstance.Value;
        }
    }

    public class SingletonTester
    {
        public static void TestSingleton()
        {
            SingletonDemo ins1 = null;
            SingletonDemo ins2 = null;

            Thread t1 = new Thread(() =>
            {
                ins1 = SingletonDemo.GetInstance();
            }
            );

            Thread t2 = new Thread(() =>
            {
                ins2 = SingletonDemo.GetInstance();
            }
            );

            t1.Start();
            t2.Start();

            t1.Join();
            t2.Join();

            Console.WriteLine(Object.ReferenceEquals(ins1, ins2));
        }

        public static void TestPerThreadSingleton()
        {
            PerThreadSingletonDemo ins1 = null;
            PerThreadSingletonDemo ins2 = null;

            Thread t1 = new Thread(() =>
            {
                ins1 = PerThreadSingletonDemo.GetThreadInstance();
            }
            );

            Thread t2 = new Thread(() =>
            {
                ins2 = PerThreadSingletonDemo.GetThreadInstance();
            }
            );

            t1.Start();
            t2.Start();

            t1.Join();
            t2.Join();

            Console.WriteLine(Object.ReferenceEquals(ins1, ins2));
        }
    }

    #endregion


    #region "  Example 2 "
    public interface IDatabase
    {
        int GetPopulation(string name);

    }

    public class SingletonExp2 : IDatabase
    {
        private static int instance_count = 0;
        public static int InstanceCount
        {
            get => instance_count;
        }
        private Dictionary<string, int> capitals;
        private SingletonExp2()
        {
            instance_count++;
            Thread t = new Thread(() =>
            {
                capitals = new Dictionary<string, int>();
                capitals.Add("1", 1);
                capitals.Add("2", 2);
                capitals.Add("3", 3);
                capitals.Add("4", 4);
                capitals.Add("5", 5);
                capitals.Add("6", 6);

                Thread.Sleep(2000);
            });

            t.Join();
        }

        public int GetPopulation(string name)
        {
            return capitals.Keys.Contains(name)?capitals[name]:-1;
        }

        //private static SingletonExp2 _instance = new SingletonExp2();
        
        // lazy make sure to construct the object only when needed
        private static Lazy<SingletonExp2> _instance = new Lazy<SingletonExp2>();
        public static SingletonExp2 Instance {
            get => _instance.Value;
        }


    }


    public class OrdinaryDatabase : IDatabase
    {
        private Dictionary<string, int> capitals;
        private OrdinaryDatabase()
        {
            Thread t = new Thread(() =>
            {
                capitals = new Dictionary<string, int>();
                capitals.Add("1", 1);
                capitals.Add("2", 2);
                capitals.Add("3", 3);
                capitals.Add("4", 4);
                capitals.Add("5", 5);
                capitals.Add("6", 6);

                Thread.Sleep(2000);
            });

            t.Join();
        }

        public int GetPopulation(string name)
        {
            return capitals.Keys.Contains(name) ? capitals[name] : -1;
        }



    }

    public class Exp2Demo
    {
        public static void Demo()
        {
            var db = SingletonExp2.Instance;
            Console.WriteLine(db.GetPopulation("1"));
        }
    }
    
    [TestFixture]
    public class SingletonEx2Tests
    {
        [Test]
        public void IsSingleton_SingletonEx2()
        {
            var i1 = SingletonExp2.Instance;
            var i2 = SingletonExp2.Instance;


            Assert.That(i1, Is.SameAs(i2));
            Assert.That(SingletonExp2.InstanceCount, Is.EqualTo(1));

        }

        public void DI_PolulationTest()
        {
            var cb = new ContainerBuilder();
            // the ordinary database is not singleton by default
            // but through dependeancy injection we make it as singleton.

            cb.RegisterType<OrdinaryDatabase>()
                .As<IDatabase>()
                .SingleInstance();
            using(var c = cb.Build())
            {
                var d = c.Resolve<IDatabase>();
            }
        }
    }

    #endregion


    #region " Ambient Context "

    public sealed class BuildingContext : IDisposable
    {
        public int WallHeight;

        private static Stack<BuildingContext> stack = new Stack<BuildingContext>();

        public BuildingContext()
        {
            stack.Push(new BuildingContext(0));
        }

        public BuildingContext(int h)
        {
            this.WallHeight = h;
            stack.Push(this);
        }

        public static BuildingContext Current => stack.Peek();

        public void Dispose()
        {
            if (stack.Count > 1)
                stack.Pop();
        }
    }

    public class Building
    {
        public List<Wall> Walls = new List<Wall>();
        public override string ToString()
        {
            var sb = new StringBuilder();
            foreach(Wall w in Walls)
            {
                sb.AppendLine($" Wall : {w.ToString()} ");
            }
            return sb.ToString();
        }
    }

    public class Wall
    {
        public WallPoint Start, End;
        public int Height;

        public Wall(WallPoint s,WallPoint e)
        {
            this.Start = s;
            this.End = e;
            //this.Height = height;
            this.Height = BuildingContext.Current.WallHeight;
        }

        public override string ToString()
        {
            return $" Start : {Start.ToString()} ,End : {End.ToString()}, Height : {this.Height} ";
        }
    }

    public class WallPoint
    {
        private int x, y;
        public WallPoint(int x,int y)
        {
            this.x = x;
            this.y = y;
        }

        public override string ToString()
        {
            return $" x: {x} ,y: {y}";
        }
    }

    public static class AmbientContextDemo
    {
        public static void Demo()
        {
            var house = new Building();

            using (new BuildingContext(3000))
            {
                //ground floor 3000 height
                house.Walls.Add(new Wall(new WallPoint(0, 0), new WallPoint(5000, 0)));
                house.Walls.Add(new Wall(new WallPoint(0, 0), new WallPoint(0, 4000)));

                //first floor 3500 height
                using (new BuildingContext(3500))
                {
                    house.Walls.Add(new Wall(new WallPoint(0, 0), new WallPoint(6000, 0)));
                    house.Walls.Add(new Wall(new WallPoint(0, 0), new WallPoint(0, 4000)));
                }

                //ground floor 3000 height
                house.Walls.Add(new Wall(new WallPoint(0, 0), new WallPoint(5000, 4000)));

            }
            Console.WriteLine(house);

        }
    }
}
    #endregion
