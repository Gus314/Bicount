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
            Console.WriteLine("Loading vocabulary.");
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
            Console.WriteLine("Finished loading vocabulary.");
        }

        public Boolean Contains(Word word)
        {
            return words.Contains(word);
        }

        public List<Word> WordsOfLength(string letters, uint length)
        {
            var lengthWords = from w in words where w.Letters.Count() == length select w;
            var anagrams = from w in lengthWords where w.IsContainedIn(letters) select w;
            return anagrams.ToList();
        }
    }
}