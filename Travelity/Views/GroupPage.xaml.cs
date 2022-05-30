using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Travelity.Abstractions.Models;
using Travelity.Models;
using Travelity.ViewModel;
using Travelity.ViewModel.GroupViewModels;
using Travelity.Views.Add_Content.Popups;
using Travelity.Views.Content;
using Xamarin.CommunityToolkit.Extensions;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Travelity.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class GroupPage : ContentView
    {
        GroupViewModel groupViewModel = new GroupViewModel();
        public GroupPage()
        {
            InitializeComponent();
            this.BindingContext = groupViewModel;

            if (groupViewModel.Groups.Count() != 0)
            {
                App.Current.MainPage.DisplayAlert("Error", "Found Data", "OK");
            }
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
            //await App.Current.MainPage.DisplayAlert("Add Travel Group", "Coming Soon!", "OK");
            var result = await Navigation.ShowPopupAsync(new AddGroupPopup()
            {
                BackgroundColor = Color.Transparent,
                Color = Color.Transparent
            });
            if (result.ToString() == "")
            {
                return;
            }
            else
            {
                var group = result as Group;
                groupViewModel.CreateGroup(group);
                // SnackBar
                var options = groupViewModel.SnackBar(group.groupName + " Has been Created");
                await Application.Current.MainPage.DisplaySnackBarAsync(options);
            }

        }
    }
}