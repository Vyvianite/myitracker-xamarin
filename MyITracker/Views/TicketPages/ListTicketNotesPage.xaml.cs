﻿using System;
using System.Collections.Generic;
using MyITracker.Common;
using Xamarin.Forms;
using Xamarin.Forms.PlatformConfiguration.iOSSpecific;

namespace MyITracker.Views {
    public partial class ListTicketNotesPage : ContentPage, IView {
        public ListTicketNotesPage() {
            InitializeComponent();
            On<Xamarin.Forms.PlatformConfiguration.iOS>().SetUseSafeArea(true);
        }
        public IViewModel ViewModel {
            get { return (IViewModel)BindingContext; }
            set { BindingContext = value; }
        }
    }
}
