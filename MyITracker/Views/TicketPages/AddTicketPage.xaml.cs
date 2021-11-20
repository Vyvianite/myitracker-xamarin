using System;
using Xamarin.Forms;
using Xamarin.Essentials;
using Xamarin.Forms.PlatformConfiguration.iOSSpecific;

namespace MyITracker.Views
{
    public partial class AddTicketPage : ContentPage, IView
    {
        #region Constructor
        public AddTicketPage()
        {
            InitializeComponent();
            On<Xamarin.Forms.PlatformConfiguration.iOS>().SetUseSafeArea(true);
        }
        #endregion
        public IViewModel ViewModel
        {
            get { return (IViewModel)BindingContext; }
            set { BindingContext = value; }
        }
    }
}
