using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace MyITracker.Common {
  public abstract class BaseNotify : INotifyPropertyChanged {
    //Interface Implementation
    public event PropertyChangedEventHandler PropertyChanged;

    protected void OnPropertyChanged([CallerMemberName] string propertiesName = "") {
      PropertyChangedEventHandler handler = PropertyChanged;
      handler?.Invoke(this, new PropertyChangedEventArgs(propertiesName));
    }

    protected void OnPropertyChanged(object sender, [CallerMemberName] string propertiesName = "") {
      PropertyChangedEventHandler handler = PropertyChanged;
      handler?.Invoke(sender, new PropertyChangedEventArgs(propertiesName));
    }

    protected bool SetField<T>(ref T field, T value, [CallerMemberName] string propertyName = "") {
      if (EqualityComparer<T>.Default.Equals(field, value)) {
        return false;
      }
      else {
        field = value;
        OnPropertyChanged(propertyName);
        return true;
      }
    }
  }
}