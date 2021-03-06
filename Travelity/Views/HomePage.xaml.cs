using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Travelity.ViewModel;
using Travelity.Views.Content;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Travelity.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class HomePage : ContentView
    {
        MainViewModel mainViewModel = new MainViewModel();
        public HomePage()
        {
            InitializeComponent();
            this.BindingContext = mainViewModel;
            refreshContent();
        }

        private async void refreshContent()
        {
          await  mainViewModel.RefreshCurrentUser();

        }

        private void ProfileButton_Click(object sender, EventArgs e)
        {
            this.Navigation.PushAsync(new ProfilePage());
        }

        async void CarouselView_GroupSelected(object sender, EventArgs e)
        {
            GroupViewModel currentGroup = Group_CarouselView.CurrentItem as GroupViewModel;

            if (currentGroup == null)
            {
                return;

            }
            else
            {

                await Navigation.PushAsync(new TravelPage(currentGroup));
                //((CollectionView)sender).SelectedItem = null;
            }
        }
    }
}