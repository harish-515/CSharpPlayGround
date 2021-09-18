using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CSharpPlayGrond.DesignPatterns.Creational
{
    #region "Example"
    interface IProduct
    {
        string Name { get; set; }

        string GetDescription();    
    }

    class Product1 : IProduct
    {
        public string Name { get; set; }

        public string GetDescription()
        {
            return $" Product1 : {this.Name}";
        }
    }

    class Product2 : IProduct
    {
        public string Name { get; set; }

        public string GetDescription()
        {
            return $" Product2 : {this.Name}";
        }
    }

    interface IProductCreator
    {
        IProduct CreateProduct();
    }

    class Product1Creator : IProductCreator
    {
        public IProduct CreateProduct()
        {
            return new Product1();
        }
    }

    class Product2Creator : IProductCreator
    {
        public IProduct CreateProduct()
        {
            return new Product2();
        }
    }

    static class FactoryMethod
    {
        public static void demo()
        {
            IProductCreator p1c = new Product1Creator();
            IProduct p1 = p1c.CreateProduct();
            p1.Name = "Sample1";
            Console.WriteLine(p1.GetDescription());

            IProductCreator p2c = new Product2Creator();
            IProduct p2 = p1c.CreateProduct();
            p2.Name = "Sample2";
            Console.WriteLine(p2.GetDescription());

        }

    }
    #endregion

    #region "Problem 1"
    //public enum CoordinateSystem
    //{
    //    Cartesian,
    //    Polar
    //}
    //class Point
    //{
    //    private readonly double x;
    //    private readonly double y;

    //    public Point(double a,double b,
    //        CoordinateSystem sys = CoordinateSystem.Cartesian)
    //    {
    //        switch (sys)
    //        {
    //            case CoordinateSystem.Cartesian:
    //                x = a;
    //                y = b;
    //                break;

    //            case CoordinateSystem.Polar:
    //                x = a * Math.Cos(b);
    //                y = a * Math.Sin(b);
    //                break;

    //            default:
    //                throw new ArgumentOutOfRangeException(nameof(sys), sys, null); 
    //        }   
    //    }
    //}


    //class Point
    //{
    //    private double x;
    //    private double y;

    //    public Point(double x, double y)
    //    {
    //        this.x = x;
    //        this.y = y;
    //    }

    //    // factory methods to create new objects as desired
    //    // overload with same arguments (which is not posible with constructor overloading)
    //    // Better readability
    //    public static Point NewCartesionPoint(double x,double y)
    //    {
    //        return new Point(x, y);
    //    }
    //    public static Point NewPolarPoint(double rho, double theta)
    //    {
    //        return new Point(rho * Math.Cos(theta), rho * Math.Sin(theta));
    //    }
    //}
    #endregion

    #region "Problem 2"
    // asynchronos initialization.
    // as constructor cannot initialize in async. Factories can do that for us.

    //public class Foo
    //{
    //    //public Foo()
    //    //{
    //    //       // await Task.Delay(1000); cannot be done..
    //    //}

    //    //public async Task<Foo> InitAsync()
    //    //{
    //    //    await Task.Delay(1000);
    //    //    return this;
    //    //}


    //    // ----------------- Factory 

    //    private Foo()
    //    {
          
    //    }

    //    private async Task<Foo> InitAsync()
    //    {
    //        await Task.Delay(1000);
    //        return this;
    //    }

    //    public static Task<Foo> CreateAsync()
    //    {
    //        var result = new Foo();
    //        return result.InitAsync(); 
    //    } 
    //}

    //public class Problem2Demo
    //{
    //    public static async Task Demo()
    //    {
    //        //var foo = new Foo();
    //        //await foo.InitAsync(); 

    //       Foo x = await Foo.CreateAsync();
    //    } 
    //}
    #endregion

    #region "Problem 2"
    // Seperate Factory Class
    // When moving to a seperate factory. We must keep tht constructor public to be available for usage
    // And as this will be available publically . users can still use ths constructor whihc ma be a problem.

    // To avoid that make the constructor internal (only solves if we expose the functionality as a library)
    // make the factory method as inner class
    class Point1
    {
        private readonly double x;
        private readonly double y;

        private Point1(double x,double y)
        {
            this.x = x;
            this.y = y;
        }

        //public static Point OriginProperty1 => new Point(0, 0); // initializes a new point every time we get
        //public static Point OriginProperty2 = new Point(0, 0);  // single copy is maintained and sent as reference


        // using static class
        //public class Factory
        //{
        //    public static Point NewCartesionPoint(double x, double y)
        //    {
        //        return new Point(x, y);
        //    }
        //    public static Point NewPolarPoint(double rho, double theta)
        //    {
        //        return new Point(rho * Math.Cos(theta), rho * Math.Sin(theta));
        //    }
        //}



        public static PointFactory Factory => new PointFactory(); 
        public class PointFactory
        {
            public  Point1 NewCartesionPoint(double x, double y)
            {
                return new Point1(x, y);
            }
            public  Point1 NewPolarPoint(double rho, double theta)
            {
                return new Point1(rho * Math.Cos(theta), rho * Math.Sin(theta));
            }
        }
    }


    public class Problem3Demo
    {
        public static void  Demo()
        {
            var point1 = Point1.Factory.NewCartesionPoint(1, 2); 
        }
    }
    #endregion
}
