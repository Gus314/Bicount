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
using System.IO;

namespace Bicount.Main
{
    public class Vocabulary
    {
        private readonly ICollection<Word> words = new List<Word>();

        // TODO: FR - Change this resource location.
        public Vocabulary(System.IO.Stream fileStream)
        {
            using (StreamReader reader = new StreamReader(fileStream))
            {                
                while (!reader.EndOfStream)
                {
                    string line = reader.ReadLine();
                    try
                    {
                        words.Add(Word.Construct(line));
                    }
                    catch (InvalidWordException)
                    {
                        Console.WriteLine("Unable to add word: '" + line + "'.");
                    }
                }
            }

        }

        // TODO: FR - Fix Contains function for Words.
        public Boolean Contains(Word word)
        {
            return words.Contains(word);
        }
    }
}