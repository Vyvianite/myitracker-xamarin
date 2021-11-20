using System;
using System.Diagnostics.CodeAnalysis;
using MyITracker.Common;

namespace MyITracker.ViewModels {

  /*Designed to make PropertyChanged events easeier to use, as well as providing a base for 
   more complex subclasses. */
  public class Observable<TValue> : BaseNotify {
    public Observable() { }

    [MaybeNull]
    /* Dumb bindable value store. */
    public TValue Value { get; set; } //todo test if setter is called by xaml code.

    /* Sets Value and raises PropertyChanged event. Primarily provides a standard api for subclasses. */
    public void Set(TValue input) {
      Value = input;
      OnPropertyChanged("Value");
    }

    [return: MaybeNull]
    public static implicit operator TValue(Observable<TValue> val) {
      return val.Value;
    }

    public TValue FromObservable() => Value;
  }

  /* Subclass which uses a converter to map input types. */
  public class Observable<TValue, TConverter> : Observable<TValue> {

    /* Type converter store */
    private readonly Func<TConverter, TValue> converter;
    
    public Observable(Func<TConverter, TValue> converter) {
      this.converter = converter;
    }

    /* Overload of Set() that uses typeconverter */
    public void Set(TConverter input) {
      Set(converter(input));
    }
  }
}
