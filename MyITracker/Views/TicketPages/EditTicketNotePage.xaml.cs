using System;
using System.Collections.Generic;
using MyITracker.Common;
using Xamarin.Forms;

namespace MyITracker.Views {
    public partial class EditTicketNotePage : ContentPage, IView {
        public EditTicketNotePage() {
            InitializeComponent();
        }
        public IViewModel ViewModel {
            get => (IViewModel)BindingContext; 
            set => BindingContext = value; 
        }
    }
}
