using System;
using Xamarin.Forms;
using Xamarin.Forms.PlatformConfiguration.iOSSpecific;

namespace MyITracker.Views {
  public partial class DashboardPage : Xamarin.Forms.TabbedPage, IView {
    #region Constructor
    public DashboardPage() {
      InitializeComponent();
      On<Xamarin.Forms.PlatformConfiguration.iOS>().SetUseSafeArea(true);
    }
    #endregion
    public IViewModel ViewModel {
      get => (IViewModel)BindingContext;
      set => BindingContext = value;
    }
  }
}
