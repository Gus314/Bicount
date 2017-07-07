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
            Player player1 = new Player(new Name("James", "Macdonald"), PlayerType.Human);
            Player player2 = new Player(new Name("John", "Smith"), PlayerType.Computer);
            players.Add(PlayerNum.One, player1);
            players.Add(PlayerNum.Two, player2);

            using (var dictionaryStream = Assembly.GetExecutingAssembly().GetManifestResourceStream("Bicount.Resources.dictionary.txt"))
            {
                Vocabulary vocabulary = new Vocabulary(dictionaryStream);
                Game game = new Game(players, vocabulary);
                game.PlayGame();
            }
        }
    }
}

