using System;
using Xamarin.Forms;
using System.Threading.Tasks;
using Microsoft.FSharp.Core;
using MyITracker.Common;

/* Ways to inititalize viewmodels:
 *  Static factory methods with private constructors
 *  Seperately called init method
 *  Fire and forget async init from constructor */

namespace MyITracker.ViewModels {
  public abstract class BaseVM : BaseNotify, IViewModel {

    public abstract Task InitializeAsync();

    protected static async Task InvalidVM() {
      ViewActions.DisplayAlert("Error:", "Data could not be loaded", "Ok").SafeFireAndForget(true);
      await NavigationService.PopAsync();
    }
    public static async Task<T> ResToExc<T>(Task<FSharpResult<T, string>> tResult) {
      var result = await tResult;
      return result.IsOk
        ? result.ResultValue
        : throw new Exception(result.ErrorValue);
    }

    ////Helper function for spliting F# record types into vm's properties and raising change notifs
    //public static void Deconstruct<TFrom, TTo>(TFrom from, TTo to) where TTo : BaseVM {
    //  var fromProps = typeof(TFrom).GetProperties();
    //  foreach (var t in typeof(TTo).GetProperties()) {
    //    foreach (var f in fromProps) {
    //      if (f.Name.Equals(t.Name) && f.GetType().Equals(t.GetType()) && t.CanWrite) {
    //        t.SetValue(to, f.GetValue(from));
    //        to.OnPropertyChanged(t.Name);
    //      }
    //    }
    //  }
    //}
  }
}