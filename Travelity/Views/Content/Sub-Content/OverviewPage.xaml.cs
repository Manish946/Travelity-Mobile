using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Travelity.Abstractions.Models;
using Travelity.ViewModel.GroupViewModels;
using Travelity.Views.Add_Content.Popups;
using Xamarin.CommunityToolkit.Extensions;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Travelity.Views.Content.Sub_Content
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class OverviewPage : ContentView
    {
        GroupViewModel groupViewModel = new GroupViewModel();
        Group group = new Group();
        public OverviewPage()
        {
            InitializeComponent();

        }
        protected override void OnBindingContextChanged()
        {
            base.OnBindingContextChanged();

            groupViewModel = BindingContext as GroupViewModel;
            group = groupViewModel.Group;
        }

        private async void Invite_Friends(object sender, EventArgs e)
        {
            var result = await Navigation.ShowPopupAsync(new InviteFriendToGroupPopup()
            {
                BackgroundColor = Color.Transparent,
                Color = Color.Transparent

            });
            if (result.ToString() == "Cancel" || result.ToString() == "")
            {
                return;
            }
            else
            {
                var friend = result as User;
                groupViewModel.AddUserToGroup(friend, group);
                groupViewModel.Group.Users.Add(friend);
                //ChatRoomVM.AddChatRoom(friend.username);

                // SnackBar
                var options = groupViewModel.SnackBar(friend.fullName+" Added to the Group");
                await Application.Current.MainPage.DisplaySnackBarAsync(options);
            }
        }
    }
}