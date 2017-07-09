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

        // TODO: FR - Improve AI.
        // TODO: FR - What if no words are available?
        public Word DetermineGuess(String letters)
        {
            Word selectedWord = null;
            const uint maxAttempts = 100;
            uint numAttempts = 0;
            do
            {
                uint numberLetters = (uint)Random.Next(1, 10);
                List<Word> wordsOfLength = Vocabulary.WordsOfLength(letters, numberLetters);
                if(wordsOfLength.Any())
                {
                    uint wordChoice = (uint)Random.Next(wordsOfLength.Count());
                    selectedWord = wordsOfLength[(int) wordChoice];
                }

                numAttempts++;

            } while ((selectedWord == null) && (numAttempts < maxAttempts));

            return selectedWord ?? Word.Construct("");
        }
    }
}