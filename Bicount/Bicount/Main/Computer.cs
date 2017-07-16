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
    public class Computer : Player
    {
        private Vocabulary Vocabulary { get; }
        private Random Random { get; }

        public Computer(Name name, Enums.PlayerType playerType, Vocabulary vocabulary) : base(name, playerType)
        {
            Vocabulary = vocabulary;
            Random = new Random();
        }


        public Word DetermineGuess(String letters)
        {
            Word selectedWord = null;

            IList<uint> wordLengths = new List<uint>();
            for (uint i = 1;  i <= Round.NumberOfLetters; i++)
            {
                wordLengths.Add(i);
            }

            
            while(wordLengths.Count() > 0)
            {
                uint numberOfLettersIndex = (uint)Random.Next(wordLengths.Count());
                uint numberOfLetters = wordLengths[(int)numberOfLettersIndex];
                wordLengths.RemoveAt((int)numberOfLettersIndex);

                List<Word> wordsOfLength = Vocabulary.WordsOfLength(letters, numberOfLetters);
                if (wordsOfLength.Any())
                {
                    uint wordChoice = (uint)Random.Next(wordsOfLength.Count());
                    selectedWord = wordsOfLength[(int)wordChoice];
                }

                if(selectedWord != null)
                {
                    break;
                }
            }


            return selectedWord ?? Word.Construct("");
        }
    }
}