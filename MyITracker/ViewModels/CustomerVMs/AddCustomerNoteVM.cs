using System;
using System.Threading.Tasks;
using System.Linq;
using Xamarin.Forms;
using System.Collections.Generic;


namespace MyITracker.ViewModels {

  public class AddCustomerNoteVM : BaseVM {
    #region Properties
    private readonly string CID;
    private readonly IRefreshable ParentVM;

    public string Label { get; set; }
    public string Body { get; set; }
    #endregion

    public AddCustomerNoteVM(IRefreshable parentVM, string cid) {
      CID = cid;
      ParentVM = parentVM;
    }

    #region Commands
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

    public async Task UploadData() {
      if (new HashSet<object> {
          Label,
          Body
        }.NullCheck()) {
        ViewActions.DisplayAlert("Error:", "Fill all entries", "Ok").SafeFireAndForget(true);
        return;
      }

      try {
        var newCust = new CustomerNote("", "", CID, Label, Body, "", "");
        string response = await ResToExc(CustomerRepo.Note.insert(newCust)); //todo validate response
        ParentVM.RefreshData().SafeFireAndForget(true);
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
