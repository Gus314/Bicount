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
    public class Word
    {
        public String Letters { get; }
        private const int maxLetters = 30;

        private Word(String letters)
        {
            this.Letters = letters;
        }

        public static Word Construct(String letters)
        {
            // Ensure case is never an issue.
            letters = letters.ToUpper();

            if(IsValid(letters))
            {
                return new Word(letters);
            }
            else
            {
                throw new InvalidWordException("Invalid word: '" + letters + "'.");
            }
        }

        public static bool IsValid(String letters)
        {
            var invalidLetters = from letter in letters where letter < 'A' || letter > 'z' select letter;
            return (!invalidLetters.Any()) && letters.Length <= maxLetters;
        }

        public override int GetHashCode()
        {
            return Letters.GetHashCode();
        }

        public override bool Equals(object obj)
        {
            var other = obj as Word;

            if (other == null)
            {
                return false;
            }

            return this.Letters.Equals(other.Letters);
        }

        public bool IsContainedIn(string availableLetters)
        {
            List<Char> availableLettersList = availableLetters.ToList();

            foreach (char letter in Letters)
            {
                if(availableLettersList.Contains(letter))
                {
                    availableLettersList.Remove(letter);
                }
                else
                {
                    return false;
                }
            }

            return true;
        }
    }
}