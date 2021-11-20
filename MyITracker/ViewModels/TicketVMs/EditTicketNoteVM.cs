using System;
using System.Threading.Tasks;
using System.Linq;

using Xamarin.Forms;
using Newtonsoft.Json;
using System.Collections.Generic;



namespace MyITracker.ViewModels {

  public class EditTicketNoteVM : BaseVM, IViewModel {

    #region Constructor
    public EditTicketNoteVM(IRefreshable parentVM, TicketNote note) {
      _note = new TicketNoteData() {
        rid = note.rid,
        note = note.note,
        tid = note.tid,
        version = note.version
      };

      ParentVM = parentVM;

      DeleteCommand = new Command(async () => {
        var response = await ViewActions.DisplayAlert("Alert", "Are you sure?", "Yes", "Cancel");
        if (response) {
          await DeleteNote();
        }
        else {
          return;
        }
      });

      UploadCommand = new Command(async () => {
        await UploadData();
      });
    }
    #endregion

    #region Properties
    private readonly IRefreshable ParentVM;

    private TicketNoteData _note;
    public TicketNoteData Note {
      get => _note;
      set => SetField(ref _note, value);
    }
    #endregion

    #region Commands
    public Command DeleteCommand { get; private set; }
    public Command UploadCommand { get; private set; }
    #endregion

    #region Functions
    public override Task InitializeAsync() {
      return Task.CompletedTask;
    }

    async Task DeleteNote() {
      try {
        var upload = new Dictionary<string, string> {
          {"rid", Note.rid },
        };
        var response = await Api.MultipartPost(Api.Pages.TicketUri, Api.TicketCommands.DeleteNote, upload); //todo validate response

        ParentVM.RefreshData().SafeFireAndForget(true);
        await NavigationService.PopAsync();
      }
      catch (Exception e) {
        ViewActions.DisplayAlert("Error:", "Can't post data", "Ok").SafeFireAndForget(true);
      }
    }

    async Task UploadData() {
      if (new HashSet<object> {
          Note.note,
        }.NullCheck()) {
        ViewActions.DisplayAlert("Error:", "Fill all entries", "Ok").SafeFireAndForget(true);
        return;
      }

      try {
        var upload = new Dictionary<string, string> {
          {"tid", Note.tid },
          {"rid", Note.rid },
          {"note", Note.note },
          {"version", Note.version }
        };
        var response = await Api.MultipartPost(Api.Pages.TicketUri, Api.TicketCommands.UpdateNote, upload); //todo validate response

        if (Utilities.NoteMergeConflict(response, Note)) {
          return;
        }

        ParentVM.RefreshData().SafeFireAndForget(true);
        ViewActions.DisplayAlert("Success", "Ticket was updated", "Ok").SafeFireAndForget(true);
        await NavigationService.PopAsync();
      }
      catch (Exception e) {
        ViewActions.DisplayAlert("Error:", "Can't post data", "Ok").SafeFireAndForget(true);
      }
    }
    #endregion
  }
}