using System;
using System.Collections.Generic;

using Xamarin.Forms;

namespace MyITracker.Views {
    public partial class AddCustomerNotePage : ContentPage, IView {
        public AddCustomerNotePage() {
            InitializeComponent();
        }
        public IViewModel ViewModel {
            get => (IViewModel)BindingContext;
            set => BindingContext = value;
        }
    }
}
