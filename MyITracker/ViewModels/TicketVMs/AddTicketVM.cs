using System;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using System.Linq;

using Xamarin.Forms;

using System.Collections.Generic;

namespace MyITracker.ViewModels {
  public class AddTicketVM : BaseVM {
    public AddTicketVM(DashboardVM parentVM) {
      ParentVM = parentVM;

      UploadCommand = new Command(async () => {
        await UploadData();
      });

      CancelCommand = new Command(async () => {
        await NavigationService.PopAsync();
      });
    }

    #region Properties
    private readonly DashboardVM ParentVM;

    private TicketData _ticket = new TicketData();
    public TicketData Ticket {
      get => _ticket;
      set => SetField(ref _ticket, value);
    }

    private TicketNoteData _note = new TicketNoteData();
    public TicketNoteData Note {
      get => _note;
      set => SetField(ref _note, value);
    }

    /* Custom setters. They take the string from json, find the matching value in the lists, and then insert that as
     * the object in the ticket instead of the simple string. */
    public ShortCustomer Customer { get; set; }
    public string qbsql_id {
      set => Customer = Lists.Customers.FirstOrDefault(x => x.Index.Equals(value));
    }

    public TechData Assigned { get; set; }
    public string uid {
      set => Assigned = Lists.Techs.FirstOrDefault(x => x.user_id.Equals(value));
    }

    public SituationData Situation { get; set; }
    public string situation {
      set => Situation = Lists.Situations.FirstOrDefault(x => x.situation_id.Equals(value));
    }

    private PriorityData _PriorityObject;
    public PriorityData Priority {
      get => _PriorityObject;
      set {
        _PriorityObject = value;
        _priority = value.prid;
      }
    }

    private string _priority;
    public string priority {
      get => _priority;
      set {
        _priority = value;
        Priority = Lists.Priorities.FirstOrDefault(x => x.prid.Equals(value));
      }
    }

    public ObservableCollection<ShortCustomer> CustomerCollection {
      get => Lists.Customers;
    }

    public ObservableCollection<TechData> TechCollection {
      get => Lists.Techs;
    }

    public ObservableCollection<PriorityData> PriorityCollection {
      get => Lists.Priorities;
    }

    public ObservableCollection<SituationData> SituationCollection {
      get => Lists.Situations;
    }

    #endregion

    #region Commands
    public Command UploadCommand { get; private set; }

    public Command CancelCommand { get; private set; }
    #endregion

    #region Methods
    public override Task InitializeAsync() {
      return Task.CompletedTask;
    }

    //todo wwhy oh why is this tnotes and not notes like in mainticketvm
    public async Task UploadData() {
      if (new HashSet<object> { /* Todo add behavior based validation */
          Ticket.tkt_subject,
          Ticket.Assigned,
          Ticket.Situation,
          Ticket.Customer,
          Ticket.Priority,
          Note.note
        }.NullCheck()) {
        ViewActions.DisplayAlert("Error:", "Fill all entries", "Ok").SafeFireAndForget(true);
        return;
      }

      try {
        var upload = new Dictionary<string, string> {
          {"tkt_subject", Ticket.tkt_subject},
          {"user_id", Ticket.Assigned.user_id},
          {"situation", Ticket.Situation.situation_id},
          {"priority", Ticket.Priority.prid},
          {"qbsql_id", Ticket.Customer.Index },
          {"tnotes", Note.note },
          {"createdby", User.Auth.Value.Username },
        };
        string response = await Api.MultipartPost(Api.Pages.TicketUri, Api.TicketCommands.NewTicket, upload);
        ParentVM.InitializeAsync().SafeFireAndForget(true);
        ViewActions.DisplayAlert("Success", "Ticket was added", "Ok").SafeFireAndForget(true);
        NavigationService.PopAsync();
      }
      catch {
        ViewActions.DisplayAlert("Error:", "Can't post data", "Ok").SafeFireAndForget(true);
      }

    }
    #endregion
  }
}



//string nullItems = Utilities.NullCheckDictionary(nullCheck);
//if (!string.IsNullOrEmpty(nullItems)) {
//    _ = ViewActions.DisplayAlert("Error:", $"These fields cannot be null: {nullCheck}", "Ok");
//    return;
//}