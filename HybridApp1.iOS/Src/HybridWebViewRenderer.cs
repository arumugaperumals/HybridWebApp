using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;


using HybridApp1;

using Xamarin.Forms;
using System.Reflection;
using HybridApp1.iOS;
using Xamarin.Forms.Platform.iOS;
using WebKit;
using Foundation;
using System.IO;

[assembly: ExportRenderer(typeof(HybridWebView), typeof(HybridWebViewRenderer))]
namespace HybridApp1.iOS
{
	public class HybridWebViewRenderer : ViewRenderer<HybridWebView, WKWebView>, IWKScriptMessageHandler
	{
const string JavaScriptFunction = "function notifyClicked(data){window.webkit.messageHandlers.invokeAction.postMessage(data);}";
		WKUserContentController userController;

		protected override void OnElementChanged(ElementChangedEventArgs<HybridWebView> e)
		{
			base.OnElementChanged(e);
			WKWebView webView = null; 
			if (Control == null)
			{
				userController = new WKUserContentController();
				var script = new WKUserScript(new NSString(JavaScriptFunction), WKUserScriptInjectionTime.AtDocumentEnd, false);
				userController.AddUserScript(script);
				userController.AddScriptMessageHandler(this, "invokeAction");

				var config = new WKWebViewConfiguration { UserContentController = userController };
				webView = new WKWebView(Frame, config);
				SetNativeControl(webView);
			}
			if (e.OldElement != null)
			{
				userController.RemoveAllUserScripts();
				userController.RemoveScriptMessageHandler("invokeAction");
				var hybridWebView = e.OldElement as HybridWebView;
			//	hybridWebView.Cleanup();
			}
			if (e.NewElement != null)
			{
				SetNativeObject(typeof(HybridWebView), e.NewElement, webView);
				string fileName = Path.Combine(NSBundle.MainBundle.BundlePath, string.Format("Content/{0}", Element.Uri));
				Control.LoadRequest(new NSUrlRequest(new NSUrl(fileName, false)));
			}
		}

internal static void SetNativeObject(Type type, object obj, object nativeObject)
{
	if (obj == null) return;
	PropertyInfo property = type.GetTypeInfo().GetDeclaredProperty("NativeObject");
			if (property != null)
				property.SetValue(obj, nativeObject);
		}

		public void DidReceiveScriptMessage(WKUserContentController userContentController, WKScriptMessage message)
		{
			Element.InvokeAction(message.Body.ToString());
		}
	}
}
