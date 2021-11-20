using System;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using Xamarin.Forms;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Linq;

using static MyITracker.CustomerRepo;
using Task = System.Threading.Tasks.Task;

namespace MyITracker.ViewModels {
  public class EditCustomerVM : BaseVM, IRefreshable {

    #region Properties
    private readonly IRefreshable parentVM;
    public string CustomerId { get; }

    public Observable<string> Name { get; } = new Observable<string>();
    public Observable<string> Contract { get; } = new Observable<string>();
    public Observable<string> Street { get; } = new Observable<string>();
    public Observable<string> City { get; } = new Observable<string>();
    public Observable<string> State { get; } = new Observable<string>();
    public Observable<string> Zipcode { get; } = new Observable<string>();
    public Observable<string> Phone { get; } = new Observable<string>();
    public Observable<string> Email { get; } = new Observable<string>();
    public Observable<string> Contact { get; } = new Observable<string>();
    public Observable<string> AltContact { get; } = new Observable<string>();

    public Observable<ObservableCollection<CustomerNote>> Notes { get; } = new Observable<ObservableCollection<CustomerNote>>();

    public Observable<ObservableCollection<ShortTicket>> History { get; } = new Observable<ObservableCollection<ShortTicket>>();

    public Observable<bool> IsRefreshing { get; } = new Observable<bool>();
    #endregion

    #region Constructor
    public EditCustomerVM(IRefreshable parentVM, ShortCustomer customer) {
      this.parentVM = parentVM;
      CustomerId = customerId;
    }
    #endregion

    #region Commands
    Command upload;
    public Command Upload {
      get => upload ??= new Command(async () => {
        await UploadData();
      });
    }

    Command refresh;
    public Command Refresh {
      get => refresh ??= new Command(async () => {
        IsRefreshing.Set(true);
        await RefreshData();
        await Task.Delay(500);
        IsRefreshing.Set(false);
      });
    }

    Command editNote;
    public Command EditNote {
      get => editNote ??= new Command(async (item) => {
        var vm = new EditCustomerNoteVM(this, (CustomerNote)item);
        vm.InitializeAsync();
        await NavigationService.PushAsync(vm);
      });
    }

    Command addNote;
    public Command AddNote {
      get => addNote ??= new Command(async () => {
        var vm = new AddCustomerNoteVM(this, CustomerId);
        vm.InitializeAsync();
        await NavigationService.PushAsync(vm);
      });
    }

    Command editTicket;
    public Command EditTicket {
      get => editTicket ??= new Command(async (item) => {
        var vm = new EditTicketVM(this, ((ShortTicket)item).Index);
        vm.InitializeAsync().SafeFireAndForget(true);
        await NavigationService.PushAsync(vm);
      });
    }
    #endregion

    #region Methods

    public void Map(Customer customer) {
      if (customer is null) {
        throw new NullReferenceException("Customer cannot be null");
      }

      Name.Set(customer.Name);
      Contract.Set(customer.Contract);
      Street.Set(customer.Street);
      City.Set(customer.City);
      State.Set(customer.State);
      Zipcode.Set(customer.Zipcode);
      Phone.Set(customer.Phone);
      Email.Set(customer.Email);
      Contact.Set(customer.Contact);
      AltContact.Set(customer.AltContact);
    }

    public override async Task InitializeAsync() {
      try {
        var tCustomer = get(CustomerId);
        var tNotes = Note.list(CustomerId);
        var tHistory = getHistory(CustomerId);

        var customer = await ResToExc(tCustomer);
        Map(customer);

        Notes.Set(new ObservableCollection<CustomerNote>(await ResToExc(tNotes)));
        History.Set(new ObservableCollection<ShortTicket>(await ResToExc(tHistory)));
      }
      catch (Exception e) {
        await InvalidVM();
      }
    }

    public async Task RefreshData() {
      try {
        var tNotes = Note.list(CustomerId);
        var tHistory = getHistory(CustomerId);

        Notes.Set(new ObservableCollection<CustomerNote>(await ResToExc(tNotes)));
        History.Set(new ObservableCollection<ShortTicket>(await ResToExc(tHistory)));
      }
      catch (Exception e) {
        ViewActions.DisplayAlert("Error:", "Cannot load data", "Ok").SafeFireAndForget(true);
      }
    }

    private async Task UploadData() {
      if (new HashSet<string> { /* Todo add behavior based validation */
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
        var response = update(CustomerId, Name, Phone, Email, Contact, AltContact, Contract, Street, City, State, Zipcode);
        var debug = await ResToExc(response);
        parentVM.InitializeAsync().SafeFireAndForget(true);
        ViewActions.DisplayAlert("Success", "Customer was updated", "Ok").SafeFireAndForget(true); //todo Validate this is actually succesfull.
        await NavigationService.PopAsync();
      }
      catch {
        ViewActions.DisplayAlert("Error:", "Could not Update Record", "Ok").SafeFireAndForget(true);
      }
    }
    #endregion
  }
}