using System;
using System.Collections.Generic;

using Xamarin.Forms;

namespace MyITracker.Views {

    public partial class AddTicketNotePage : ContentPage, IView {

        public AddTicketNotePage() {
            InitializeComponent();
        }

        public IViewModel ViewModel {
            get { return (IViewModel)BindingContext; }
            set { BindingContext = value; }
        }
    }
}
