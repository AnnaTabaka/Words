using System;
using System.Linq;
using System.Windows.Forms;

namespace Words
{
    public partial class Window : Form
    {
        #region Ctors

        public Window()
        {
            InitializeComponent();
            //Example sentence
            rtxSoruce.Text = @"Example: Time times time times time squared equals time times time times time times time.";
        }

        #endregion Ctors
        #region Private methods

        //Write list of stem of words, words with the same stem in brackets and its occurrence to 'output'
        private void StemmedWords()
        {
            var wordCount = new WordCount();
            wordCount.CountStemmedWords(rtxSoruce.Text);

            var text = string.Empty;
            foreach (var word in wordCount.CountedWords)
            {
                var stemmedWords = string.Join(", ", wordCount.StemmedWords[word.Key].Select(n => n.ToString()));
                text = string.Format("{0}{1} ({3}) - {2}\n", text, word.Key, wordCount.CountedWords[word.Key],
                    stemmedWords);
            }
            rtxResult.Text = text;
        }

        //Write list of words and its occurrence to 'output'
        private void Words()
        {
            var wordCount = WordCount.CountWords(rtxSoruce.Text);
            var text = wordCount.Aggregate(string.Empty,
                (t, word) => string.Format("{0}{1} - {2}\n", t, word.Key, wordCount[word.Key]));
            rtxResult.Text = text;
        }

        #endregion Private methods
        #region Event handlers

        //Clear 'input'
        private void butClear_Click(object sender, EventArgs e)
        {
            rtxSoruce.Clear();
        }

        //Clear 'input' and 'output'
        private void butClearAll_Click(object sender, EventArgs e)
        {
            rtxSoruce.Clear();
            rtxResult.Clear();
        }

        //Write list of words and its occurrence to 'output'
        private void butCountBasic_Click(object sender, EventArgs e)
        {
            Words();
        }

        //Write list of stem of words, words with the same stem in brackets and its occurrence to 'output'
        private void butCountStem_Click(object sender, EventArgs e)
        {
            StemmedWords();
        }

        #endregion Event handlers
    }
}
