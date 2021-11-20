using System;
using System.Threading.Tasks;
using System.Linq;
using Xamarin.Forms;
using System.Collections.Generic;
using Microsoft.FSharp.Core;

namespace MyITracker.ViewModels {

  public class EditCustomerNoteVM : BaseVM {
    #region Properties
    private readonly IRefreshable parentVM;

    private string NoteId;

    private string version;

    public string Label { get; set; }
    public string Body { get; set; }
    #endregion

    public EditCustomerNoteVM(IRefreshable parentVM, CustomerNote note) {
      this.parentVM = parentVM;
      Note = note;
      version = note.Version;
      Label = note.Label;
      Body = note.Body;
    }

    #region Commands
    Command delete;
    public Command Delete {
      get => delete ??= new Command(async () => {
        var response = await ViewActions.DisplayAlert("Alert", "Are you sure?", "Yes", "Cancel");
        if (response) {
          await DeleteNote();
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
    #endregion

    #region Functions
    public override Task InitializeAsync() {
      return Task.CompletedTask;
    }

    public async Task DeleteNote() {
      try {
        var response = await ResToExc(CustomerRepo.Note.delete(Note.Index)); //todo validate response

        parentVM.RefreshData().SafeFireAndForget(true);
        await NavigationService.PopAsync();
      }
      catch (Exception e) {
        ViewActions.DisplayAlert("Error:", "Can't post data", "Ok").SafeFireAndForget(true);
      }
    }

    async Task UploadData() {
      if (new HashSet<object> {
          Note,
          Label
        }.NullCheck()) {
        ViewActions.DisplayAlert("Error:", "Fill all entries", "Ok").SafeFireAndForget(true);
        return;
      }

      try {
        var response = await ResToExc(CustomerRepo.Note.update(new CustomerNote(
          Note.Index,
          version,
          Note.ParentIndex,
          Note.Label,
          Note.Body,
          Note.Fullname,
          Note.Creator))); //todo validate response

        var merge = NoteModule.mergeResolver(response, Note.Body, Note.Version); //todo test creating fsoptions with null as param.

        if (merge.IsSome()) { //todo test this
          (Body, version) = merge.Value;
          return;
        }
        parentVM.RefreshData().SafeFireAndForget(true);
        ViewActions.DisplayAlert("Success", "Note was updated", "Ok").SafeFireAndForget(true);
        await NavigationService.PopAsync();
      }
      catch (Exception e) {
        ViewActions.DisplayAlert("Error:", "Can't post data", "Ok").SafeFireAndForget(true);
      }
    }
    #endregion
  }
}