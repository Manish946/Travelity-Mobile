using MvvmHelpers;
using MvvmHelpers.Commands;
using PropertyChanged;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Travelity.Abstractions.Models;
using Xamarin.CommunityToolkit.UI.Views;
using Xamarin.Essentials;

namespace Travelity.ViewModel.FriendsViewModels
{
    [AddINotifyPropertyChangedInterface]
    public class FriendViewModel : BaseTravelityViewModel
    {
        public ObservableRangeCollection<User> FriendsCollection { get; set; }
        public AsyncCommand LoadUserFriendsCommand { get; }
        public AsyncCommand RefreshCommand { get; }

        private bool isBusy { get; set; }
        public LayoutState MainState { get; set; }
        public LayoutState ChatRoomState { get; set; }


        public FriendViewModel()
        {
            CurrentUsername = Preferences.Get("CurrentUsername", "");
            FriendsCollection = new ObservableRangeCollection<User>();
            LoadFriends();
            RefreshCommand = new AsyncCommand(Refresh);

        }

        private async void LoadFriends()
        {
            //var Friends = await Client.GetUserFriends(CurrentUsername);
            //FriendsCollection.AddRange(Friends);
            FriendsCollection = await Client.GetUserFriends(CurrentUsername);
            if (FriendsCollection.Count() == 0)
            {
               MainState = LayoutState.Empty;
               ChatRoomState = LayoutState.Empty;
            }
            else
            {
                MainState = LayoutState.None;
                ChatRoomState = LayoutState.None;

            }
            // FriendsList = await Client.GetUserFriends(CurrentUsername);
        }
        public bool IsFriend(string username)
        {
            User Friend = FriendsCollection.Where(friend=> friend.username == username).FirstOrDefault();
            if(Friend == null)
            {
                return false;
            }
            else
            {
                return true;
            }
            
        }


        private void RefreshView_Refreshing(object sender, EventArgs e)
        {

        }
        public async Task Refresh()
        {
            if (isBusy)
            {
                return;
            }
            isBusy = true;

            try
            {
                LoadFriends();

            }
            catch (Exception ex)
            {
                await App.Current.MainPage.DisplayAlert("Error", ex.Message, "OK");

            }
            finally
            {
                isBusy = false;
            }

        }
    }
}
