using System;
using System.Collections.Generic;
using System.Diagnostics;

namespace CSharpPlayGrond.DesignPatterns.Structural
{
    /// <summary>
    /// A class which functions as an interface to a particular
    /// resource. That resource may be remote,expensive to construct
    /// or may require logging or some other added fucntionality.
    ///
    /// Proxy Vs Decorator --  No additional memebers only additional functionality
    /// in exisiting memebers
    ///
    ///
    /// </summary>

    #region " Protection Proxy "

    public interface ICar
    {
        void Drive();
    }

    public class Car : ICar
    {
        public void Drive()
        {
            Console.WriteLine("Car is being Driven");
        }
    }

    public class Driver
    {
        public Driver(int age)
        {
            Age = age;
        }

        public int Age { get; }
    }

    public class CarPoxy : ICar
    {
        public Driver Driver;
        public Car car = new Car();

        public CarPoxy(Driver driver)
        {
            Driver = driver;
        }

        public void Drive()
        {
            if (Driver.Age >= 16)
            {
                car.Drive();
            }
            else
            {
                Console.WriteLine("Too Young to driver the car.");
            }
        }
    }

    public static class ProtectionProxyDemo
    {
        public static void Demo()
        {
            ICar car = new CarPoxy(new Driver(22));
            car.Drive();
        }
    }

    #endregion " Protection Proxy "

    #region " Property Proxy "

    public class Property<T> : IEquatable<Property<T>> where T : new()
    {
        private T value;

        public T Value
        {
            get => value;
            set
            {
                if (Equals(this.value, value)) return;
                Console.WriteLine($"Assigning value to {value}");
                this.value = value;
            }
        }

        public Property() : this(Activator.CreateInstance<T>())
        {
        }

        public Property(T value)
        {
            this.value = value;
        }

        public static implicit operator T(Property<T> property)
        {
            return property.value; // int n = p_int;
        }

        public static implicit operator Property<T>(T value)
        {
            return new Property<T>(value); // Property<int> p = 123;
        }

        public static bool operator ==(Property<T> left, Property<T> right)
        {
            return EqualityComparer<Property<T>>.Default.Equals(left, right);
        }

        public static bool operator !=(Property<T> left, Property<T> right)
        {
            return !(left == right);
        }

        public override bool Equals(object obj)
        {
            return Equals(obj as Property<T>);
        }

        public bool Equals(Property<T> other)
        {
            return other != null &&
                   EqualityComparer<T>.Default.Equals(value, other.value) &&
                   EqualityComparer<T>.Default.Equals(Value, other.Value);
        }

        public override int GetHashCode()
        {
            int hashCode = 1927018180;
            hashCode = hashCode * -1521134295 + EqualityComparer<T>.Default.GetHashCode(value);
            hashCode = hashCode * -1521134295 + EqualityComparer<T>.Default.GetHashCode(Value);
            return hashCode;
        }
    }

    // without Proxy
    //public class Creature
    //{
    //    public Property<int> Agility { get; set; }
    //}
    public class Creature
    {
        private Property<int> agility = new Property<int>();

        public int Agility
        {
            get => agility.Value;
            set
            {
                agility.Value = value;
            }
        }
    }

    public static class PropertyProxyDemo
    {
        public static void demo()
        {
            var c = new Creature();
            c.Agility = 10; // Without Proxy
                            // c.set_agility(10) is not called
                            // where as the implicit converter
                            // are called
                            // c.set_agility = new Property<int>(10)
                            // we are replacing the exisitng property object
                            // with a new one

            c.Agility = 10; // second time
        }
    }

    #endregion " Property Proxy "

    #region " Value Proxy "

    // proxy on a primitive type

    [DebuggerDisplay("{value*100.0f}%")]
    public struct Percentage
    {
        private readonly float value;

        internal Percentage(float value)
        {
            this.value = value;
        }

        public static float operator *(float f, Percentage p)
        {
            return f * p.value;
        }

        public static Percentage operator +(Percentage p1, Percentage p2)
        {
            return new Percentage(p1.value + p2.value);
        }

        public override string ToString()
        {
            return $"{value * 100}%";
        }
    }

    public static class PercentageExtenstions
    {
        public static Percentage Percent(this float value)
        {
            return new Percentage(value / 100f);
        }

        public static Percentage Percent(this int value)
        {
            return new Percentage(value / 100f);
        }
    }

    public static class ValueProxy
    {
        //// here price can be sent any thing as it is an
        //// interger type
        //static void BySomething(int price)
        //{
        //}

        private static void demo()
        {
            Console.WriteLine(10f * 5.Percent());
            Console.WriteLine(3.Percent() + 5.Percent());
        }
    }

    #endregion " Value Proxy "

    #region " Composite Proxy 1 "

    public class Monster
    {
        public byte Age;
        public int X, Y;
    }

    public class Monsters
    {
        private readonly int size;
        private readonly byte[] Age;
        private readonly int[] X;
        private readonly int[] Y;

        public Monsters(int size)
        {
            this.size = size;
            Age = new byte[size];
            X = new int[size];
            Y = new int[size];
        }

        public struct MonsterProxy
        {
            private readonly Monsters monsters;
            private readonly int index;

            public MonsterProxy(Monsters monsters, int index)
            {
                this.monsters = monsters;
                this.index = index;
            }

            public ref byte Age => ref monsters.Age[index];
            public ref int X => ref monsters.X[index];
            public ref int Y => ref monsters.Y[index];
        }

        public IEnumerator<MonsterProxy> GetEnumerator()
        {
            for (int pos = 0; pos < size; pos++)
            {
                yield return new MonsterProxy(this, pos);
            }
        }
    }

    public static class CompositionProxyDemo
    {
        public static void Demo()
        {
            // without Proxy
            // Memory Arrangement
            // Age X Y Age X Y Age X Y Age X Y Age X Y ..

            var monsters = new Monster[100];

            foreach (var c in monsters)
            {
                c.X++;
            }

            // with Proxy
            // Memory Arrangement
            // Age Age Age ..
            // X X X ..
            // Y Y Y ..

            var monsters2 = new Monsters(100);
            foreach (var c in monsters2)
                c.X++;
        }
    }

    #endregion " Composite Proxy 1 "

    #region " Composite Proxy 2 "

    public class MasonrySettings
    {
        //public bool? All
        //{
        //    get
        //    {
        //        if (Pillars == Walls &&
        //            Walls == Floors)
        //            return Pillars;
        //        else
        //            return null;
        //    }
        //    set
        //    {
        //        if(value.HasValue)
        //        {
        //            Pillars = value.Value;
        //            Walls = value.Value;
        //            Floors = value.Value;
        //        }
        //    }
        //}
        //public bool Pillars, Walls, Floors;

        // Array Backed Properties
        private readonly bool[] flags = new bool[3];

        public bool Pillars
        {
            get
            {
                return flags[0];
            }
            set
            {
            }
        }
    }

    #endregion " Composite Proxy 2 "
}