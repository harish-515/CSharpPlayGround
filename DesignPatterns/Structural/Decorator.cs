using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Text;

namespace CSharpPlayGrond.DesignPatterns.Structural.Decorator
{
    // Add additional functionality to an exisiting code
    // without modifing the exisitng code base.

    #region " Custom String Builder "

    public class CodeBuilder
    {
        private StringBuilder builder = new StringBuilder();

        public override string ToString()
        {
            return builder.ToString();
        }

        public void GetObjectData(SerializationInfo info, StreamingContext context)
        {
            ((ISerializable)builder).GetObjectData(info, context);
        }

        public int EnsureCapacity(int capacity)
        {
            return builder.EnsureCapacity(capacity);
        }

        public string ToString(int startIndex, int length)
        {
            return builder.ToString(startIndex, length);
        }

        public CodeBuilder Clear()
        {
            builder.Clear();
            return this;
        }

        public CodeBuilder Append(char value, int repeatCount)
        {
            builder.Append(value, repeatCount);
            return this;
        }

        public CodeBuilder Append(char[] value, int startIndex, int charCount)
        {
            builder.Append(value, startIndex, charCount);
            return this;
        }

        public CodeBuilder Append(string value)
        {
            builder.Append(value);
            return this;
        }

        public CodeBuilder Append(string value, int startIndex, int count)
        {
            builder.Append(value, startIndex, count);
            return this;
        }

        public CodeBuilder AppendLine()
        {
            builder.AppendLine();
            return this;
        }

        public CodeBuilder AppendLine(string value)
        {
            builder.AppendLine(value);
            return this;
        }

        public void CopyTo(int sourceIndex, char[] destination, int destinationIndex, int count)
        {
            builder.CopyTo(sourceIndex, destination, destinationIndex, count);
        }

        public CodeBuilder Insert(int index, string value, int count)
        {
            builder.Insert(index, value, count);
            return this;
        }

        public CodeBuilder Remove(int startIndex, int length)
        {
            builder.Remove(startIndex, length);
            return this;
        }

        public CodeBuilder Append(bool value)
        {
            builder.Append(value);
            return this;
        }

        public CodeBuilder Append(sbyte value)
        {
            builder.Append(value);
            return this;
        }

        public CodeBuilder Append(byte value)
        {
            builder.Append(value);
            return this;
        }

        public CodeBuilder Append(char value)
        {
            builder.Append(value);
            return this;
        }

        public CodeBuilder Append(short value)
        {
            builder.Append(value);
            return this;
        }

        public CodeBuilder Append(int value)
        {
            builder.Append(value);
            return this;
        }

        public CodeBuilder Append(long value)
        {
            builder.Append(value);
            return this;
        }

        public CodeBuilder Append(float value)
        {
            builder.Append(value);
            return this;
        }

        public CodeBuilder Append(double value)
        {
            builder.Append(value);
            return this;
        }

        public CodeBuilder Append(decimal value)
        {
            builder.Append(value);
            return this;
        }

        public CodeBuilder Append(ushort value)
        {
            builder.Append(value);
            return this;
        }

        public CodeBuilder Append(uint value)
        {
            builder.Append(value);
            return this;
        }

        public CodeBuilder Append(ulong value)
        {
            builder.Append(value);
            return this;
        }

        public CodeBuilder Append(object value)
        {
            builder.Append(value);
            return this;
        }

        public CodeBuilder Append(char[] value)
        {
            builder.Append(value);
            return this;
        }

        public CodeBuilder Insert(int index, string value)
        {
            builder.Insert(index, value);
            return this;
        }

        public CodeBuilder Insert(int index, bool value)
        {
            builder.Insert(index, value);
            return this;
        }

        public CodeBuilder Insert(int index, sbyte value)
        {
            builder.Insert(index, value);
            return this;
        }

        public CodeBuilder Insert(int index, byte value)
        {
            builder.Insert(index, value);
            return this;
        }

        public CodeBuilder Insert(int index, short value)
        {
            builder.Insert(index, value);
            return this;
        }

        public CodeBuilder Insert(int index, char value)
        {
            builder.Insert(index, value);
            return this;
        }

        public CodeBuilder Insert(int index, char[] value)
        {
            builder.Insert(index, value);
            return this;
        }

        public CodeBuilder Insert(int index, char[] value, int startIndex, int charCount)
        {
            builder.Insert(index, value, startIndex, charCount);
            return this;
        }

        public CodeBuilder Insert(int index, int value)
        {
            builder.Insert(index, value);
            return this;
        }

        public CodeBuilder Insert(int index, long value)
        {
            builder.Insert(index, value);
            return this;
        }

        public CodeBuilder Insert(int index, float value)
        {
            builder.Insert(index, value);
            return this;
        }

        public CodeBuilder Insert(int index, double value)
        {
            builder.Insert(index, value);
            return this;
        }

        public CodeBuilder Insert(int index, decimal value)
        {
            builder.Insert(index, value);
            return this;
        }

        public CodeBuilder Insert(int index, ushort value)
        {
            builder.Insert(index, value);
            return this;
        }

        public CodeBuilder Insert(int index, uint value)
        {
            builder.Insert(index, value);
            return this;
        }

        public CodeBuilder Insert(int index, ulong value)
        {
            builder.Insert(index, value);
            return this;
        }

        public CodeBuilder Insert(int index, object value)
        {
            builder.Insert(index, value);
            return this;
        }

        public CodeBuilder AppendFormat(string format, object arg0)
        {
            builder.AppendFormat(format, arg0);
            return this;
        }

        public CodeBuilder AppendFormat(string format, object arg0, object arg1)
        {
            builder.AppendFormat(format, arg0, arg1);
            return this;
        }

        public CodeBuilder AppendFormat(string format, object arg0, object arg1, object arg2)
        {
            builder.AppendFormat(format, arg0, arg1, arg2);
            return this;
        }

        public CodeBuilder AppendFormat(string format, params object[] args)
        {
            builder.AppendFormat(format, args);
            return this;
        }

        public CodeBuilder AppendFormat(IFormatProvider provider, string format, params object[] args)
        {
            builder.AppendFormat(provider, format, args);
            return this;
        }

        public CodeBuilder Replace(string oldValue, string newValue)
        {
            builder.Replace(oldValue, newValue);
            return this;
        }

        public bool Equals(CodeBuilder sb)
        {
            return builder.Equals(sb);
        }

        public CodeBuilder Replace(string oldValue, string newValue, int startIndex, int count)
        {
            builder.Replace(oldValue, newValue, startIndex, count);
            return this;
        }

        public CodeBuilder Replace(char oldChar, char newChar)
        {
            builder.Replace(oldChar, newChar);
            return this;
        }

        public CodeBuilder Replace(char oldChar, char newChar, int startIndex, int count)
        {
            builder.Replace(oldChar, newChar, startIndex, count);
            return this;
        }

        public int Capacity
        {
            get => builder.Capacity;
            set => builder.Capacity = value;
        }

        public int MaxCapacity => builder.MaxCapacity;

        public int Length
        {
            get => builder.Length;
            set => builder.Length = value;
        }

        public char this[int index]
        {
            get => builder[index];
            set => builder[index] = value;
        }
    }

    #endregion " Custom String Builder "

    #region " Adapter Decorator  "

    public class MyStringBuilder
    {
        private readonly StringBuilder sb = new StringBuilder();

        public static implicit operator MyStringBuilder(string s)
        {
            var msb = new MyStringBuilder();
            msb.sb.Append(s);
            return msb;
        }

        public static MyStringBuilder operator +(MyStringBuilder msb, string s)
        {
            msb.Append(s);
            return msb;
        }

        public override string ToString()
        {
            return sb.ToString();
        }

        public void GetObjectData(SerializationInfo info, StreamingContext context)
        {
            ((ISerializable)sb).GetObjectData(info, context);
        }

        public int EnsureCapacity(int capacity)
        {
            return sb.EnsureCapacity(capacity);
        }

        public string ToString(int startIndex, int length)
        {
            return sb.ToString(startIndex, length);
        }

        public StringBuilder Clear()
        {
            return sb.Clear();
        }

        public StringBuilder Append(char value, int repeatCount)
        {
            return sb.Append(value, repeatCount);
        }

        public StringBuilder Append(char[] value, int startIndex, int charCount)
        {
            return sb.Append(value, startIndex, charCount);
        }

        public StringBuilder Append(string value)
        {
            return sb.Append(value);
        }

        public StringBuilder Append(string value, int startIndex, int count)
        {
            return sb.Append(value, startIndex, count);
        }

        public StringBuilder AppendLine()
        {
            return sb.AppendLine();
        }

        public StringBuilder AppendLine(string value)
        {
            return sb.AppendLine(value);
        }

        public void CopyTo(int sourceIndex, char[] destination, int destinationIndex, int count)
        {
            sb.CopyTo(sourceIndex, destination, destinationIndex, count);
        }

        public StringBuilder Insert(int index, string value, int count)
        {
            return sb.Insert(index, value, count);
        }

        public StringBuilder Remove(int startIndex, int length)
        {
            return sb.Remove(startIndex, length);
        }

        public StringBuilder Append(bool value)
        {
            return sb.Append(value);
        }

        public StringBuilder Append(sbyte value)
        {
            return sb.Append(value);
        }

        public StringBuilder Append(byte value)
        {
            return sb.Append(value);
        }

        public StringBuilder Append(char value)
        {
            return sb.Append(value);
        }

        public StringBuilder Append(short value)
        {
            return sb.Append(value);
        }

        public StringBuilder Append(int value)
        {
            return sb.Append(value);
        }

        public StringBuilder Append(long value)
        {
            return sb.Append(value);
        }

        public StringBuilder Append(float value)
        {
            return sb.Append(value);
        }

        public StringBuilder Append(double value)
        {
            return sb.Append(value);
        }

        public StringBuilder Append(decimal value)
        {
            return sb.Append(value);
        }

        public StringBuilder Append(ushort value)
        {
            return sb.Append(value);
        }

        public StringBuilder Append(uint value)
        {
            return sb.Append(value);
        }

        public StringBuilder Append(ulong value)
        {
            return sb.Append(value);
        }

        public StringBuilder Append(object value)
        {
            return sb.Append(value);
        }

        public StringBuilder Append(char[] value)
        {
            return sb.Append(value);
        }

        public StringBuilder Insert(int index, string value)
        {
            return sb.Insert(index, value);
        }

        public StringBuilder Insert(int index, bool value)
        {
            return sb.Insert(index, value);
        }

        public StringBuilder Insert(int index, sbyte value)
        {
            return sb.Insert(index, value);
        }

        public StringBuilder Insert(int index, byte value)
        {
            return sb.Insert(index, value);
        }

        public StringBuilder Insert(int index, short value)
        {
            return sb.Insert(index, value);
        }

        public StringBuilder Insert(int index, char value)
        {
            return sb.Insert(index, value);
        }

        public StringBuilder Insert(int index, char[] value)
        {
            return sb.Insert(index, value);
        }

        public StringBuilder Insert(int index, char[] value, int startIndex, int charCount)
        {
            return sb.Insert(index, value, startIndex, charCount);
        }

        public StringBuilder Insert(int index, int value)
        {
            return sb.Insert(index, value);
        }

        public StringBuilder Insert(int index, long value)
        {
            return sb.Insert(index, value);
        }

        public StringBuilder Insert(int index, float value)
        {
            return sb.Insert(index, value);
        }

        public StringBuilder Insert(int index, double value)
        {
            return sb.Insert(index, value);
        }

        public StringBuilder Insert(int index, decimal value)
        {
            return sb.Insert(index, value);
        }

        public StringBuilder Insert(int index, ushort value)
        {
            return sb.Insert(index, value);
        }

        public StringBuilder Insert(int index, uint value)
        {
            return sb.Insert(index, value);
        }

        public StringBuilder Insert(int index, ulong value)
        {
            return sb.Insert(index, value);
        }

        public StringBuilder Insert(int index, object value)
        {
            return sb.Insert(index, value);
        }

        public StringBuilder AppendFormat(string format, object arg0)
        {
            return sb.AppendFormat(format, arg0);
        }

        public StringBuilder AppendFormat(string format, object arg0, object arg1)
        {
            return sb.AppendFormat(format, arg0, arg1);
        }

        public StringBuilder AppendFormat(string format, object arg0, object arg1, object arg2)
        {
            return sb.AppendFormat(format, arg0, arg1, arg2);
        }

        public StringBuilder AppendFormat(string format, params object[] args)
        {
            return sb.AppendFormat(format, args);
        }

        public StringBuilder AppendFormat(IFormatProvider provider, string format, object arg0)
        {
            return sb.AppendFormat(provider, format, arg0);
        }

        public StringBuilder AppendFormat(IFormatProvider provider, string format, object arg0,
          object arg1)
        {
            return sb.AppendFormat(provider, format, arg0, arg1);
        }

        public StringBuilder AppendFormat(IFormatProvider provider, string format, object arg0,
          object arg1, object arg2)
        {
            return sb.AppendFormat(provider, format, arg0, arg1, arg2);
        }

        public StringBuilder AppendFormat(IFormatProvider provider, string format, params object[] args)
        {
            return sb.AppendFormat(provider, format, args);
        }

        public StringBuilder Replace(string oldValue, string newValue)
        {
            return sb.Replace(oldValue, newValue);
        }

        public bool Equals(StringBuilder sb)
        {
            return this.sb.Equals(sb);
        }

        public StringBuilder Replace(string oldValue, string newValue, int startIndex, int count)
        {
            return sb.Replace(oldValue, newValue, startIndex, count);
        }

        public StringBuilder Replace(char oldChar, char newChar)
        {
            return sb.Replace(oldChar, newChar);
        }

        public StringBuilder Replace(char oldChar, char newChar, int startIndex, int count)
        {
            return sb.Replace(oldChar, newChar, startIndex, count);
        }

        public int Capacity
        {
            get => sb.Capacity;
            set => sb.Capacity = value;
        }

        public int MaxCapacity => sb.MaxCapacity;

        public int Length
        {
            get => sb.Length;
            set => sb.Length = value;
        }

        public char this[int index]
        {
            get => sb[index];
            set => sb[index] = value;
        }
    }

    #endregion " Adapter Decorator  "

    #region " Shape Decorator "

    public abstract class Shape
    {
        public virtual string AsString() => string.Empty;
    }

    public class Circle : Shape
    {
        public Circle(int radius)
        {
            Radius = radius;
        }

        public int Radius { get; set; }

        public override string AsString()
        {
            return $"Circle of radius {Radius}.";
        }
    }

    public class Square : Shape
    {
        public Square(int size)
        {
            Size = size;
        }

        public int Size { get; set; }

        public override string AsString()
        {
            return $"Square of size {Size}.";
        }
    }

    public class ColoredShape : Shape
    {
        public ColoredShape(Shape shape, string color)
        {
            Shape = shape;
            Color = color;
        }

        public Shape Shape { get; set; }

        public string Color { get; set; }

        public override string AsString()
        {
            return $"{Shape.AsString()} And of color {Color}.";
        }
    }

    public class TransparentShape : Shape
    {
        public TransparentShape(Shape shape, float transparent)
        {
            Shape = shape;
            Transparent = transparent;
        }

        public Shape Shape { get; set; }

        public float Transparent { get; set; }

        public override string AsString()
        {
            return $"{Shape.AsString()} And with {Transparent * 100} % transperency.";
        }
    }

    #region " Decorator With Policy "

    public abstract class ShapeDecorator : Shape
    {
        protected internal readonly List<Type> types = new List<Type>();
        protected internal Shape Shape;

        public ShapeDecorator(Shape shape)
        {
            this.Shape = shape;
            if (shape is ShapeDecorator sd)
                types.AddRange(sd.types);
        }
    }

    public abstract class ShapeDecorator<TSelf, TCyclePolicy> : ShapeDecorator
        where TCyclePolicy : ShapeDecoratorCyclePolicy, new()
    {
        protected readonly TCyclePolicy policy = new TCyclePolicy();

        protected ShapeDecorator(Shape shape) : base(shape)
        {
            if (policy.TypesAdditionAllowed(typeof(TSelf), types))
                types.Add(typeof(TSelf));
        }
    }

    public class ColoredShapreWithPolicy : ShapeDecorator
    {
        public ColoredShapreWithPolicy(Shape shape, string color) : base(shape)
        {
            Shape = shape;
            Color = color;
        }

        public string Color { get; set; }

        public override string AsString()
        {
            return $"{Shape.AsString()} And of color {Color}.";
        }
    }

    #endregion " Decorator With Policy "

    #region " Decorator Cycle Policies "

    public abstract class ShapeDecoratorCyclePolicy
    {
        // Checks is if we can allow the wrapping of the exisitng object in this decorator
        public abstract bool TypesAdditionAllowed(Type type, IList<Type> types);

        // Checks if the decorator need to be appied on the object
        public abstract bool ApplicationAllowed(Type type, IList<Type> types);
    }

    public class ThrowOnCyclePolicy : ShapeDecoratorCyclePolicy
    {
        private bool handler(Type type, IList<Type> alltypes)
        {
            if (alltypes.Contains(type))
                throw new InvalidOperationException(
                    $"Cycle Detected ! This object is alreday of type {type.FullName}");
            return true;
        }

        public override bool ApplicationAllowed(Type type, IList<Type> types)
        {
            return handler(type, types);
        }

        public override bool TypesAdditionAllowed(Type type, IList<Type> types)
        {
            return handler(type, types);
        }
    }

    #endregion " Decorator Cycle Policies "

    #endregion " Shape Decorator "

    public static class DecoratorDemo
    {
        public static void ShapeDecoratorDemo()
        {
            var sq = new Square(5);
            var c = new Circle(4);

            var red_sq = new ColoredShape(sq, "red");

            var trans_shape = new TransparentShape(red_sq, 0.03f);

            Console.WriteLine(trans_shape.AsString());

            Shape s = new Circle(5);
            Shape cs = new ColoredShape(s, "blue");
            Shape ts = new TransparentShape(cs, 0.01f);

            Console.WriteLine(ts.AsString());
        }
    }
}