using Acr.UserDialogs;
using Prism.Commands;
using Prism.Mvvm;
using Prism.Services;
using PropertyChanged;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Travelity.Abstractions.Models;
using Travelity.Models;
using Travelity.Service;
using Travelity.Views;
using Travelity.Views.Add_Content;
using Xamarin.Essentials;
using Xamarin.Forms;

namespace Travelity.ViewModel
{
    [AddINotifyPropertyChangedInterface]

    public class GoogleUserViewModel : BaseTravelityViewModel
    {
        // Implementing Google Manager Interface.
        private readonly IGooglemanager _googleManager;
        // Creating new GoogleUser Object.
        private GoogleUser _googleUser;
        public bool isLoading { get; set; }
        public string LottieAnimationSource { get; set; }
        public bool ShowLoginAnimation { get; set; }
        public bool ShowGoogleAnimation { get; set; }
        public GoogleUser GoogleUser
        {
            get { return _googleUser; }
            set { _googleUser = value; }
        }



        // Delegate Command For buttons of Login and logout.
        public ICommand GoogleLoginCommand { get; }
        public ICommand GoogleLogoutCommand { get; }

        // Checks if the user is already Logged in.
        public bool IsLoggedIn { get; set; }

        public GoogleUserViewModel()
        {
            // DependencyService To fetch data.
            _googleManager = DependencyService.Get<IGooglemanager>();
            userService = DependencyService.Get<IUserService>();
            GoogleLoginCommand = new Command(GoogleLogin);
            GoogleLogoutCommand = new Command(GoogleLogout);
            ShowLoginAnimation = true;
            ShowGoogleAnimation = false;

        }

        private void GoogleLogin(object obj)
        {
            ShowLoginAnimation = true;
            ShowGoogleAnimation = false;
            _googleManager.Login(OnLoginComplete);

        }

        private void GoogleLogout(object obj)
        {
            _googleManager.Logout();
        }

        public void CheckUserLoggedIn()
        {
            ShowLoginAnimation = true;
            ShowGoogleAnimation = false;
            _googleManager.Login(AutoLogin);
        }
        public void NewUserLoggedIn()
        {
            ShowLoginAnimation = true;
            ShowGoogleAnimation = false;
            _googleManager.Login(OnLoginComplete);

        }
        // Checks if user has completed the login with Google Account.
        private async void OnLoginComplete(GoogleUser googleUser, string message)
        {
            if (googleUser != null)
            {
                // Here we will get the API from Google and we will assign  to our Object GoogleUser.
                GoogleUser = googleUser;
                CurrentGoogleUser = googleUser;
                IsLoggedIn = true;
                // Separating Google Email to create Travelity User name which will be an Unique ID.
                int IndexOfGoogleMail = GoogleUser.Email.IndexOf("@");
                string Username = GoogleUser.Email.Substring(0, IndexOfGoogleMail);
                CurrentUsername = Username;
                isLoading = true;

                // Storing Data to call globally using essentials
                Preferences.Set("CurrentUsername", CurrentUsername);
                Preferences.Set("Email", CurrentGoogleUser.Email);
                Preferences.Set("Name", CurrentGoogleUser.Name);
                Preferences.Set("Picture", CurrentGoogleUser.Picture);

                await userService.AddCurrentGoogleUser(googleUser);


                // IF USER EXISTS
                try
                {
                    User CheckUser = await Client.GetUserByUsername(Username);

                    if (CheckUser.username != null)
                    {
                        // LottieAnimationSource = "https://assets1.lottiefiles.com/packages/lf20_af77tbqx.json";
                        await userService.AddCurrentUser(CheckUser);
                        // UserDialogs.Instance.ShowLoading("Logging in...",MaskType.Gradient);
                        ShowLoginAnimation = false;
                        ShowGoogleAnimation = true;
                        if (ShowGoogleAnimation == true)
                        {
                            await Task.Delay(2900);

                        }
                        App.Current.MainPage = new NavigationPage(new MainPage());
                        Preferences.Set("HaveAccount", "True");

                        // UserDialogs.Instance.HideLoading();
                        // LottieAnimationSource = "";
                    }
                }
                catch (Exception)
                {
                    //await App.Current.MainPage.DisplayAlert("Successfully Logged In!", " Please fill out the remaining data.", "Create");

                    await Application.Current.MainPage.Navigation.PushAsync(new AddUserPage());
                    Preferences.Set("HaveAccount", "False");


                }






            }
            // If some error shows then Display alert will be triggered.
            else
            {
            }
        }

        // AUTO LOGIN METHOD
        private async void AutoLogin(GoogleUser googleUser, string message)
        {
            if (googleUser != null)
            {
                // Here we will get the API from Google and we will assign  to our Object GoogleUser.
                GoogleUser = googleUser;
                CurrentGoogleUser = googleUser;
                IsLoggedIn = true;
                // Separating Google Email to create Travelity User name which will be an Unique ID.
                int IndexOfGoogleMail = GoogleUser.Email.IndexOf("@");
                string Username = GoogleUser.Email.Substring(0, IndexOfGoogleMail);
                CurrentUsername = Username;
                isLoading = true;

                // Storing Data to call globally using essentials
                Preferences.Set("CurrentUsername", CurrentUsername);
                Preferences.Set("Email", CurrentGoogleUser.Email);
                Preferences.Set("Name", CurrentGoogleUser.Name);
                Preferences.Set("Picture", CurrentGoogleUser.Picture);

                await userService.AddCurrentGoogleUser(googleUser);


                // IF USER EXISTS
                try
                {
                    MvvmHelpers.ObservableRangeCollection<User> AllUsers = await Client.GetUsers();
                    var CheckUser = AllUsers.Where(user => user.username == CurrentUsername).FirstOrDefault();

                    if (CheckUser.username != null)
                    {
                        // LottieAnimationSource = "https://assets1.lottiefiles.com/packages/lf20_af77tbqx.json";
                        await userService.AddCurrentUser(CheckUser);
                        // UserDialogs.Instance.ShowLoading("Logging in...",MaskType.Gradient);
                        ShowLoginAnimation = false;
                        ShowGoogleAnimation = true;
                        
                        // UserDialogs.Instance.HideLoading();
                        // LottieAnimationSource = "";
                    }
                    else
                    {
                        await Application.Current.MainPage.Navigation.PushAsync(new AddUserPage());

                    }
                }
                catch (Exception)
                {
                    //await App.Current.MainPage.DisplayAlert("Successfully Logged In!", " Please fill out the remaining data.", "Create");

                    await Application.Current.MainPage.Navigation.PushAsync(new AddUserPage());

                }
                





            }
            // If some error shows then Display alert will be triggered.
            else
            {
            }
        }

    }
}
