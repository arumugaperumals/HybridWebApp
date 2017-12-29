using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;
using UIWebView = Windows.UI.Xaml.Controls.WebView;

[assembly: Dependency(typeof(HybridApp1.UWP.HybridWebViewDependencyServices))]
namespace HybridApp1.UWP
{
    public class HybridWebViewDependencyServices : IHybridWebViewDependencyServices
    {
        public void SetLabel(object nativeObject, string value)
        {
            (nativeObject as UIWebView).InvokeScriptAsync("setLabel", new string[] { value });
        }
    }
}
