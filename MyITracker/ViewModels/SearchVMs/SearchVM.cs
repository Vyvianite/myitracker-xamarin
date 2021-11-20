using System;
using System.Collections.Generic;
using Xamarin.Forms;
using System.Threading.Tasks;

using Newtonsoft.Json;

namespace MyITracker.ViewModels {

  public class SearchVM : BaseVM {
    #region Properties
    private readonly DashboardVM ParentVM;
    private readonly string SearchTerm;

    public Observable<Search> SearchResults { get; } = new Observable<Search>();
    #endregion

    #region Constructor
    public SearchVM(DashboardVM parentVM, string searchTerm) {
      ParentVM = parentVM;
      SearchTerm = searchTerm;
    }
    #endregion

    #region Commands
    Command editTicketNote;
    public Command EditTicketNote {
      get => editTicketNote ??= new Command(async (item) => {
        var vm = new EditTicketNoteVM(ParentVM, (TicketNote)item);
        await NavigationService.PushAsync(vm);
      });
    }

    Command editCustomerNote;
    public Command EditCustomerNote {
      get => editCustomerNote ??= new Command(async (item) => {
        var vm = new EditCustomerNoteVM(ParentVM, (CustomerNote)item);
        await NavigationService.PushAsync(vm);
      });
    }
    #endregion

    #region Methods
    public override async Task InitializeAsync() {
      try {
        SearchResults.Set(await ResToExc(SearchRepo.search(SearchTerm)));
      }
      catch (Exception e) {
        ViewActions.DisplayAlert("Error:", "Cannot access server data", "Ok").SafeFireAndForget(true);
        await NavigationService.PopAsync();
      }
    }
    #endregion
  }
}
