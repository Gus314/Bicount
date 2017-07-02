﻿using System;
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
        private const int maxLetters = 9;

        private Word(String letters)
        {
            this.Letters = letters;
        }

        public static Word Construct(String letters)
        {
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
    }
}