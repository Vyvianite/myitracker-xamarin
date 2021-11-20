using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Xamarin.Forms;

namespace MyITracker.ViewModels {
  public class DashboardVM : BaseVM, IRefreshable {
    #region Properties
    public Observable<bool> IsRefreshing { get; } = new Observable<bool>();

    public Observable<ObservableCollection<ShortTicket>> Tickets { get; } = new Observable<ObservableCollection<ShortTicket>>();

    public Observable<ObservableCollection<ShortCustomer>> Customers { get; } = new Observable<ObservableCollection<ShortCustomer>>();
    #endregion

    #region Constructor
    public DashboardVM() { }
    #endregion

    #region Commands
    Command search;
    public Command Search {
      get => search ??= new Command(async (obj) => {
        var vm = new SearchVM(this, (string)obj);
        vm.InitializeAsync().SafeFireAndForget(true);
        await NavigationService.PushAsync(vm);
      });
    }

    Command logout;
    public Command LogoutCommand {
      get => logout ??= new Command(async () => {
        var response = await ViewActions.DisplayAlert("Alert", "Are you sure?", "Yes", "Cancel");
        if (response) {
          await LoginRepo.empty();
          var vm = new LoginVM();
          vm.InitializeAsync().SafeFireAndForget(true);
          NavigationService.ChangeNavStack(vm);
        }
        else {
          return;
        }
      });
    }

    Command refresh;
    public Command Refresh =>
      refresh ??= new Command(async () => {
        IsRefreshing.Set(true); //todo use threads --Huh? why was I asking this here?
        await InitializeAsync();
        await Task.Delay(500); //The loading felt too fast.
        IsRefreshing.Set(false);
    });

    Command addTicket;
    public Command AddTicket {
      get => addTicket ??= new Command(async () => {
        var vm = new AddTicketVM(this);
        await NavigationService.PushAsync(vm);
      });
    }

    Command addCustomer;
    public Command AddCustomer {
      get => addCustomer ??= new Command(async () => {
        var vm = new AddCustomerVM(this);
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

    Command editCustomer;
    public Command EditCustomer {
      get => editCustomer ??= new Command(async (item) => {
        var vm = new EditCustomerVM(this, ((ShortCustomer)item));
        vm.InitializeAsync().SafeFireAndForget(true);
        await NavigationService.PushAsync(vm);
      });
    }
    #endregion

    #region Methods
    /* These list are interconnected,
    if any one of them fails, the whole prgram
    should display an error and quit. */
    public override async Task InitializeAsync() {
      try {
        //todo add more validation
        /* Send requests first, and the await the results */
        var ticketTask = TicketRepo.list();
        var customerTask = CustomerRepo.list();
        var techTask = UserRepo.listTechs();
        var situationTask = SituationRepo.list();
        var priorityTask = PriorityRepo.list();

        var rTechs = await ResToExc(techTask);
        var rSituations = await ResToExc(situationTask);
        var rPriorities = await ResToExc(priorityTask);
        var rCustomers = await ResToExc(customerTask);
        var rTickets = await ResToExc(ticketTask);

        var techs = new List<User>(rTechs);
        if (Lists.Techs is null || !Lists.Techs.SequenceEqual(rTechs)) {
          Lists.Techs = techs;
        }

        var situations = new List<Situation>(rSituations);
        if (Lists.Situations is null || !Lists.Situations.SequenceEqual(situations)) {
          Lists.Situations = situations;
        }

        var priorities = new List<Priority>(rPriorities);
        if (Lists.Priorities is null || !Lists.Priorities.SequenceEqual(priorities)) {
          Lists.Priorities = priorities;
        }

        var customers = new ObservableCollection<ShortCustomer>(rCustomers);
        if (Customers.Value is null || !Customers.Value.SequenceEqual(customers)) {
          Customers.Set(customers);
          Lists.Customers = customers.ToList();
        }

        var tickets = new ObservableCollection<ShortTicket>(rTickets);
        if (Tickets is null || !Tickets.Value.SequenceEqual(tickets)) {
          Tickets.Set(tickets);
        }
      }
      catch (Exception e) {
        ViewActions.DisplayAlert("Error:", $"Cannot access server data: {e.Message};", "Ok").SafeFireAndForget(true);
      }
    }

    public async Task RefreshData() {
      await InitializeAsync();
    }
    #endregion
  }
}
