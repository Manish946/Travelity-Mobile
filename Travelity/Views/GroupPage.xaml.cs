using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Travelity.Models;
using Travelity.ViewModel;
using Travelity.Views.Content;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Travelity.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class GroupPage : ContentView
    {
        public GroupPage()
        {
            InitializeComponent();
        }



        async void CollectionView_GroupSelected(object sender, SelectionChangedEventArgs e)
        {
            if (!(e.CurrentSelection.FirstOrDefault() is GroupViewModel currentGroup))
            {
                return;

            }
            else
            {

                await Navigation.PushAsync(new TravelPage(currentGroup));
                ((CollectionView)sender).SelectedItem = null;
            }

        }

        private async void Add_NewTravel_Button(object sender, EventArgs e)
        {
            await App.Current.MainPage.DisplayAlert("Add Travel Group", "Coming Soon!", "OK");

        }
    }
}