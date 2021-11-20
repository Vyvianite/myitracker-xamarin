using System;
using Xamarin.Forms;
using Xamarin.Essentials;
using Xamarin.Forms.PlatformConfiguration.iOSSpecific;
using MyITracker.Common;

namespace MyITracker.Views
{
    public partial class EditTicketPage : ContentPage, IView
    {
        #region Constructor
        public EditTicketPage()
        {
            InitializeComponent();
            On<Xamarin.Forms.PlatformConfiguration.iOS>().SetUseSafeArea(true);

            #region ViewLogic
            #endregion
        }
        #endregion
        public IViewModel ViewModel
        {
            get { return (IViewModel)BindingContext; }
            set { BindingContext = value; }
        }

        //protected override void OnAppearing()
        //{
        //    base.OnAppearing();
        //    _ = ServerApi.MultipartPostLoadLists(); /* This should refresh the global lists when needed. */

        //}
    }
}

//public EditTicket()
//{
//    InitializeComponent();

//    On<iOS>().SetUseSafeArea(true);

//    BindingContext = VMLocator.MainTicketVM;

//    #region ViewLogic
//    //This does have some business logic in the view, but the way the propertychanged event bubbles up still decouples the vm. May want to fix this later however.
//    VMLocator.MainTicketVM.PropertyChanged += async (sender, args) =>
//    {
//        if (args.PropertyName.Equals("Nav") && VMLocator.MainTicketVM.Nav == "Back")
//        {
//            await Navigation.PopModalAsync();
//        }
//        if (args.PropertyName.Equals("Message"))
//        {
//            await DisplayAlert("Alert", VMLocator.MainTicketVM.Message, "OK");
//        }
//    };
//    #endregion
//}

