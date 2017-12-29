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

namespace HybridApp1.Droid
{
    public class BaseUrl
    {
        public string Get()
        {
            return "file:///android_asset/";
        }
    }
}