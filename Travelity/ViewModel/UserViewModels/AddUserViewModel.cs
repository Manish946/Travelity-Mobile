using System;
using System.Collections.Generic;
using System.Text;
using Travelity.Abstractions.Models;
using Travelity.Models;
using Travelity.Views.Content.Sub_Content;
using Xamarin.Essentials;
using Xamarin.Forms;
using Xamarin.CommunityToolkit.Extensions;
using Xamarin.CommunityToolkit.UI.Views.Options;
using PropertyChanged;
using System.IO;
using Xamarin.CommunityToolkit.UI.Views;
using System.Threading.Tasks;

namespace Travelity.ViewModel.UserViewModels
{
    [AddINotifyPropertyChangedInterface]

    public class AddUserViewModel : BaseUserViewModel
    {
        public Command SaveCommand { get; }
        public Command CancelCommand { get; }
        private User user;
        private GoogleUserViewModel GoogleUser;
        public LayoutState UploadState { get; set; }
        public bool GoogleLoginAnimation { get; set; }
        public bool VisibleGrid { get; set; }
        public AddUserViewModel()
        {
            SaveCommand = new Command(onSave);
            SaveCommand.ChangeCanExecute();
            User = new User();
            CurrentGoogleUser = new GoogleUser();
            // Settings Data from Preferences.
            CurrentUsername = Preferences.Get("CurrentUsername", "");
            CurrentGoogleUser.Email = Preferences.Get("Email", "");
            CurrentGoogleUser.Name = Preferences.Get("Name", "");
            CurrentGoogleUser.Picture = Preferences.Get("Picture", "");
            GoogleUser = new GoogleUserViewModel();
            //
            user = User;
            // Setting Data from Direct Google API.
            user.email = CurrentGoogleUser.Email;
            user.username = CurrentUsername;
            user.profilePicture = CurrentGoogleUser.Picture;
            user.fullName = user.firstName + " " + user.lastName;
            // Animation
            GoogleLoginAnimation = false;
            VisibleGrid = true;
        }

        private async void onSave(object obj)
        {
            if (user.firstName == null || user.lastName == null || user.password == null)
            {
                await App.Current.MainPage.DisplayAlert("Register Error", "Please Fill Out Before Registering!", "OK");
                return;
            }
            else
            {
               
                await Client.CreateUser(user);
                SnackBarOptions option = SnackBar("User Has Been Registered");
                await App.Current.MainPage.DisplaySnackBarAsync(option);
                GoogleUser.NewUserLoggedIn();
                // Animation
                GoogleLoginAnimation = true;
                VisibleGrid = false;
                // await Application.Current.MainPage.Navigation.PushAsync(new MainPage());

            }
        }
        public async void ChangeProfilePicture(Stream mediaFile, string path)
        {
            UploadState = LayoutState.Loading;
            Task<string> downloadableImage = fireStorageDB.UploadProfilePicture(mediaFile, path);
            if (await downloadableImage != null)
            {
                user.profilePicture = await downloadableImage;
                UploadState = LayoutState.None;

            }
            else
            {
                return;
            }
        }

    }
}
