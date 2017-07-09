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
    // TODO: FR - Amend design as use of both Player->Computer subclassing and PlayerType enum seems redundant.
    public class Player
    {
        public PlayerType PlayerType{get;}
        public Name Name { get; }
        public uint Score { get; set; }

        public Player(Name name, PlayerType playerType)
        {
            Name = name;
            PlayerType = playerType;
            Score = Score;
        }
    }
}