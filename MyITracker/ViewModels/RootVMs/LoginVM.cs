using System;
using Xamarin.Forms;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.FSharp.Core;

namespace MyITracker.ViewModels {
  public class LoginVM : BaseVM {
    #region Properties
    public string? Username { get; set; }
    public string? Password { get; set; }
    #endregion

    public LoginVM() {}

    #region Commands
    Command login;
    public Command Login {
      get => login ??= new Command(async () => {
        await LoginValidation();
      });
    }
    #endregion

    #region Methods
    public override Task InitializeAsync() {
      return Task.CompletedTask;
    }

    private async Task LoginValidation() {
      try {
        if (new HashSet<string>{ Username, Password }.NullCheck()) {
          ViewActions.DisplayAlert("Auth Failure:", "Username of Password cannot be null", "Ok").SafeFireAndForget(true);
          return;
        }

        var res = await ResToExc(LoginRepo.validate(Username, Password));

        if (res) {
          HttpApi.setAuth(new FSharpOption<Login>(new Login { Username = Username, Password = Password }));
          var vm = new DashboardVM();
          vm.InitializeAsync().SafeFireAndForget(true);
          NavigationService.ChangeNavStack(vm);
          LoginRepo.insert(Username, Password).SafeFireAndForget(false);
        }
        else {
          ViewActions.DisplayAlert("Auth Failure:", "Invalid Username or Password", "Ok").SafeFireAndForget(true);
        }
      }
      catch (Exception e) {
        ViewActions.DisplayAlert("Error:", e.Message, "Ok").SafeFireAndForget(true);
      }
    }
    #endregion
  }
}
