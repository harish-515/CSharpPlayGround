using System;
using System.Collections.Generic;
using System.Linq;

namespace CSharpPlayGrond.DesignPatterns.SOLID_Design_Principles
{
    public interface IRelationshipBrowser
    {
        IEnumerable<string> FindAllChildrenOf(string name);
    }

    public enum Relationship
    {
        Parent,
        Child,
        Sibiling
    }

    public class Person
    {
        public string Name { get; set; }
    }

    //low-level objects
    public class Relationships : IRelationshipBrowser
    {
        // need to expose unwanted propeties to outside world for their consumtion.
        //without dependency inversion
        public List<(Person, Relationship, Person)> Relations { get; set; }

        private List<(Person, Relationship, Person)> relations = new List<(Person, Relationship, Person)>();

        public void AddParentAndChild(Person parent, Person child)
        {
            relations.Add((parent, Relationship.Parent, child));
            relations.Add((child, Relationship.Child, parent));
        }

        public IEnumerable<string> FindAllChildrenOf(string parent)
        {
            return relations.Where((x) => x.Item1.Name == parent && x.Item2 == Relationship.Parent)
                            .Select((x) => x.Item3.Name);
        }
    }

    // Research is a high level object
    public class Research // Dependency Inverson Demo
    {
        private string parent = "john";

        public Research(Relationships relationships)
        {
            // here we are directly accessing the public properties exposed by the relationship class
            // making this class depend on that class for relationships.
            Console.WriteLine($"Children of {parent} are {string.Join(",", relationships.Relations.Where((x) => x.Item1.Name == parent && x.Item2 == Relationship.Parent).Select((X) => X.Item3.Name))}");
        }

        public Research(IRelationshipBrowser relationshipBrowser)
        {
            // here we rely on the interface implementation rather than the concrete implementation of relations in relationship class
            Console.WriteLine($"Children of {parent} are {string.Join(",", relationshipBrowser.FindAllChildrenOf(parent))}");
        }

        private static void DoResearch()
        {
            var parent = new Person { Name = "john" };
            var child1 = new Person { Name = "chris" };
            var child2 = new Person { Name = "mary" };

            var relationships = new Relationships();
            relationships.AddParentAndChild(parent, child1);
            relationships.AddParentAndChild(parent, child2);

            Research rec = new Research(relationships);
        }
    }
}