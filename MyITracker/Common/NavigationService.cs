using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using MyITracker.Common;
using Xamarin.Forms;

namespace MyITracker.ViewModels {
  public static class NavigationService {
    private static Dictionary<Type, Type> ViewReference = new Dictionary<Type, Type>();

    public static void Register<TViewModel, TView>() where TViewModel : IViewModel where TView : IView {
      if (!ViewReference.ContainsKey(typeof(TViewModel))) {
        ViewReference.Add(typeof(TViewModel), typeof(TView));
      }
    }

    public static async Task PushAsync<TViewModel>(TViewModel vm) where TViewModel : IViewModel//You can make this construct vm as well if you find you need to. working version should be commented below.
    {
      await Application.Current.MainPage.Navigation.PushAsync((Page)ViewFactory(vm));
    }

    public static async Task PopAsync() {
      await Application.Current.MainPage.Navigation.PopAsync();
    }

    public static void ChangeNavStack<TViewModel>(TViewModel vm) where TViewModel : IViewModel {
      Application.Current.MainPage = new NavigationPage((Page)ViewFactory(vm));
    }

    private static IView ViewFactory<TViewModel>(TViewModel viewModel) where TViewModel : IViewModel {
      if (ViewReference.ContainsKey(typeof(TViewModel))) {
        var page = (IView)Activator.CreateInstance(ViewReference[typeof(TViewModel)]);
        page.ViewModel = viewModel;
        return page;
      }
      else {
        throw new KeyNotFoundException();
      }
    }
  }
}

//private static IView VMAndPageFactory( Type VMType, IViewModel instanceVM = null, object vmParameter = null )
//{
//    IViewModel vm;
//    if ( instanceVM is object )
//    {
//        vm = instanceVM;
//    }
//    else
//    {
//        vm = (IViewModel)Activator.CreateInstance( type: VMType, args: vmParameter );
//    }
//    if ( ViewReference.ContainsKey( VMType ) )
//    {
//        return (IView)Activator.CreateInstance( ViewReference[ VMType ], args: vm );
//    }
//    else
//    {
//        throw new KeyNotFoundException();
//    }

//}