using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Travelity.Abstractions.Models;
using Travelity.ViewModel;
using Travelity.ViewModel.UserViewModels;
using Xamarin.CommunityToolkit.Extensions;
using Xamarin.CommunityToolkit.UI.Views;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Travelity.Views.Add_Content.Popups
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class AddFriendPopup : Popup
    {
        UserViewModel userViewModel = new UserViewModel();
        public ICommand ResponseRequest { get; }
        public AddFriendPopup()
        {
            InitializeComponent();
            this.BindingContext = userViewModel;
            userViewModel.AddFriendState = LayoutState.Empty;
            ResponseRequest = new Command(ResponseFriendRequest);
        }

        private void ResponseFriendRequest(object obj)
        {
            Dismiss("");
        }

        private void SearchFriend_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (FriendSearch.Text != "")
            {
                var searchFriendResult = userViewModel.GlobalUsers.Where(user => user.fullName.ToLower().Contains(FriendSearch.Text.ToLower()) && user.username != userViewModel.CurrentUsername);

                //var SentFriendRequest = userViewModel.SentFriendRequests;
                //var Users = new List<User>();
                //for (int i = 0; i < SentFriendRequest.Count; i++)
                //{
                //    IEnumerable<User> a = searchFriendResult.Where(user => user.username == SentFriendRequest[i].Sender);
                //    Users.AddRange(a);
                //}
                if (searchFriendResult.Count() == 0)
                {
                    userViewModel.AddFriendState = LayoutState.Empty;

                }
                else
                {
                    userViewModel.AddFriendState = LayoutState.None;

                    UserCollectionView.ItemsSource = searchFriendResult;
                }
               

            }
            else
            {
                userViewModel.AddFriendState = LayoutState.Empty;
            }

        }


    }
}