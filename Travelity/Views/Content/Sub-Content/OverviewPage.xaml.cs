using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Travelity.ViewModel.GroupViewModels;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Travelity.Views.Content.Sub_Content
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class OverviewPage : ContentView
    {
        GroupViewModel groupViewModel = new GroupViewModel();
        public OverviewPage()
        {
            InitializeComponent();
            this.BindingContext = groupViewModel;
        }

        private async void Invite_Friends(object sender, EventArgs e)
        {
          await App.Current.MainPage.DisplayAlert("Invite Friends", "Coming Soon!", "OK");
        }
    }
}