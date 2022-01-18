using System;
using System.Collections.Generic;

namespace CSharpPlayGrond.DesignPatterns.SOLID_Design_Principles
{
    public enum Color
    {
        Red, Green, Blue
    }

    public enum Size
    {
        Small, Medium, Large, Huge
    }

    public class Product
    {
        public string Name;
        public Color color;
        public Size size;

        public Product(string name, Color clr, Size s)
        {
            if (string.IsNullOrEmpty(name))
                throw new ArgumentNullException(paramName: nameof(Name));

            this.Name = name;
            this.color = clr;
            this.size = s;
        }
    }

    //public class ProductFilter
    //{
    //    public static IEnumerable<Product> FilterBySize(IEnumerable<Product> products,Size s)
    //    {
    //        foreach(var p in products)
    //        {
    //            if (p.size == s)
    //                yield return p;
    //        }
    //    }

    //    public static IEnumerable<Product> FilterByColor(IEnumerable<Product> products, Color c)
    //    {
    //        foreach (var p in products)
    //        {
    //            if (p.color  == c)
    //                yield return p;
    //        }
    //    }

    //}

    public interface ISpecification<T>
    {
        bool IsSatisfied(T t);
    }

    public interface IFilter<T>
    {
        IEnumerable<T> Filter(IEnumerable<T> items, ISpecification<T> spec);
    }

    public class ColorSpecification : ISpecification<Product>
    {
        private Color color;

        public ColorSpecification(Color clr)
        {
            this.color = clr;
        }

        public bool IsSatisfied(Product t)
        {
            return t.color == color;
        }
    }

    public class SizeSpecification : ISpecification<Product>
    {
        private Size size;

        public SizeSpecification(Size size)
        {
            this.size = size;
        }

        public bool IsSatisfied(Product t)
        {
            return t.size == size;
        }
    }

    public class AndSpecification<T> : ISpecification<T>
    {
        private ISpecification<T> spec1, spec2;

        public AndSpecification(ISpecification<T> spec1, ISpecification<T> spec2)
        {
            this.spec1 = spec1;
            this.spec2 = spec2;
        }

        public bool IsSatisfied(T t)
        {
            return spec1.IsSatisfied(t) && spec2.IsSatisfied(t);
        }
    }

    public class BetterFilter : IFilter<Product>
    {
        public IEnumerable<Product> Filter(IEnumerable<Product> items, ISpecification<Product> spec)
        {
            foreach (var i in items)
                if (spec.IsSatisfied(i))
                    yield return i;
        }
    }

    internal static class OpenClosedPrincipleDemo
    {
        private static void Demo()
        {
            var apple = new Product("Apple", Color.Red, Size.Medium);
            var Orange = new Product("Orange", Color.Green, Size.Huge);
            var house = new Product("House", Color.Blue, Size.Huge);

            Product[] products = { apple, Orange, house };
            //Console.WriteLine("Color Filter :");
            //foreach(var p in ProductFilter.FilterByColor(products, Color.Red))
            //{
            //    Console.WriteLine($"-  {p.Name}");
            //}

            //Console.WriteLine("Color Filter :");
            BetterFilter bf = new BetterFilter();
            foreach (var p in bf.Filter(products, new ColorSpecification(Color.Red)))
            {
                Console.WriteLine($"-  {p.Name}");
            }

            //new composite filter
            foreach (var p in bf.Filter(
                products, new AndSpecification<Product>(
                    new ColorSpecification(Color.Red),
                    new SizeSpecification(Size.Huge))))
            {
                Console.WriteLine($"-  {p.Name}  {p.color}  {p.size}");
            }
        }
    }
}