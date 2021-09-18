using System;
using System.Collections.Generic;
using System.Linq;


namespace CSharpPlayGrond.DesignPatterns.Structural
{
    /// <summary>
    ///  Goal : Space Optimization 
    ///  Avoid redundancy when storing data 
    /// 
    /// </summary>
    class FlyWeight
    {
    }

    public class Sentence
    {
        List<string> _words;
        List<WordToken> _tokens;

        public Sentence(string plainText)
        {
            this._words = plainText.Split(' ').ToList();
            this._tokens = new List<WordToken>(); 
            foreach(var w in _words)
            {
                _tokens.Add(new WordToken());
            }  
        }

        public WordToken this[int index]
        {
            get
            {
                return this._tokens[index];
            }
        }

        public override string ToString()
        {
            var ws = new List<string>();
            for (var i = 0; i < _words.Count; i++)
            {
                var word = _words[i];
                ws.Add(_tokens[i].Capitalize ? word.ToUpper() : word);
            }
            return string.Join(" ", ws);
        }

        public class WordToken
        {
            public WordToken()
            {
            }

            public bool Capitalize;

            public override string ToString()
            {
                return Capitalize.ToString();
            }
        }
    }
}
