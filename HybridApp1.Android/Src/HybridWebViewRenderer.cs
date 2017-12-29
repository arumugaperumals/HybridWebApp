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
using HybridApp1.Droid;
using static Java.Util.ResourceBundle;
using Xamarin.Forms.Platform.Android;
using Xamarin.Forms;
using Android.Webkit;
using System.Reflection;
using UIWebView = Android.Webkit.WebView;

[assembly: ExportRenderer(typeof(HybridWebView), typeof(HybridWebViewRenderer))]
namespace HybridApp1.Droid
{
    public class HybridWebViewRenderer : ViewRenderer<HybridWebView, UIWebView>
    {
        const string JavaScriptFunction = "function notifyClicked(data){jsBridge.invokeAction(data);}";

        protected override void OnElementChanged(ElementChangedEventArgs<HybridWebView> e)
        {
            base.OnElementChanged(e);
            UIWebView webView = null;
            if (Control == null)
            {
                webView = new UIWebView(Forms.Context);
                webView.Settings.JavaScriptEnabled = true;
                SetNativeControl(webView);
            }
            if (e.OldElement != null)
            {
                Control.RemoveJavascriptInterface("jsBridge");
                HybridWebView hybridWebView = e.OldElement as HybridWebView;
            }
            if (e.NewElement != null)
            {
                SetNativeObject(typeof(HybridWebView), e.NewElement, webView);
                Control.AddJavascriptInterface(new JSBridge(this), "jsBridge");
                Control.LoadUrl(string.Format("file:///android_asset/{0}", Element.Uri));
                InjectJS(JavaScriptFunction);
            }
        }

        internal static void SetNativeObject(Type type, object obj, object nativeObject)
        {
            if (obj == null) return;
            PropertyInfo property = type.GetTypeInfo().GetDeclaredProperty("NativeObject");
            if (property != null)
                property.SetValue(obj, nativeObject);
        }

        void InjectJS(string script)
        {
            if (Control != null)
            {
                Control.LoadUrl(string.Format("javascript: {0}", script));
            }
        }
    }

    public class JSBridge : Java.Lang.Object
    {
        readonly WeakReference<HybridWebViewRenderer> hybridWebViewRenderer;

        public JSBridge(HybridWebViewRenderer hybridRenderer)
        {
            hybridWebViewRenderer = new WeakReference<HybridWebViewRenderer>(hybridRenderer);
        }

        [JavascriptInterface]
        [Java.Interop.Export("invokeAction")]
        public void InvokeAction(string data)
        {
            HybridWebViewRenderer hybridRenderer;

            if (hybridWebViewRenderer != null && hybridWebViewRenderer.TryGetTarget(out hybridRenderer))
            {
                hybridRenderer.Element.InvokeAction(data);
            }
        }
    }
}