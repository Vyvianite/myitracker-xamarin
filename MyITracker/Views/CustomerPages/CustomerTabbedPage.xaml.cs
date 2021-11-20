﻿using System;
using Xamarin.Forms;
using Xamarin.Essentials;
using Xamarin.Forms.PlatformConfiguration.iOSSpecific;
using MyITracker.Common;

namespace MyITracker.Views
{
    public partial class CustomerTabbedPage : Xamarin.Forms.TabbedPage, IView
    {
        #region Constructor
        public CustomerTabbedPage()
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

        protected override void OnAppearing()
        {
            base.OnAppearing();

            //_ = ServerApi.MultipartPostLoadLists(); /* This should refresh the global lists when needed. */
        }
    }
}
