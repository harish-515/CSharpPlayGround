using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using System.Threading.Tasks;

namespace CSharpPlayGrond.DesignPatterns.Creational
{
    // Builder is a creational design pattern that lets you construct complex objects step by step.
    // The pattern allows you to produce different types and representations of an object using the same construction code.
    
    class NoBuilderPattern
    {
        static void Demo()
        {
            var hello = "hello";

            var sb = new StringBuilder();
            sb.Append("<p>");
            sb.Append(hello); 
            sb.Append("</p>");

            Console.WriteLine(sb.ToString());

            var words = new[] { "Hello", "World" };
            sb.Clear();
            sb.Append("ul");
            foreach(string word in words)
            {
                sb.AppendFormat("<li>{0}</li>", word); 
            }
            sb.Append("</ul>");

            Console.WriteLine(sb.ToString()); 
        }
    }


    class HtmlElement
    {
        public string Name, Text;
        public List<HtmlElement> elements = new List<HtmlElement>();
        private const int indentSize = 2;

        public HtmlElement() { }

        public HtmlElement(string name,string text)
        {
            this.Name = name ?? throw new ArgumentNullException(nameof(name)) ;
            this.Text = text ?? throw new ArgumentNullException(nameof(text));
        } 

        private string ToStringImpl(int indent)
        {
            var sb = new StringBuilder();
            var i = new string(' ', indentSize * indent);
            sb.AppendLine($"{i}<{Name}>");

            if (!string.IsNullOrWhiteSpace(Text))
            {
                sb.Append(new string(' ', indentSize * (indent+1)));
                sb.AppendLine(Text);
            }

            foreach(var e in elements)
            {
                sb.Append(e.ToStringImpl(indent + 1));
            }
            sb.AppendLine($"{i}</{Name}>");
            return sb.ToString(); 

        }

        public override string ToString()
        {
            return this.ToStringImpl(0) ;
        }
    }

    class HTMLBuilder
    {
        private readonly string rootname;
        HtmlElement root = new HtmlElement();
        
        public HTMLBuilder(string rootname)
        {
            this.rootname = rootname;
            this.root.Name = rootname;  
        } 

        public void AddChild(string name,string text)
        {
            var childele = new HtmlElement(name, text);
            root.elements.Add(childele); 
        }


        public override string ToString()
        {
            return this.root.ToString();
        }

        public void ClearElements()
        {
            root = new HtmlElement { Name = rootname };
        }
    }

    public class BuilderDemo
    {
        public static void Demo()
        {
            var htmlBuilder = new HTMLBuilder("ul");
            htmlBuilder.AddChild("li", "hello");  
            htmlBuilder.AddChild("li", "world");
            Console.WriteLine(htmlBuilder.ToString());  

        }
    }


}

namespace Coding.Exercise
{
    public abstract class CodeElement
    {
        public string name;
        public abstract string ToStringIndent(int indent);
        public override string ToString()
        {
            return this.ToStringIndent(0);
        }
    }

    public class FieldElement : CodeElement
    {
        private string datatype;
        public FieldElement(string name, string datatype)
        {
            this.name = name;
            this.datatype = datatype; 
        }

        public override string ToStringIndent(int indent)
        {
            var sb = new StringBuilder();
            sb.Append(new string(' ', 2 * indent));
            sb.AppendLine($"public {datatype} {name};");
            return sb.ToString();
        }

        public class ClassElement : CodeElement
        {
            public ClassElement()
            {

            } 
            public ClassElement(string name)
            {
                this.name = name;
            }

            public List<FieldElement> Fields = new List<FieldElement>();
            public override string ToStringIndent(int indent)
            {
                var sb = new StringBuilder();
                sb.AppendLine($"public class {this.name}");
                sb.AppendLine("{");

                foreach (FieldElement f in Fields)
                {
                    sb.Append(f.ToStringIndent(indent + 1));
                }

                sb.AppendLine("}");

                return sb.ToString();
            }
        }


        public class CodeBuilder
        {
            private readonly string classname;
            ClassElement cls = new ClassElement();

            public CodeBuilder(string name)
            {
                this.classname = name;
                this.cls.name = name;
            }

            public CodeBuilder AddFields(string name, string datatype)
            {
                cls.Fields.Add(new FieldElement(name, datatype));
                return this;
            }


            public override string ToString()
            {
                return this.cls.ToString();
            }

        }

        public static void Demo()
        {
            //CodeBuilder codeBuilder = new CodeBuilder("Person");
            //codeBuilder.AddFields("Name", "string");  
            //codeBuilder.AddFields("Age", "int");
            var cb = new CodeBuilder("Person").AddFields("Name", "string").AddFields("Age", "int");
            Console.WriteLine(cb);
        }
    }
}
