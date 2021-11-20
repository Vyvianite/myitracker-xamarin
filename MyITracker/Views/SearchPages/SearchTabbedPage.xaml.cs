using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace MyITracker.Views {
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class SearchTabbedPage : TabbedPage, IView {
        public SearchTabbedPage() {
            InitializeComponent();
        }
        public IViewModel ViewModel {
            get => (IViewModel)BindingContext;
            set => BindingContext = value;
        }
    }
}
