using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UIKit;
using WebKit;
using Xamarin.Forms;

[assembly: Dependency(typeof(HybridApp1.iOS.HybridWebViewDependencyServices))]
namespace HybridApp1.iOS
{
    public class HybridWebViewDependencyServices : IHybridWebViewDependencyServices
    {
        public void SetLabel(object nativeObject, string value)
        {
			(nativeObject as WKWebView).EvaluateJavaScript("setLabel('" + value + "')",null);
        }
    }
}