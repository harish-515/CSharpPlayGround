using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CSharpPlayGrond.DesignPatterns.Structural
{
    #region " Geometric Shapes"
    
    public class GraphicObject
    {
        public virtual string Name { get; set; } = "Group";

        public string Color;

        public void Print(StringBuilder sb, int depth)
        {
            sb.Append(new string('*', depth))
                .Append(String.IsNullOrEmpty(Color) ? string.Empty : Color)
                .Append(Name);
            foreach(var child in Children)
            {
                child.Print(sb, depth + 1);
            }
        }


        private Lazy<List<GraphicObject>> children = new Lazy<List<GraphicObject>>();
        public List<GraphicObject> Children => this.children.Value;

        public override string ToString()
        {
            var sb = new StringBuilder();
            Print(sb, 0);
            return sb.ToString();
        }

    }
    
    public class Circle : GraphicObject
    {
        public override string Name => "Circle";
    }

    public class Square : GraphicObject
    {
        public override string Name => "Square";
    }

    public static class GeometricShapesDemo
    {
        public static void Demo()
        {
            var drawing = new GraphicObject() { Name = "My Drawing" };
            drawing.Children.Add(new Square { Color = "red" });
            drawing.Children.Add(new Circle { Color = "yellow" });

            var childGroup = new GraphicObject() { Name = "My Child Group" };
            childGroup.Children.Add(new Square { Color = "blue" });
            childGroup.Children.Add(new Circle { Color = "blue" });

            drawing.Children.Add(childGroup);

            Console.WriteLine(drawing);
        }
    }


    #endregion

    #region " Neural Networks "

    public class S1
    {
        public static void foo() { }
    }
    
    public static class NeuronExtensionMethods
    {
        public static void ConnectTo(this IEnumerable<Neuron> self,
            IEnumerable<Neuron> other)
        {
            foreach(var from in self)
            {
                foreach(var to in other)
                {
                    from.Out.Add(to);
                    to.In.Add(from);
                }
            }
        }
    }

    public class Neuron : IEnumerable<Neuron>
    {
        public float Value;
        public List<Neuron> In,Out;

        public IEnumerator<Neuron> GetEnumerator()
        {
            yield return this;
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
    }
    
    public class NeuronLayer : Collection<Neuron>
    {
    
    }

    public static class NeuronDemo
    {
        public static void Demo()
        {
            var neuron1 = new Neuron();
            var neuron2 = new Neuron();
            neuron1.ConnectTo(neuron2);


            var layer1 = new NeuronLayer();
            layer1.ConnectTo(neuron2);
        }
    }

    #endregion

    #region " Exercise "


  namespace Coding.Exercise
    {
        public interface IValueContainer : IEnumerable<int>
        {

        }

        public class SingleValue : IValueContainer
        {
            public int Value;

            public IEnumerator<int> GetEnumerator()
            {
                yield return Value;
            }

            IEnumerator IEnumerable.GetEnumerator()
            {
                return GetEnumerator();
            }
        }

        public class ManyValues : List<int>, IValueContainer
        {

        }

        public static class ExtensionMethods
        {
            public static int Sum(this List<IValueContainer> containers)
            {
                int result = 0;
                foreach (var c in containers)
                    foreach (var i in c)
                        result += i;
                return result;
            }
        }
    }



    #endregion
}
