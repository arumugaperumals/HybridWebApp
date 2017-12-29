using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace HybridApp1
{
    public partial class MainPage : ContentPage
    {
        HybridWebView webView = null;
        public MainPage()
        {
            InitializeComponent();
            webView = new HybridWebView()
            {
                HorizontalOptions = LayoutOptions.Start,
                VerticalOptions = LayoutOptions.Start,
                Uri = "page1.html",
                WidthRequest = 100,
                HeightRequest = 40
            };
            Grid.SetRow(webView, 1);
            rootGrid.Children.Add(webView);
            webView.Clicked += WebView_Clicked;
            webView.SetBinding(HybridWebView.LabelProperty, new Binding() { Source = entry, Path = "Text", Mode = BindingMode.TwoWay });
        }

        private void WebView_Clicked(object sender, EventArgs args)
        {
            DisplayAlert("Message", "Click action is occured", "Ok");
        }

        private void Button_Clicked(object sender, EventArgs e)
        {
            webView.Label = "MyButton";
        }
    }
}
