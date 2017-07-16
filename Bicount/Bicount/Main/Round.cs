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
using static Bicount.Main.Enums;
using System.Timers;

namespace Bicount.Main
{
    public class Round
    {
        private const int numberOfLetters = 9;
        public static uint NumberOfLetters { get { return numberOfLetters; } }
        public Dictionary<PlayerNum, String> GuessLetters { get; }
        private readonly LetterGenerator LetterGenerator;
        public String Letters { get; private set; }
        private Vocabulary Vocabulary;
        private Dictionary<PlayerNum, Player> Players { get; }

        public Round(Vocabulary vocabulary, LetterGenerator letterGenerator, Dictionary<PlayerNum, Player> players)
        {
            Vocabulary = vocabulary;
            GuessLetters = new Dictionary<PlayerNum, string>();
            LetterGenerator = letterGenerator;
            DetermineLetters();
            Players = players;
        }

        private void DetermineLetters()
        {
            Letters = "";
            for (int i = 0; i < numberOfLetters; i++)
            {
                Letters += (LetterGenerator.Next());
            }
        }

        private uint PossiblePoints(PlayerNum playerNum)
        {
            String guessLetters = GuessLetters[playerNum];
            Word guess = Word.Construct(guessLetters);
            bool validGuess = Vocabulary.Contains(guess);
            return validGuess ? (uint) guess.Letters.Length : 0;
        }

        public Dictionary<PlayerNum, uint> DetermineScores()
        {
            Dictionary<PlayerNum, uint> scores = new Dictionary<PlayerNum, uint>();

            uint playerOneScore = PossiblePoints(PlayerNum.One);
            uint playerTwoScore = PossiblePoints(PlayerNum.Two);

            if (playerOneScore > playerTwoScore)
            {
                playerTwoScore = 0;
            }
            else if (playerTwoScore > playerOneScore)
            {
                playerOneScore = 0;
            }

            scores.Add(PlayerNum.One, playerOneScore);
            scores.Add(PlayerNum.Two, playerTwoScore);

            return scores;
        }

        public void MakeComputerGuesses()
        {
            foreach (PlayerNum playerNum in Enum.GetValues(typeof(PlayerNum)))
            {
                if (Players[playerNum] is Computer)
                {
                    GuessLetters[playerNum] = ((Computer)Players[playerNum]).DetermineGuess(Letters).Letters;
                }
            }
        }
    }
}