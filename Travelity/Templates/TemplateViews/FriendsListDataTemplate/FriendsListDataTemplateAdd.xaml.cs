using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Travelity.Abstractions.Models;
using Travelity.ViewModel.UserViewModels;
using Xamarin.CommunityToolkit.Extensions;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Travelity.Templates.TemplateViews.FriendsListDataTemplate
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class FriendsListDataTemplateAdd : ViewCell
    {
        UserViewModel userViewModel = new UserViewModel();
        public FriendsListDataTemplateAdd()
        {
            InitializeComponent();
        }

        private async void SendRequestButton_FromAdd(object sender, EventArgs e)
        {
            var button = (Button)sender;

            if (button.Text != "Cancel")
            {
                var friend = button.BindingContext as User;
                if (friend != null)
                {
                    FriendRequest friendRequest = new FriendRequest();
                    friendRequest.Sender = userViewModel.CurrentUsername;
                    friendRequest.Receiver = friend.username;
                    friendRequest.Status = 0;
                    await userViewModel.SendFriendRequest(friendRequest);
                    button.Text = "Cancel";
                    button.BackgroundColor = Color.OrangeRed;
                    button.BorderColor = Color.OrangeRed;
                    // SnackBar
                    var options = userViewModel.SnackBar("Friend Requests Sent To " + friendRequest.Receiver);
                    await Application.Current.MainPage.DisplaySnackBarAsync(options);
                }
            }
            else
            {
                button.Text = "Add";
                button.BackgroundColor = Color.FromHex("#3c74d5");
                button.BorderColor = Color.FromHex("#3c74d5");
                userViewModel.GetSentFriendRequests();
                var friend = button.BindingContext as User;
                var UserfriendRequests = await userViewModel.Client.GetUserSentRequests(userViewModel.CurrentUsername);
                FriendRequest SentRequest = UserfriendRequests.Where(request => request.Sender == userViewModel.CurrentUsername && request.Receiver == friend.username).FirstOrDefault();
                await userViewModel.DeleteFriendRequest(SentRequest.Id);
                // SnackBar
                var options = userViewModel.SnackBar("Friend Requests To " + SentRequest.Receiver + " Has Been Canceled!");
                await Application.Current.MainPage.DisplaySnackBarAsync(options);

            }

        }
    }
}