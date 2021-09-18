using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CSharpPlayGrond
{
    public class WordCloudData
    {
        private Dictionary<string, int> _wordsToCounts = new Dictionary<string, int>();

        public IDictionary<string, int> WordsToCounts
        {
            get { return _wordsToCounts; }
        }

        public WordCloudData(string inputString)
        {
            PopulateWordsToCounts(inputString);
        }

        private List<string> SplitWords(string input)
        {
            List<string> words = new List<string>();
            int startIndex = 0;
            int wordLen = 0;

            for (int i = 0; i < input.Length; i++)
            {

                if (char.IsLetter(input[i]))
                {

                    if (wordLen == 0)
                    {
                        startIndex = i;
                    }
                    wordLen++;
                }
                else
                {

                    words.Add(input.Substring(startIndex, wordLen));
                    wordLen = 0;
                }
            }

            words.Add(input.Substring(startIndex, wordLen));
            return words;
        }

        private void PopulateWordsToCounts(string inputString)
        {
            // Count the frequency of each word
            List<string> words = SplitWords(inputString);
            foreach (string word in words)
            {
                string itemkey = _wordsToCounts.Keys.Where(i => string.Equals(i, word, StringComparison.OrdinalIgnoreCase)).FirstOrDefault();
                if (string.IsNullOrEmpty(itemkey))
                {
                    _wordsToCounts.Add(word, 1);
                }
                else
                {
                    _wordsToCounts[itemkey]++;
                }
            }


        }

        public void PrintWordCount()
        {
            foreach(KeyValuePair<string,int> kp in _wordsToCounts)
            {
                Console.WriteLine($"{kp.Key} -- {kp.Value}"); 
            }
        }
    }
}
