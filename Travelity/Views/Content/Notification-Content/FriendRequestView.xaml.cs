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

namespace Travelity.Views.Content.Notification_Content
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class FriendRequestView : ContentView
    {
        UserViewModel userViewModel = new UserViewModel();
        public string USERREQUEST { get; set; }
        public FriendRequestView()
        {
            InitializeComponent();
            this.BindingContext = userViewModel;
            //userViewModel.GetSentFriendRequests();
        }

        private void BackButton(object sender, EventArgs e)
        {
            App.Current.MainPage.Navigation.PopAsync();
        }

        private async void Accept_FriendRequest(object sender, EventArgs e)
        {
            var button = (Button)sender;
            var Sender = button.BindingContext as User;
            if(Sender != null)
            {
                var UserFriendRequest = await userViewModel.GetUserReceiveRequests();
                var CurrentFriendRequest = UserFriendRequest.Where(user => user.Sender == Sender.username && user.Receiver == userViewModel.CurrentUsername).FirstOrDefault();
                if(CurrentFriendRequest != null)
                {

                     userViewModel.AcceptFriendRequest(CurrentFriendRequest);
                    // SnackBar
                    var options = userViewModel.SnackBar("Friend Requests Accepted From " + CurrentFriendRequest.Sender);
                    await App.Current.MainPage.DisplaySnackBarAsync(options);
                }
            }
        }

        private async void Decline_FriendRequest(object sender, EventArgs e)
        {
            var button = (Button)sender;
            var Sender = button.BindingContext as User;
            if (Sender != null)
            {
                var UserFriendRequest = await userViewModel.GetUserReceiveRequests();
                var CurrentFriendRequest = UserFriendRequest.Where(user => user.Sender == Sender.username && user.Receiver == userViewModel.CurrentUsername).FirstOrDefault();
                if (CurrentFriendRequest != null)
                {

                   await userViewModel.DeleteFriendRequest(CurrentFriendRequest.Id);
                    // SnackBar
                    var options = userViewModel.SnackBar("Friend Requests Declined From " + CurrentFriendRequest.Sender);
                    await App.Current.MainPage.DisplaySnackBarAsync(options);

                }
            }
        }
    }
}