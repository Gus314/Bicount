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
    public class LetterGenerator
    {
        private readonly Char[] vowels = { 'A', 'E', 'I', 'O', 'U' };
        private readonly Char[] consonants = { 'B', 'D', 'F', 'G', 'H', 'J', 'K', 'L', 'M', 'N', 'P', 'Q', 'R', 'S', 'T', 'V', 'W', 'X', 'Y', 'Z'};
        private readonly Random generator = new Random();

        // This should later take a language as a parameter.
        public LetterGenerator()
        {

        }

        public char NextVowel()
        {
            uint choice = (uint)generator.Next(vowels.Length);
            return vowels[choice];
        }

        public char NextConsonant()
        {
            uint choice = (uint)generator.Next(consonants.Length);
            return consonants[choice];
        }
        public char Next()
        {
            bool vowel = (0 == generator.Next(2));
            return vowel ? NextVowel() : NextConsonant();
        }
    }
}