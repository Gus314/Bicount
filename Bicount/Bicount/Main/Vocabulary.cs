using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;

namespace Bicount.Main
{
    public class Vocabulary
    {
        private readonly ICollection<Word> words = new List<Word>();

        private Vocabulary()
        {
            words.Add(Word.Construct("badger"));
            words.Add(Word.Construct("horse"));
            words.Add(Word.Construct("cheese"));
        }

        // TODO: FR - Fix Contains function for Words.
        public Boolean Contains(Word word)
        {
            return words.Contains(word);
        }

        public static Vocabulary Load()
        {
            return new Vocabulary();
        }
    }
}