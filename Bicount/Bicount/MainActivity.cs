using Android.App;
using Android.Widget;
using Android.OS;
using Bicount.Main;
using System.Collections.Generic;
using static Bicount.Main.Enums;
using System.Reflection;

namespace Bicount
{
    [Activity(Label = "Bicount", MainLauncher = true, Icon = "@drawable/icon")]
    public class MainActivity : Activity
    {
        protected override void OnCreate(Bundle bundle)
        {
            base.OnCreate(bundle);

            // Set our view from the "main" layout resource
            // SetContentView (Resource.Layout.Main);
            // TODO: FR - Change this to use the UI.
            Dictionary<PlayerNum, Player> players = new Dictionary<PlayerNum, Player>();

            using (var dictionaryStream = Assembly.GetExecutingAssembly().GetManifestResourceStream("Bicount.Resources.dictionary.txt"))
            {
                Vocabulary vocabulary = new Vocabulary(dictionaryStream);

                var player1 = new Computer(new Name("James", "Macdonald"), PlayerType.Computer, vocabulary);
                var player2 = new Computer(new Name("John", "Smith"), PlayerType.Computer, vocabulary);
                players.Add(PlayerNum.One, player1);
                players.Add(PlayerNum.Two, player2);

                Game game = new Game(players, vocabulary);
                game.PlayGame();
            }
        }
    }
}

