using System;
using System.Collections.ObjectModel;
using System.Net.Http;
using System.Threading.Tasks;
using System.Linq;
using Xamarin.Forms;
using Newtonsoft.Json;
using System.Collections.Generic;

namespace MyITracker.ViewModels {
  public class AddCustomerVM : BaseVM {
    #region Properties
    private readonly DashboardVM ParentVM;

    public string Name { get; set; }
    public string Contract { get; set; }
    public string Street { get; set; }
    public string City { get; set; }
    public string State { get; set; }
    public string Zipcode { get; set; }
    public string Phone { get; set; }
    public string Email { get; set; }
    public string Contact { get; set; }
    public string AltContact { get; set; }
    #endregion

    public AddCustomerVM(DashboardVM parentVM) {
      ParentVM = parentVM;
    }

    #region Commands
    Command upload;
    public Command Upload {
      get => upload ??= new Command(async () => {
        await UploadData();
      });
    }

    Command cancel;
    public Command Cancel {
      get => cancel ??= new Command(async () => {
        await NavigationService.PopAsync();
      });
    }
    #endregion

    #region Methods
    public override Task InitializeAsync() {
      return Task.CompletedTask;
    }

    public async Task UploadData() {
      if (new HashSet<object> { /* Todo add behavior based validation */
          Name,
          Contact,
          Street,
          City,
          Zipcode,
          State,
          Phone,
          Email,
          Contact,
        }.NullCheck()) {
        ViewActions.DisplayAlert("Error:", "Fill all entries", "Ok").SafeFireAndForget(true);
        return;
      }

      try {
        var response = await ResToExc(CustomerRepo.insert(new Customer("", Name, Phone, Email, Contact, AltContact, Contract, Street, City, State, Zipcode))); //todo add succes checking and handling
        ParentVM.RefreshData().SafeFireAndForget(true);
        ViewActions.DisplayAlert("Success", "Customer was added", "Ok").SafeFireAndForget(true);
        await NavigationService.PopAsync();
      }
      catch {
        ViewActions.DisplayAlert("Error:", "Could not Post", "Ok").SafeFireAndForget(true);
      }
    }
    #endregion
  }
}