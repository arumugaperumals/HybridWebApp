using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.Reflection;
using Xamarin.Forms.Platform.UWP;
using HybridApp1;
using Windows.UI.Xaml.Controls;
using HybridApp1.UWP;

[assembly: ExportRenderer(typeof(HybridWebView), typeof(HybridWebViewRenderer))]
namespace HybridApp1.UWP
{
    public class HybridWebViewRenderer : ViewRenderer<HybridWebView, Windows.UI.Xaml.Controls.WebView>
    {
        private Windows.UI.Xaml.Controls.WebView webView;
        const string JavaScriptFunction = "function notifyClicked(data){window.external.notify(data);}";

        protected override void OnElementChanged(ElementChangedEventArgs<HybridWebView> e)
        {
            base.OnElementChanged(e);

            if (Control == null)
            {
                webView = new Windows.UI.Xaml.Controls.WebView();
                SetNativeControl(webView);
            }
            if (e.OldElement != null)
            {
                Control.NavigationCompleted -= OnWebViewNavigationCompleted;
                Control.ScriptNotify -= OnWebViewScriptNotify;
            }
            if (e.NewElement != null)
            {
                SetNativeObject(typeof(HybridWebView), e.NewElement, webView);
                Control.NavigationCompleted += OnWebViewNavigationCompleted;
                Control.ScriptNotify += OnWebViewScriptNotify;
                Control.Source = new Uri(string.Format("ms-appx-web:///{0}", Element.Uri));
            }
        }
        internal static void SetNativeObject(Type type, object obj, object nativeObject)
        {
            if (obj == null) return;
            PropertyInfo property = type.GetTypeInfo().GetDeclaredProperty("NativeObject");
            if (property != null)
                property.SetValue(obj, nativeObject);
        }
        async void OnWebViewNavigationCompleted(Windows.UI.Xaml.Controls.WebView sender, WebViewNavigationCompletedEventArgs args)
        {
            if (args.IsSuccess)
            {
                // Inject JS script
                await Control.InvokeScriptAsync("eval", new[] { JavaScriptFunction });
            }
        }

        void OnWebViewScriptNotify(object sender, NotifyEventArgs e)
        {
            Element.InvokeAction(e.Value);
        }
    }
}
