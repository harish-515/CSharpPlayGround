using System;
using System.Collections.Generic;
using System.Linq;

namespace CSharpPlayGrond.DesignPatterns.Structural
{
    /// <summary>
    /// provides a simple easy to understand/user interface
    /// over a large and sophisticated body of code.
    /// </summary>

    class Facade // Fasade pattern
    {
    }

}

namespace Coding.Exercise
{
    public class Generator
    {
        private static readonly Random random = new Random();

        public List<int> Generate(int count)
        {
            return Enumerable.Range(0, count)
              .Select(_ => random.Next(1, 6))
              .ToList();
        }
    }

    public class Splitter
    {
        public List<List<int>> Split(List<List<int>> array)
        {
            var result = new List<List<int>>();

            var rowCount = array.Count;
            var colCount = array[0].Count;

            // get the rows
            for (int r = 0; r < rowCount; ++r)
            {
                var theRow = new List<int>();
                for (int c = 0; c < colCount; ++c)
                    theRow.Add(array[r][c]);
                result.Add(theRow);
            }

            // get the columns
            for (int c = 0; c < colCount; ++c)
            {
                var theCol = new List<int>();
                for (int r = 0; r < rowCount; ++r)
                    theCol.Add(array[r][c]);
                result.Add(theCol);
            }

            // now the diagonals
            var diag1 = new List<int>();
            var diag2 = new List<int>();
            for (int c = 0; c < colCount; ++c)
            {
                for (int r = 0; r < rowCount; ++r)
                {
                    if (c == r)
                        diag1.Add(array[r][c]);
                    var r2 = rowCount - r - 1;
                    if (c == r2)
                        diag2.Add(array[r][c]);
                }
            }

            result.Add(diag1);
            result.Add(diag2);

            return result;
        }
    }

    public class Verifier
    {
        public bool Verify(List<List<int>> array)
        {
            if (!array.Any()) return false;

            var expected = array.First().Sum();

            return array.All(t => t.Sum() == expected);
        }
    }

    public class MagicSquareGenerator
    {
        public List<List<int>> Generate(int size)
        {
            // todo
            var splitter = new Splitter();
            var generator = new Generator();
            var verifier = new Verifier(); 
            var possibleSquare = new List<List<int>>();
            var isdone = false;

            while (!isdone)
            {
                possibleSquare.Clear();  
                for (int i = 0; i < size; i++)
                {
                    possibleSquare.Add(generator.Generate(size));
                    isdone = verifier.Verify(splitter.Split(possibleSquare));  
                }
            }

            return possibleSquare; 
        }
    }


}