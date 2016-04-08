using System;
using System.Collections.Generic;
using System.Text;

namespace Words
{
    class WordCount
    {
        #region Fields

        public Dictionary<string, int> CountedWords;
        public Dictionary<string, HashSet<string>> StemmedWords;

        #endregion Fields
        #region Public methods

        //Create a dictionary (word, it's occurence) from input sentence
        public static Dictionary<string, int> CountWords(string sentence)
        {
            var countedWords = new Dictionary<string, int>();
            var words = SplitSentence(sentence);

            foreach (var word in words)
            {
                AddValue(countedWords, word);
            }

            return countedWords;
        }

        //Create a dictionary (word, it's occurence)
        //and a dictionary (stem of word, list of words with the same stem) from input sentence
        public void CountStemmedWords(string sentence)
        {
            CountedWords = new Dictionary<string, int>();
            StemmedWords = new Dictionary<string, HashSet<string>>();
            var words = SplitSentence(sentence);

            foreach (var word in words)
            {
                var stemmedWord = Stem(word);

                AddValue(CountedWords, stemmedWord);
                AddValue(StemmedWords, stemmedWord, word);
            }
        }

        #endregion Public methods
        #region Private methods

        //Create a list of words from input sentence
        private static IEnumerable<string> SplitSentence(string sentence)
        {
            sentence = sentence.ToLower();
            var words = new List<string>();

            var sb = new StringBuilder();
            foreach (var c in sentence)
            {
                if (char.IsLetterOrDigit(c) || c.Equals('\''))
                {
                    sb.Append(c);
                }
                else
                {
                    if (sb.Length > 0)
                    {
                       words.Add(sb.ToString());
                    }
                    sb = new StringBuilder();
                }
            }
            if (sb.Length > 0)
            {
                words.Add(sb.ToString());
            }

            return words;
        }

        //The Porter2 stemming algorithm, author of the class in Porter2.cs: Kamil Bartocha
        private static string Stem(string word)
        {
            var stemmer = new Porter2();
            return stemmer.stem(word);
        }

        //Add word and its occurrence to dictionary (word, it's occurence)
        private static void AddValue(Dictionary<string, int> countedWords, string word)
        {
            int count;
            if (countedWords.TryGetValue(word, out count))
            {
                countedWords[word] = count + 1;
            }
            else
            {
                countedWords.Add(word, 1);
            }
        }

        //Add stem of word and word with the same stem to dictionary (stem of word, list of words with the same stem)
        private static void AddValue(Dictionary<string, HashSet<String>> stemmedWords, string stemmedWord, string word)
        {
            HashSet<string> hashSet;
            if (stemmedWords.TryGetValue(stemmedWord, out hashSet))
            {
                hashSet.Add(word);
            }
            else
            {
                hashSet = new HashSet<string>{word};
                stemmedWords.Add(stemmedWord, hashSet);
            }
        }

        #endregion Private methods
    }
}
