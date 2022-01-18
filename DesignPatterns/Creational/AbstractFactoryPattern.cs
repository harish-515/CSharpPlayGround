using System;
using System.Collections.Generic;

namespace CSharpPlayGrond.DesignPatterns.Creational
{
    #region "Example 1"

    internal interface Shape
    {
        string Name { get; set; }

        string GetDescription();
    }

    internal interface IEarPhones
    {
        string Name { get; set; }

        string GetDescription();
    }

    internal interface IAccessories
    {
    }

    internal class AbstractFactoryPatternDemo
    {
    }

    #endregion "Example 1"

    #region "Problem 1"

    // Abstarct Factory return abstract class or interface

    //public interface IHotDrink
    //{
    //    void Consume();
    //}

    //internal class Tea : IHotDrink
    //{
    //    public void Consume()
    //    {
    //        Console.WriteLine("This is Tea.");
    //    }
    //}

    //internal class Coffee : IHotDrink
    //{
    //    public void Consume()
    //    {
    //        Console.WriteLine("This is Coffee.");
    //    }
    //}

    //public interface IHotDrinkFactory
    //{
    //    IHotDrink Prepare(int amount);
    //}

    //internal class TeaFactory : IHotDrinkFactory
    //{
    //    public IHotDrink Prepare(int amount)
    //    {
    //        Console.WriteLine($"Preparing Tea {amount}....");
    //        return new Tea();
    //    }
    //}

    //internal class CoffeeFactory : IHotDrinkFactory
    //{
    //    public IHotDrink Prepare(int amount)
    //    {
    //        Console.WriteLine($"Preparing Coffee {amount}....");
    //        return new Coffee();
    //    }
    //}

    //public class HotDrinkMachine
    //{
    //    public enum AvailableDrink
    //    {
    //        Tea,
    //        Coffee
    //    }

    //    private Dictionary<AvailableDrink, IHotDrinkFactory> factories = new Dictionary<AvailableDrink, IHotDrinkFactory>();

    //    public HotDrinkMachine()
    //    {
    //        foreach(AvailableDrink drink in Enum.GetValues(typeof(AvailableDrink)))
    //        {
    //            var factory = (IHotDrinkFactory)Activator.CreateInstance(Type.GetType("CSharpPlayGrond.DesignPatterns.Creational." +Enum.GetName(typeof(AvailableDrink), drink) + "Factory"));
    //            factories.Add(drink, factory);
    //        }
    //    }

    //    public IHotDrink MakeDrink(AvailableDrink drink,int amount)
    //    {
    //        return factories[drink].Prepare(amount);
    //    }

    //}

    //public class Demo
    //{
    //    public Demo()
    //    {
    //        var machine = new HotDrinkMachine();
    //        var drink = machine.MakeDrink(HotDrinkMachine.AvailableDrink.Tea, 10);
    //        drink.Consume();
    //    }
    //}

    #endregion "Problem 1"

    #region "Problem 2"

    public interface IHotDrink
    {
        void Consume();
    }

    internal class Tea : IHotDrink
    {
        public void Consume()
        {
            Console.WriteLine("This is Tea.");
        }
    }

    internal class Coffee : IHotDrink
    {
        public void Consume()
        {
            Console.WriteLine("This is Coffee.");
        }
    }

    public interface IHotDrinkFactory
    {
        IHotDrink Prepare(int amount);
    }

    internal class TeaFactory : IHotDrinkFactory
    {
        public IHotDrink Prepare(int amount)
        {
            Console.WriteLine($"Preparing Tea {amount}....");
            return new Tea();
        }
    }

    internal class CoffeeFactory : IHotDrinkFactory
    {
        public IHotDrink Prepare(int amount)
        {
            Console.WriteLine($"Preparing Coffee {amount}....");
            return new Coffee();
        }
    }

    public class HotDrinkMachine
    {
        // We have replaced the Enum by iterating all the implementations of IHotDrinkFactory through reflection.

        private List<Tuple<string, IHotDrinkFactory>> factories = new List<Tuple<string, IHotDrinkFactory>>();

        public HotDrinkMachine()
        {
            foreach (var t in typeof(HotDrinkMachine).Assembly.GetTypes())
            {
                // check if the type t is implementing IHotDrinkFactory and not Interface
                //itself
                if (typeof(IHotDrinkFactory).IsAssignableFrom(t) && !t.IsInterface)
                {
                    factories.Add(Tuple.Create(
                        t.Name.Replace("Factory", string.Empty),
                        (IHotDrinkFactory)Activator.CreateInstance(t)
                        ));
                }
            }
        }

        public IHotDrink MakeDrink()
        {
            Console.WriteLine("Available Drinks : ");
            for (int i = 0; i < factories.Count; i++)
            {
                Console.WriteLine($"{i} : {factories[i].Item1}");
            }

            while (true)
            {
                string s;
                int drinkNo = 0, amount = 0;
                Console.WriteLine("Enter the required drink");
                s = Console.ReadLine();
                if (!string.IsNullOrEmpty(s))
                    int.TryParse(s, out drinkNo);

                if (drinkNo > 0 && drinkNo < factories.Count)
                {
                    Console.WriteLine("Enter the required amount");
                    s = Console.ReadLine();
                    if (!string.IsNullOrEmpty(s))
                        int.TryParse(s, out amount);

                    return factories[drinkNo].Item2.Prepare(amount);
                }

                Console.WriteLine("Incorrect Input Selected.");
            }
        }
    }

    public class Demo
    {
        public Demo()
        {
            var machine = new HotDrinkMachine();
            var drink = machine.MakeDrink();
            drink.Consume();
        }
    }

    #endregion "Problem 2"
}