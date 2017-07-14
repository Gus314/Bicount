using Android.App;
using Android.Widget;
using Android.OS;
using Bicount.Main;
using System.Collections.Generic;
using static Bicount.Main.Enums;
using System.Reflection;
using System;
using System.IO;
using System.Timers;

namespace Bicount
{
    [Activity(Label = "Bicount", MainLauncher = true, Icon = "@drawable/icon")]
    public class MainActivity : Activity
    {
        private void Choons(Object source, EventArgs eventArgs)
        {
            Console.WriteLine("Cool choons.");
        }

        private Round round;

        private void OnTimedEvent(Object source, System.Timers.ElapsedEventArgs e)
        {
            Console.WriteLine("Timer end.");
            round.MakeComputerGuesses();
            TextView playerGuessText = (TextView)FindViewById(Resource.Id.currentWord);
            round.GuessLetters[PlayerNum.One] = playerGuessText.Text;
            Dictionary<PlayerNum, uint> scores = round.DetermineScores();
            string results = "";
            foreach (PlayerNum playerNum in Enum.GetValues(typeof(PlayerNum)))
            {
                results += (playerNum + " scored " + scores[playerNum] + " points.");
                results += ("Their word was : " + round.GuessLetters[playerNum] + ".");
            }

            Console.WriteLine(results);

            new AlertDialog.Builder(this).SetMessage(results).SetTitle("Round Over.").Show();
        }

        protected override void OnCreate(Bundle bundle)
        {
            base.OnCreate(bundle);

            SetContentView(Resource.Layout.RoundPage);

            // TODO: FR - Change this to use the UI.
            Dictionary<PlayerNum, Player> players = new Dictionary<PlayerNum, Player>();

            Vocabulary vocabulary = null;
            using (var dictionaryStream = Assembly.GetExecutingAssembly().GetManifestResourceStream("Bicount.Resources.dictionary.txt"))
            {
                vocabulary = new Vocabulary(dictionaryStream);
            }
            var player1 = new Computer(new Name("James", "Macdonald"), PlayerType.Computer, vocabulary);
            var player2 = new Computer(new Name("John", "Smith"), PlayerType.Computer, vocabulary);
            players.Add(PlayerNum.One, player1);
            players.Add(PlayerNum.Two, player2);

            Game game = new Game(players, vocabulary);
            round = game.NextRound();
            
            TextView currentLetters = (TextView) FindViewById(Resource.Id.currentLetters);
            currentLetters.Text = round.Letters;


            const uint roundSeconds = 60;
            const uint roundMilliseconds = roundSeconds * 1000;
            Timer timer = new Timer(roundMilliseconds)
            {
                AutoReset = false
            };
            timer.Elapsed += OnTimedEvent;
            Console.WriteLine("timer start.");
            timer.Enabled = true;


            //Button button = (Button) FindViewById(Resource.Id.my_button);
            //button.Click += Choons;

            Console.WriteLine("startoo");
            // game.PlayGame();
        }
    }
}

