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
    public class Name // TODO: FR - Add validation.
    {
        public String Forename { get; }
        public String Surname { get; }

        public Name(String forename, String surname)
        {
            Forename = forename;
            Surname = surname;
        }
    }
}