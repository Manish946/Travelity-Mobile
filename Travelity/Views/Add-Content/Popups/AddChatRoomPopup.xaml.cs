using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Travelity.Abstractions.Models;
using Travelity.Service.FirebaseService;
using Travelity.ViewModel.ChatViewModels;
using Travelity.ViewModel.FriendsViewModels;
using Travelity.ViewModel.UserViewModels;
using Travelity.Views.Content;
using Xamarin.CommunityToolkit.Extensions;
using Xamarin.CommunityToolkit.UI.Views;
using Xamarin.Essentials;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Travelity.Views.Add_Content.Popups
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class AddChatRoomPopup : Popup
    {
        FriendViewModel friendViewModel = new FriendViewModel();
        FirebaseDB firebaseDB = new FirebaseDB();

        public AddChatRoomPopup()
        {
            InitializeComponent();
            this.BindingContext = friendViewModel;
            //if (UserCollectionView.ItemsSource.Cast<User>().Count() == 0)
            //{
            //    friendViewModel.ChatRoomState = LayoutState.Empty;

            //}
            //else
            //{
            //    friendViewModel.ChatRoomState = LayoutState.None;

            //}
        }


        private void SearchFriend_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (FriendSearch.Text != "")
            {
                IEnumerable<User> searchFriendResult = friendViewModel.FriendsCollection.Where(user => user.fullName.ToLower().Contains(FriendSearch.Text.ToLower()));


                if (searchFriendResult.Count() == 0)
                {
                    friendViewModel.ChatRoomState = LayoutState.Empty;

                }
                else
                {
                    friendViewModel.ChatRoomState = LayoutState.None;

                    UserCollectionView.ItemsSource = searchFriendResult;
                }

            }
            else
            {
                IEnumerable<User> searchFriendResult = friendViewModel.FriendsCollection;

                if (searchFriendResult.Count() == 0)
                {
                    friendViewModel.ChatRoomState = LayoutState.Empty;

                }
                else
                {
                    friendViewModel.ChatRoomState = LayoutState.None;
                    UserCollectionView.ItemsSource = searchFriendResult;

                }
            }

        }

        private async void CreateChatRoom(object sender, EventArgs e)
        {
            var button = (Button)sender;
            var friend = button.BindingContext as User;
            if (friend != null)
            {
                var ChatRoom = await firebaseDB.ChatRoomExists(friend.username, friendViewModel.CurrentUsername);

                if (ChatRoom == null)
                {

                    Dismiss(friend);

                }
                else
                {
                    Dismiss("");
                    Preferences.Set("RoomProp", ChatRoom.Key);
                    await Application.Current.MainPage.Navigation.PushAsync(new ChatPage());

                }

            }

        }

        protected override object GetLightDismissResult()
        {
            return "Cancel";
        }
    }
}