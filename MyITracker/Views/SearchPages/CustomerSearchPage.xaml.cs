using System;
using System.Collections.Generic;

using Xamarin.Forms;

namespace MyITracker.Views {
    public partial class CustomerSearchPage : ContentPage, IView {
        public CustomerSearchPage() {
            InitializeComponent();
        }
        public IViewModel ViewModel {
            get { return (IViewModel)BindingContext; }
            set { BindingContext = value; }
        }
    }
}
