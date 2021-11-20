using System;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using System.Linq;
using Xamarin.Forms;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Threading;

using static MyITracker.TicketRepo;

namespace MyITracker.ViewModels {
  public class EditTicketVM : BaseVM, IRefreshable {

    #region Properties
    private readonly IRefreshable ParentVM;
    private readonly string TID;

    private string subject;
    public string Subject {
      get => subject;
      set => SetField(ref subject, value);
    }

    private User assigned;
    public User Assigned {
      get => assigned;
      set => SetField(ref assigned, value);
    }

    private Situation situation;
    public Situation Situation {
      get => situation;
      set => SetField(ref situation, value);
    }

    private Customer customer;
    public Customer Customer {
      get => customer;
      set => SetField(ref customer, value);
    }

    private Priority priority;
    public Priority Priority {
      get => priority;
      set => SetField(ref priority, value);
    }

    private ObservableCollection<TicketNote> notes;
    public ObservableCollection<TicketNote> Notes {
      get => notes;
      set => SetField(ref notes, value);
    }

    private bool _isRefreshing = false;
    public bool IsRefreshing {
      get => _isRefreshing;
      private set => SetField(ref _isRefreshing, value);
    }

    public ObservableCollection<User> TechCollection { get => Lists.Techs; }
    public ObservableCollection<Priority> PriorityCollection { get => Lists.Priorities; }
    public ObservableCollection<Situation> SituationCollection { get => Lists.Situations; }
    #endregion

    #region Constructor
    public EditTicketVM(IRefreshable parentVM, string tid) {
      ParentVM = parentVM;
      TID = tid;
    }
    #endregion

    #region Commands
    Command close;
    public Command Close {
      get => close ??= new Command(async () => {
        var response = await ViewActions.DisplayAlert("Alert", "Are you sure?", "Yes", "Cancel");
        if (response) {
          await CloseTicket();
        }
        else {
          return;
        }
      });
    }

    Command upload;
    public Command Upload {
      get => upload ??= new Command(async () => {
        await UploadData();
      });
    }

    Command refresh;
    public Command Refresh {
      get => refresh ??= new Command(async () => {
      IsRefreshing = true;
      await RefreshData();
      await Task.Delay(500);
      IsRefreshing = false;
    });
    }

    Command editNote;
    public Command EditNote {
      get => editNote ??= new Command(async (item) => {
        var vm = new EditTicketNoteVM(this, (TicketNote)item);
        vm.InitializeAsync().SafeFireAndForget(true);
        await NavigationService.PushAsync(vm);
      });
    }

    Command addNote;
    public Command AddNote {
      get => addNote ??= new Command(async () => {
        var vm = new AddTicketNoteVM(this, TID.ToString());
        vm.InitializeAsync().SafeFireAndForget(true);
        await NavigationService.PushAsync(vm);
      });
    }
    #endregion

    #region Functions
    public override async Task InitializeAsync() {
      try {
        var tTicket = get(TID);
        var tNotes = Note.list(TID);

        var ticket = await ResToExc(tTicket);
        Deconstruct(ticket, this);

        Notes = new ObservableCollection<TicketNote>(await ResToExc(tNotes));
      }
      catch (Exception e) {
        await InvalidVM();
      }
    }

    public async Task RefreshData() {
      try {
        var tNotes = Note.list(TID);
        Notes = new ObservableCollection<TicketNote>(await ResToExc(tNotes));
      }
      catch (Exception e) {
        ViewActions.DisplayAlert("Error:", "Cannot load data", "Ok").SafeFireAndForget(true);
      }
    }

    private async Task UploadData() {
      if (new HashSet<object> { /* Todo add behavior based validation */
          Subject,
          Assigned,
          Situation,
          Customer,
          Priority,
        }.NullCheck()) {
        ViewActions.DisplayAlert("Error:", "Fill all entries", "Ok").SafeFireAndForget(true);
        return;
      }

      try {
        var upload = new Dictionary<string, string> {
          {"tid", TID },
          {"tkt_subject", Ticket.tkt_subject},
          {"user_id", Ticket.Assigned.user_id},
          {"situation", Ticket.Situation.situation_id},
          {"priority", Ticket.Priority.prid},
        };

        string response = await ResToExc();//todo validate me
        ParentVM.InitializeAsync().SafeFireAndForget(true);
        ViewActions.DisplayAlert("Success", "Ticket was updated", "Ok").SafeFireAndForget(true);
        await NavigationService.PopAsync();
      }
      catch (Exception e) {
        ViewActions.DisplayAlert("Error:", "Can't post data", "Ok").SafeFireAndForget(true);
      }
    }

    private async Task CloseTicket() {
      try {
        var upload = new Dictionary<string, string> {
          {"tid", TID }
        };

        var response = await Api.MultipartPost(Api.Pages.TicketUri, Api.TicketCommands.CloseTicket, upload);
        ParentVM.InitializeAsync().SafeFireAndForget(true);
        ViewActions.DisplayAlert("Success", "Ticket was closed", "Ok").SafeFireAndForget(true);
        await NavigationService.PopAsync();
      }
      catch (Exception e) {
        ViewActions.DisplayAlert("Error:", "Cannot close Ticket", "Ok").SafeFireAndForget(true);
        NavigationService.PopAsync().SafeFireAndForget(true);
      }
    }
    #endregion
  }
}


