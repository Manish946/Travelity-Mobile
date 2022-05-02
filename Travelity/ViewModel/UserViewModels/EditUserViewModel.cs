using MvvmHelpers.Commands;
using Plugin.Media.Abstractions;
using PropertyChanged;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Threading.Tasks;
using Travelity.Abstractions.Models;
using Travelity.Service;
using Travelity.Service.FirebaseService;
using Travelity.Views;
using Travelity.Views.Content;
using Xamarin.CommunityToolkit.Extensions;
using Xamarin.CommunityToolkit.UI.Views;
using Xamarin.CommunityToolkit.UI.Views.Options;
using Xamarin.Essentials;

namespace Travelity.ViewModel.UserViewModels
{
    [AddINotifyPropertyChangedInterface]

    public class EditUserViewModel : BaseUserViewModel
    {
        public Command SaveEditCommand { get; }
        private User user;
        public LayoutState MainState { get; set; }
        private MainViewModel mainViewModel;
        public LayoutState UploadState { get; set; }
        public bool OnSucessAnimation { get; set; }
        public double GridOpacity { get; set; }
        public EditUserViewModel()
        {
            SaveEditCommand = new Command(OnEdit);
            user = new User();
            CurrentUsername = Preferences.Get("CurrentUsername", "");
            userService = Xamarin.Forms.DependencyService.Get<IUserService>();
            CurrentUser = Task.Run(() => Client.GetUserByUsername(CurrentUsername)).Result;
            mainViewModel = new MainViewModel();
            OnSucessAnimation = false;
            UploadState = LayoutState.None;

            GridOpacity = 1;
        }

        private async void OnEdit(object obj)
        {
            user = CurrentUser;

            if (user.password == null)
            {
                await App.Current.MainPage.DisplayAlert("Error", "Password is required.", "OK");
                await Xamarin.Forms.Application.Current.MainPage.Navigation.PushAsync(new EditProfilePage());
            }
            else 
            {
                user.fullName = user.firstName + " " + user.lastName;
                await Client.UpdateUser(user.id, user);
                // Updating Local Database of User.
                User UpdatedUser = await Client.GetUserByUsername(user.username);
                await userService.updateCurrentUser(UpdatedUser);
                await userService.AddCurrentUser(UpdatedUser);
                OnSucessAnimation = true;
                GridOpacity = 0.1;
                await Task.Delay(1000);
                SnackBarOptions option = SnackBar("User Has Been Updated");
                await App.Current.MainPage.DisplaySnackBarAsync(option);
                await Xamarin.Forms.Application.Current.MainPage.Navigation.PushAsync(new ProfilePage());
            }
        }

        public async void ChangeProfilePicture(Stream mediaFile, string path)
        {
            UploadState = LayoutState.Loading;
            Task<string> downloadableImage = fireStorageDB.UploadProfilePicture(mediaFile, path);
            if (await downloadableImage != null)
            {
                user = CurrentUser;
                user.profilePicture = await downloadableImage;
                await Client.UpdateUser(user.id, user);
                User UpdatedUser = await Client.GetUserByUsername(user.username);
                await userService.updateCurrentUser(UpdatedUser);
                await userService.AddCurrentUser(UpdatedUser);
                UploadState = LayoutState.None;

            }
            else
            {
                return;
            }
        }
        //public string PreviousProfilePicture()
        //{
        //    user = CurrentUser;
        //    return user.profilePicture+" Profile.png";
        //}
    }
}
