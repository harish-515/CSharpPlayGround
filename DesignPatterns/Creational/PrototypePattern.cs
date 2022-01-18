using System.IO;
using System.Xml.Serialization;

namespace CSharpPlayGrond.DesignPatterns.Creational
{
    public class Point
    {
        public int X, Y;

        public Point()
        {
        }

        public Point(int x, int y)
        {
            X = x;
            Y = y;
        }
    }

    public class Line
    {
        public Point Start, End;

        public Line()
        {
        }

        public Line DeepCopy()
        {
            using (var ms = new MemoryStream())
            {
                var s = new XmlSerializer(typeof(Line));
                s.Serialize(ms, this);
                ms.Position = 0;
                return (Line)s.Deserialize(ms);
            }
        }
    }
}