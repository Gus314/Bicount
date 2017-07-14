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

        public Game(Dictionary<PlayerNum, Player> players, Vocabulary vocabulary)
        {
            Players = players;
            Vocabulary = vocabulary;
            LetterGenerator = new LetterGenerator();
        }

        private Player DetermineWinner()
        {
            // TODO: FR - What about a draw?
            Player winner = Players.Values.First();

            foreach(Player player in Players.Values)
            {
                if (player.Score > winner.Score)
                {
                    winner = player;
                }
            }

            return winner;
        }

        public Round NextRound()
        {
           return new Round(Vocabulary, LetterGenerator, Players);
        }

        public void PlayGame()
        {
           /* Console.WriteLine("Starting a new game.");


            for(uint i = 0; i < numRounds; i++)
            {
                var round = 
                round.Play();

                Dictionary<PlayerNum, uint> scores = round.DetermineScores();

                foreach(PlayerNum playerNum in Enum.GetValues(typeof(PlayerNum)))
                {
                    Players[playerNum].Score += scores[playerNum];
                    Console.WriteLine(Players[playerNum].Name.Forename + " scored " + scores[playerNum] + " points.");
                    Console.WriteLine("Their word was : " + round.GuessLetters[playerNum] + ".");
                }
            }

            var winner = DetermineWinner();
            Console.WriteLine("The winner was: " + winner.Name.Forename + " " + winner.Name.Surname + ".");*/
        }
    }
}