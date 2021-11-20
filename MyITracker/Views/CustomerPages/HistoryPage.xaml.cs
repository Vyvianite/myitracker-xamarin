﻿using System;
using Xamarin.Forms;
using Xamarin.Essentials;
using Xamarin.Forms.PlatformConfiguration.iOSSpecific;
using MyITracker.Common;
using MyITracker.ViewModels;

using System.Collections.Generic;

namespace MyITracker.Views {
  public partial class HistoryPage : ContentPage, IView {
    #region Constructor
    public HistoryPage() {
      InitializeComponent();
      On<Xamarin.Forms.PlatformConfiguration.iOS>().SetUseSafeArea(true);

      #region ViewLogic
      #endregion
    }
    #endregion
    public IViewModel ViewModel {
      get { return (IViewModel)BindingContext; }
      set { BindingContext = value; }
    }
  }
}
