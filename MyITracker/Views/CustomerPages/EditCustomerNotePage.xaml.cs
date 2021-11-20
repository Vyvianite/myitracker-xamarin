using System;
using System.Collections.Generic;

using Xamarin.Forms;

namespace MyITracker.Views {
    public partial class EditCustomerNotePage : ContentPage, IView {
        public EditCustomerNotePage() {
            InitializeComponent();
        }
        public IViewModel ViewModel {
            get => (IViewModel)BindingContext;
            set => BindingContext = value;
        }
    }
}
