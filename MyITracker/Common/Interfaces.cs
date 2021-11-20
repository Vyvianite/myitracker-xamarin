using System;
using System.ComponentModel;
using System.Threading.Tasks;
using MyITracker.ViewModels;

namespace MyITracker {

  public interface IViewModel : INotifyPropertyChanged {
    Task InitializeAsync();
  }

  public interface IRefreshable : IViewModel {
    Task RefreshData();
    Observable<bool> IsRefreshing { get; }
  }

  public interface IView {
    IViewModel ViewModel { get; set; } //Property injection for views binding context
  }
}