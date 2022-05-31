using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Travelity.Abstractions.Models;
using Travelity.ViewModel.FriendsViewModels;
using Xamarin.CommunityToolkit.UI.Views;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Travelity.Views.Add_Content.Popups
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class InviteFriendToGroupPopup : Popup
    {
        FriendViewModel friendViewModel = new FriendViewModel();
        public InviteFriendToGroupPopup()
        {
            InitializeComponent();
            this.BindingContext = friendViewModel;

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

        private void InviteFriends(object sender, EventArgs e)
        {
            var button = (Button)sender;
            var friend = button.BindingContext as User;
            if (friend != null)
            {
                    Dismiss(friend);
            }

        }

        protected override object GetLightDismissResult()
        {
            return "Cancel";
        }
    }
}