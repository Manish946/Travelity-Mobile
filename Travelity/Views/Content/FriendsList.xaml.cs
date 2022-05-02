using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Travelity.ViewModel.FriendsViewModels;
using Travelity.Views.Add_Content.Popups;
using Xamarin.CommunityToolkit.Extensions;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Travelity.Views.Content
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class FriendsList : ContentPage
    {
        FriendViewModel friendViewModel = new FriendViewModel();
        public FriendsList()
        {
            InitializeComponent();
            this.BindingContext = friendViewModel;
        }

        private void GoBack_Button(object sender, EventArgs e)
        {
            Navigation.PopAsync();
        }

        private async void RefreshView_Refreshing(object sender, EventArgs e)
        {
           await friendViewModel.Refresh();
            Friend_RefreshView.IsRefreshing = false;
        }

        private async void AddNewFriend_Button(object sender, EventArgs e)
        {
            await this.Navigation.ShowPopupAsync(new AddFriendPopup()
            {
                BackgroundColor = Color.Transparent,
                Color = Color.Transparent

            });
        }
    }
}