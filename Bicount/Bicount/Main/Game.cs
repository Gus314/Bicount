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

namespace Bicount.Main
{
    public class Game
    {
        private Dictionary<PlayerNum, Player> Players { get; }
        private Vocabulary Vocabulary { get; }
        private const int numRounds = 3;
        private readonly LetterGenerator LetterGenerator;
        private int currentRound = 0;

        public Game(Dictionary<PlayerNum, Player> players, Vocabulary vocabulary)
        {
            Players = players;
            Vocabulary = vocabulary;
            LetterGenerator = new LetterGenerator();
        }

        public IEnumerable<Player> DetermineWinners()
        {
            // Allow for the possibility of a draw.
            uint maxScore = 0;
            foreach (Player player in Players.Values)
            {
                if (player.Score > maxScore)
                {
                    maxScore = player.Score;
                }
            }

            return (from player in Players.Values where player.Score == maxScore select player);      
        }

        public Round NextRound()
        {
           // TODO: FR - What if game is finished?
           currentRound++;
           return new Round(Vocabulary, LetterGenerator, Players);
        }

        public bool IsFinished()
        {
            // TODO: FR - Variable naming could be misleading here, as currentRound should actually be the last round played when this returns true.
            return (numRounds == currentRound);
        }
    }
}