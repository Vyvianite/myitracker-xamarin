using System;
using System.Threading.Tasks;
using System.Linq;

using Xamarin.Forms;

using System.Collections.Generic;


namespace MyITracker.ViewModels {

  public class AddTicketNoteVM : BaseVM {
    #region Constructor
    public AddTicketNoteVM(IRefreshable parentVM, string tid) {

      ParentVM = parentVM;
      TID = tid;

      UploadCommand = new Command(async () => {
        await UploadData();
      });
    }
    #endregion

    #region Properties
    private readonly string TID;
    private readonly IRefreshable ParentVM;

    private TicketNoteData _note = new TicketNoteData();
    public TicketNoteData Note {
      get => _note;
      set => SetField(ref _note, value);
    }
    #endregion

    #region Commands
    public Command UploadCommand { get; private set; }
    #endregion

    #region Functions
    public override Task InitializeAsync() {
      return Task.CompletedTask;
    }

    public async Task UploadData() {
      if (new HashSet<object> { /* Todo add behavior based validation */
          Note.note,
        }.NullCheck()) {
        ViewActions.DisplayAlert("Error:", "Fill all entries", "Ok").SafeFireAndForget(true);
        return;
      }

      try {
        var upload = new Dictionary<string, string> {
          {"note", Note.note },
          {"tid", TID },
        };

        string response = await Api.MultipartPost(Api.Pages.TicketUri, Api.TicketCommands.NewNote, upload); //todo validate me
        ParentVM.RefreshData().SafeFireAndForget(true);
        ViewActions.DisplayAlert("Success", "Ticket was updated", "Ok").SafeFireAndForget(true);
        await NavigationService.PopAsync();
      }
      catch {
        await ViewActions.DisplayAlert("Error:", "Can't post data", "Ok");
      }
    }
    #endregion
  }
}