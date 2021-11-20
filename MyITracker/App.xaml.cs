using System;
using Xamarin.Forms;
using MyITracker.Views;
using MyITracker.ViewModels;
using Microsoft.FSharp.Core;

namespace MyITracker {

  public partial class App : Application {

    /* App activity is closed by backing out of the app. When reentering all state with still be in memory, but the app constructor will be reinitialized */
    public App() {
      InitializeComponent();
      Register(); //Registering View/ViewModel pairings
      MainPage = new LoadingPage();
      LoginCheck();
    }

    private async protected void LoginCheck() {
      var login = await LoginRepo.get();
      if (login is null) {
        newLogin();
      }
      else {
        try {
          var valid = login.Value; //todo Is this the best place to have responsibility for setting Auth?
          HttpApi.setAuth(new FSharpOption<Login>(new Login { Username = valid.Username, Password = valid.Password }));
          var vm = new DashboardVM();
          vm.InitializeAsync().SafeFireAndForget(true);
          MainPage = new NavigationPage(new DashboardPage() { ViewModel = vm });
        }
        catch {
          ViewActions.DisplayAlert("Error", "Could not get local token. Please refresh your login.", "Ok");
          newLogin();
        }
        
      }
      void newLogin() {
        var vm = new LoginVM();
        vm.InitializeAsync().SafeFireAndForget(true);
        MainPage = new NavigationPage(new LoginPage() { ViewModel = vm });
      }
    }

    private static protected void Register() {
      NavigationService.Register<LoginVM, LoginPage>();
      NavigationService.Register<DashboardVM, DashboardPage>();

      NavigationService.Register<EditCustomerVM, CustomerTabbedPage>();
      NavigationService.Register<AddCustomerVM, AddCustomerPage>();
      NavigationService.Register<EditCustomerNoteVM, EditCustomerNotePage>();
      NavigationService.Register<AddCustomerNoteVM, AddCustomerNotePage>();

      NavigationService.Register<EditTicketVM, TicketTabbedPage>();
      NavigationService.Register<AddTicketVM, AddTicketPage>();
      NavigationService.Register<EditTicketNoteVM, EditTicketNotePage>();
      NavigationService.Register<AddTicketNoteVM, AddTicketNotePage>();

      NavigationService.Register<SearchVM, SearchTabbedPage>();
    }

    protected override void OnStart() {
    }

    protected override void OnSleep() {
    }

    protected override void OnResume() {
    }
  }
}
