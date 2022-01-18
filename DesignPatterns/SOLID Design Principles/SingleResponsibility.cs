using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;

namespace CSharpPlayGrond.DesignPatterns.SOLID_Design_Principles
{
    public class Journal
    {
        private readonly List<String> enteries = new List<string>();

        private static int count = 0;

        public int AddEntry(String text)
        {
            enteries.Add($"{++count}: {text}");
            return count;
        }

        public void RemoveEntery(int index)
        {
            enteries.RemoveAt(index);
        }

        public override string ToString()
        {
            return String.Join(Environment.NewLine, enteries);
        }

        //// Persisitance responsibility added here
        //public void save(String filename)
        //{
        //    File.WriteAllText(filename, ToString());
        //}

        //public void load(string filename)
        //{
        //}
    }

    public class Persisitance
    {
        public void SaveToFile(Journal j, string filename, bool overwrite = false)
        {
            if (overwrite || !File.Exists(filename))
            {
                File.WriteAllText(filename, j.ToString());
            }
        }
    }

    internal static class SingleResponsibilityDemo
    {
        private static void Demo()
        {
            var j = new Journal();
            j.AddEntry("i cried today");
            j.AddEntry("i ate a bug");

            Console.WriteLine(j);

            var p = new Persisitance();
            var filename = @"C:\temp\journal.txt";
            p.SaveToFile(j, filename);

            Process.Start(filename);
        }
    }
}