using System;
using Xamarin.Forms;
using Xamarin.Essentials;
using Xamarin.Forms.PlatformConfiguration.iOSSpecific;
using MyITracker.Common;

namespace MyITracker.Views
{
    public partial class LoginPage : ContentPage, IView
    {
        #region Constructor
        public LoginPage()
        {
            InitializeComponent();

            #region ViewLogic
            #endregion
        }
        #endregion
        public IViewModel ViewModel
        {
            get { return (IViewModel)BindingContext; }
            set { BindingContext = value; }
        }

        private async void Handle_Clicked(object sender, EventArgs e)
        {
            await Browser.OpenAsync( new Uri("https://myitracker.com/"), BrowserLaunchMode.SystemPreferred);
        }
    }
}