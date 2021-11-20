using System;
using System.Collections.Generic;

using Xamarin.Forms;

namespace MyITracker.Views {
    public partial class LoadingPage : ContentPage, IView {
        public LoadingPage() {
            InitializeComponent();
        }
        public IViewModel ViewModel {
            get { return (IViewModel)BindingContext; }
            set { BindingContext = value; }
        }
    }
}
