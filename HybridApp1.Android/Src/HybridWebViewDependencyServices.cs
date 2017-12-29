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
using HybridApp1;
using UIWebView = Android.Webkit.WebView;
using Xamarin.Forms;

[assembly: Dependency(typeof(HybridApp1.Droid.HybridWebViewDependencyServices))]
namespace HybridApp1.Droid
{
    public class HybridWebViewDependencyServices : IHybridWebViewDependencyServices
    {
        public void SetLabel(object nativeObject, string value)
        {
            string script = "setLabel('" + value + "');";
            (nativeObject as UIWebView).EvaluateJavascript(script, null);
        }
    }
}