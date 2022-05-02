using MvvmHelpers;
using MvvmHelpers.Commands;
using PropertyChanged;
using Refit;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using Xamarin.CommunityToolkit.Extensions;
using System.Text;
using System.Threading.Tasks;
using Travelity.Abstractions.Models;
using Travelity.RefitAbstractions;
using Travelity.Service;
using Travelity.Views.Add_Content;
using Xamarin.Essentials;
using Xamarin.CommunityToolkit.UI.Views;

namespace Travelity.ViewModel.UserViewModels
{
    [AddINotifyPropertyChangedInterface]

    public class UserViewModel : BaseUserViewModel
    {
        static readonly string BaseAddress = DeviceInfo.Platform == DevicePlatform.Android ? "http://164.68.120.109:8010/api" : "http://164.68.120.109:8010/api";
        private bool isBusy;

        public Xamarin.Forms.INavigation Navigation { get; set; }
        public AsyncCommand LoadUsersCommand { get; }
        public ObservableRangeCollection<User> Users { get; set; }
        public ObservableRangeCollection<User> GlobalUsers { get; set; }
        public ObservableRangeCollection<User> UserFriendRequests { get; set; }
        public ObservableRangeCollection<FriendRequest> SentFriendRequests { get; set; }
        // private ITravelityApiClient client1;
        public LayoutState MainState { get; set; }
        public LayoutState AddFriendState { get; set; }

        public Command AddUserCommand { get; }
        public AsyncCommand RefreshFriendRequests { get; }
        public UserViewModel(Xamarin.Forms.INavigation navigation)
        {
            this.Navigation = navigation;
            LoadUsersCommand = new AsyncCommand(async () => await ExecuteLoadUserCommand());
            userService = Xamarin.Forms.DependencyService.Get<IUserService>();
            CurrentUsername = Preferences.Get("CurrentUsername", "");

            AddUserCommand = new Command(OnaddUserCommand);
            Users = new ObservableRangeCollection<User>();
            GlobalUsers = new ObservableRangeCollection<User>();
            UserFriendRequests = new ObservableRangeCollection<User>();

            SentFriendRequests = new ObservableRangeCollection<FriendRequest>();

            // GetAllUsers();
        }
        public UserViewModel()
        {
            GetAllUsers();
            CurrentUsername = Preferences.Get("CurrentUsername", "");
            GetUserFriendRequests();
            RefreshFriendRequests = new AsyncCommand(OnRefreshFriendRequests);

        }

        private async Task OnRefreshFriendRequests()
        {
            if (isBusy)
            {
                return;
            }
            isBusy = true;

            try
            {
                GetUserFriendRequests();


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


        public async void GetSentFriendRequests()
        {
            try
            {
                ObservableRangeCollection<FriendRequest> existsUser = new ObservableRangeCollection<FriendRequest>();
                await Task.Run(async ()=> existsUser = await Client.GetUserSentRequests(CurrentUsername));

                if (existsUser != null)
                {
                    var sentRequest = await Client.GetUserSentRequests(CurrentUsername);
                    if (sentRequest != null)
                    {
                        SentFriendRequests = await Client.GetUserSentRequests(CurrentUsername);
                    }
                }
            }
            catch(Exception ex)
            {
                return;
                // HANDLE  unhandled Exception.
            }
            
        }

        public async void AcceptFriendRequest(FriendRequest friendRequest)
        {
            try
            {
                // Create Friend for Current User
                Friend ReceiverFriend = new Friend()
                {
                    User_Username = friendRequest.Receiver,
                    Friend_Username = friendRequest.Sender
                };
                await Client.CreateFriend(ReceiverFriend);

                // Create Friend for Sender User
                Friend SenderFriend = new Friend()
                {
                    User_Username = friendRequest.Sender,
                    Friend_Username = friendRequest.Receiver
                };
                await Client.CreateFriend(SenderFriend);

                // Change Request to 1 as Accepted.
                friendRequest.Status = 1;
                await Client.UpdateFriendRequest(friendRequest.Id, friendRequest);

                // Refresh
                await OnRefreshFriendRequests();


            }
            catch
            {


            }
        }

        private async void GetUserFriendRequests()
        {
            try
            {
                if (await Client.GetUserFriendRequests(CurrentUsername) != null)
                {
                    ObservableRangeCollection<User> ReceiveRequest = await Client.GetUserFriendRequests(CurrentUsername);
                    if (ReceiveRequest != null)
                    {
                        UserFriendRequests = await Client.GetUserFriendRequests(CurrentUsername);
                        if (UserFriendRequests.Count() == 0)
                        {
                            MainState = LayoutState.Empty;
                        }
                        else
                        {
                            MainState = LayoutState.None;
                        }

                    }
                }


            }
            catch
            {


            }
        }

        public async Task SendFriendRequest(FriendRequest friendRequest)
        {

            await Client.SendFriendRequest(friendRequest);
        }
        public async Task DeleteFriendRequest(int id)
        {
            await Client.DeleteFriendRequest(id);
            // Refresh
            await OnRefreshFriendRequests();

        }
        public async Task<ObservableRangeCollection<FriendRequest>> GetUserReceiveRequests()
        {
            return await Client.GetUserReceiveRequests(CurrentUsername);

        }
        private async void GetAllUsers()
        {
            GlobalUsers = await Client.GetUsers();
            
        }

        private async void OnaddUserCommand(object obj)
        {
            await Navigation.PushAsync(new AddUserPage());

        }

        private async Task ExecuteLoadUserCommand()
        {
            IsBusy = true;

            try
            {
                Users.Clear();
                //var userList = await Client.GetUsers();
                //User testUser = await Client.GetUserByUsername("manishestha");
                //testUser.email = "EMAIL";
                //Users.Add(testUser);
                //  Users.ReplaceRange(userList);
                var user = await userService.GetCurrentUser();
                Users.Add(user);
            }
            catch (Exception ex)
            {
                await App.Current.MainPage.DisplayAlert("Error", ex.Message, "OK");

            }
            finally
            {
                IsBusy = false;
            }


        }
    }
}
