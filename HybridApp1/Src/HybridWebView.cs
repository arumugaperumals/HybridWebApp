using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace HybridApp1
{
    /// <summary>
    /// Represents the method that will handle the <see cref=""/> event of a <see cref="HybridWebView"/>.  
    /// </summary>
    /// <param name="sender">The instance of <see cref="HybridWebView"/> that is the source of the event.</param>
    /// <param name="args">The <see cref="EventArgs"/> that contains the event data.</param>
    public delegate void ClickedEventHandler(object sender, EventArgs args);


    /// <summary>
    /// A visual element that is used to place web view on screen.
    /// </summary>
    public class HybridWebView : View
    {
        #region Internal Fields and Properties
        private object nativeObject;
        private object NativeObject
        {
            get { return nativeObject; }
            set { nativeObject = value; }
        }
        #endregion

        #region Bindable Properties
        /// <summary>
        /// Gets or Sets the URI.
        /// </summary>
        public string Uri
        {
            get { return (string)GetValue(UriProperty); }
            set { SetValue(UriProperty, value); }
        }
        /// <summary>
        /// Gets or Sets the label.
        /// </summary>
        public string Label
        {
            get { return (string)GetValue(LabelProperty); }
            set { SetValue(LabelProperty, value); }
        }
        #endregion

        #region Static Bindable properties
        public static readonly BindableProperty UriProperty = BindableProperty.Create(propertyName: "Uri", returnType: typeof(string), declaringType: typeof(HybridWebView), defaultValue: default(string), defaultBindingMode: BindingMode.Default);
        public static readonly BindableProperty LabelProperty = BindableProperty.Create(propertyName: "Label", returnType: typeof(string), declaringType: typeof(HybridWebView), defaultValue: default(string), defaultBindingMode: BindingMode.Default, propertyChanged: OnLabelPropertyChanged);
        #endregion

        #region Static Property Changed Event handlers
        /// <summary>
        /// Called on label property changed.
        /// </summary>
        /// <param name="bindable"></param>
        /// <param name="oldValue"></param>
        /// <param name="newValue"></param>
        private static void OnLabelPropertyChanged(BindableObject bindable, object oldValue, object newValue)
        {
            HybridWebView webView = bindable as HybridWebView;
            if (webView.NativeObject != null)
            {
                DependencyService.Get<IHybridWebViewDependencyServices>().SetLabel(webView.NativeObject, newValue as string);
            }
        }
        #endregion

        #region Constructor
        /// <summary>
        /// Initializes a new instance of <see cref="HybridWebView"/> instance.
        /// </summary>
        public HybridWebView()
        {
            Label = "Button1";
        }
        #endregion

        #region Events
        /// <summary>
        /// Occurs when the view is clicked. 
        /// </summary>
        public event ClickedEventHandler Clicked;
        #endregion

        #region Internal Implementation
        /// <summary>
        /// Raises the <see cref="Clicked"/>  event
        /// </summary>
        /// <param name="sender">The instance of <see cref="HybridWebView"/> which is the source of the event. </param>
        /// <param name="e">The <see cref="EventArgs"/> that contains the event data.</param>
        internal void OnClicked(object sender, EventArgs e)
        {
            Clicked?.Invoke(this, e);
        }
        /// <summary>
        /// Invokes an action of specified data.
        /// </summary>
        /// <param name="data"></param>
        public void InvokeAction(string data)
        {
            if (data.Equals("clicked"))
            {
                OnClicked(this, new EventArgs());
            }
        }
        #endregion
    }
}
