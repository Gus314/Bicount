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
        private Round round;
        private Game game;
        private Player player1;
        private Player player2;
        private Vocabulary vocabulary;
        private Timer roundTimer;
        private Timer secondTimer;

        private void OnTimedEvent()
        {
            secondTimer.Stop();
            roundTimer.Stop();

            EditText currentWordText = (EditText)FindViewById(Resource.Id.currentWord);
            round.GuessLetters[PlayerNum.One] = currentWordText.Text;
            round.MakeComputerGuesses(); // TODO: FR - If no word exists, this may crash.

            SetContentView(Resource.Layout.RoundResultsPage);
            UpdateRoundResultsPage();
        }

        private void DispatchOnTimedEvent(Object source, System.Timers.ElapsedEventArgs e)
        {
            RunOnUiThread(OnTimedEvent);
        }

        private void UpdateEndGamePage()
        {
            TextView player1FinalScore = (TextView)FindViewById(Resource.Id.player1FinalScore);
            player1FinalScore.Text = player1.Name.Forename + " " + player1.Name.Surname + " scored " + player1.Score + " points."; 

            TextView player2FinalScore = (TextView)FindViewById(Resource.Id.player2FinalScore);
            player2FinalScore.Text = player2.Name.Forename + " " + player2.Name.Surname + " scored " + player2.Score + " points.";

            IEnumerable<Player> winners = game.DetermineWinners();
            TextView finalResultDisplay = (TextView)FindViewById(Resource.Id.finalResultDisplay);
            string finalResultText = "The game was won by:\n";
            finalResultDisplay.Text = finalResultText;

            Button endGameButton = (Button)FindViewById(Resource.Id.endGameButton);
            endGameButton.Click += OnClickEndGameButton;

            Button playAgainButton = (Button)FindViewById(Resource.Id.playAgainButton);
            playAgainButton.Click += OnClickPlayAgainButton;
        }

        private void OnClickEndGameButton(Object source, EventArgs args)
        {
            System.Environment.Exit(0);
        }

        private void OnClickPlayAgainButton(Object source, EventArgs args)
        {
            UpdateStartGamePage();
            SetContentView(Resource.Layout.StartGamePage);
        }

        private void UpdateStartGamePage()
        {
            Button startGameButton = (Button)FindViewById(Resource.Id.startGameButton);
            startGameButton.Click += OnClickStartGame;
        }

        private void OnClickStartGame(Object source, EventArgs args)
        {
            EditText forenameEntryText = (EditText)FindViewById(Resource.Id.forenameEntry);
            string forename = forenameEntryText.Text;

            EditText surnameEntryText = (EditText)FindViewById(Resource.Id.surnameEntry);
            string surname = surnameEntryText.Text;

            Name player1Name = new Name(forename, surname);

            SetContentView(Resource.Layout.RoundPage);
            SetupGame(player1Name);
            NextRound();
        }

        private void UpdateRoundResultsPage()
        {
            Dictionary<PlayerNum, uint> scores = round.DetermineScores();

            TextView player1ResultWordText = (TextView)FindViewById(Resource.Id.player1resultword);
            player1ResultWordText.Text = player1.Name.Forename + " " + player1.Name.Surname + " " + "had the word '" + round.GuessLetters[PlayerNum.One] + "'.";

            TextView player2ResultWordText = (TextView)FindViewById(Resource.Id.player2resultword);
            player2ResultWordText.Text = player2.Name.Forename + " " + player2.Name.Surname + " " + "had the word '" + round.GuessLetters[PlayerNum.Two] + "'.";

            TextView roundScoreDisplayText = (TextView)FindViewById(Resource.Id.roundscoredisplay);
            bool draw = (scores[PlayerNum.One] == scores[PlayerNum.Two]);
            if(draw)
            {
                roundScoreDisplayText.Text = "Both players scored " + scores[PlayerNum.One] +" points.";
            }
            else if(scores[PlayerNum.One] > scores[PlayerNum.Two])
            {
                roundScoreDisplayText.Text = player1.Name.Forename + " " + player1.Name.Surname + " scored " + scores[PlayerNum.One] + " points.";
            }
            else // player two scored more points than player one.
            {
                roundScoreDisplayText.Text = player2.Name.Forename + " " + player2.Name.Surname + " scored " + scores[PlayerNum.Two] + " points.";
            }

            Button continueButton = (Button)FindViewById(Resource.Id.continueButton);
            continueButton.Click += OnClickContinueButton;
        }

        private void OnClickContinueButton(Object source, EventArgs args)
        {
            if(game.IsFinished())
            {
                SetContentView(Resource.Layout.EndGamePage);
            }
            else
            {
                SetContentView(Resource.Layout.RoundPage);
                NextRound();
            }
        }

        private void SetupGame(Name player1Name)
        {
            Dictionary<PlayerNum, Player> players = new Dictionary<PlayerNum, Player>();

            players.Clear();
            player1 = new Player(player1Name, PlayerType.Human);
            player2 = new Computer(new Name("Computer", "Player"), PlayerType.Computer, vocabulary);
            players.Add(PlayerNum.One, player1);
            players.Add(PlayerNum.Two, player2);

            game = new Game(players, vocabulary);
        }

        protected override void OnCreate(Bundle bundle)
        {
            base.OnCreate(bundle);

            using (var dictionaryStream = Assembly.GetExecutingAssembly().GetManifestResourceStream("Bicount.Resources.dictionary.txt"))
            {
                vocabulary = new Vocabulary(dictionaryStream);
            }

            SetContentView(Resource.Layout.StartGamePage);
            UpdateStartGamePage();
        }

        private void DispatchUpdateTimer(Object source, EventArgs args)
        {
            RunOnUiThread(UpdateTimer);
        }

        private void UpdateTimer()
        {
            secondsPassed++;
            TextView timeRemaining = (TextView)FindViewById(Resource.Id.timeRemaining);
            if (secondsPassed >= 0)
            {
                timeRemaining.Text = (60 - secondsPassed).ToString();
            }
            else
            {
                timeRemaining.Text = 0.ToString();
            }
        }

        // TODO: FR - Find a better way of doing this.
        private uint secondsPassed = 0;

        private void NextRound()
        {
            secondsPassed = 0;
            round = game.NextRound();

            TextView currentLetters = (TextView)FindViewById(Resource.Id.currentLetters);
            currentLetters.Text = round.Letters;

            const uint roundSeconds = 60;
            const uint roundMilliseconds = roundSeconds * 1000;
            roundTimer = new Timer(roundMilliseconds)
            {
                AutoReset = false
            };
            roundTimer.Elapsed += DispatchOnTimedEvent;

            secondTimer = new Timer(1000); // One second timer.
            secondTimer.Elapsed += DispatchUpdateTimer;

            roundTimer.Enabled = true;
            secondTimer.Enabled = true;
        }
    }
}

