using System;
using System.Collections.Generic;

using Xamarin.Forms;

namespace MyITracker.Views {
    public partial class SearchPage : ContentPage, IView {
        public SearchPage() {
            InitializeComponent();
        }
        public IViewModel ViewModel {
            get { return (IViewModel)BindingContext; }
            set { BindingContext = value; }
        }
    }
}
