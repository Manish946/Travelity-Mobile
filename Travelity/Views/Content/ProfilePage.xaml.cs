using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Travelity.Models;
using Travelity.Service;
using Travelity.ViewModel;
using Xamarin.CommunityToolkit.Extensions;
using Xamarin.Essentials;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Travelity.Views.Content
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class ProfilePage : ContentPage
    {
        private readonly IGooglemanager _googleManager;
        private readonly IUserService userService;
        private MainViewModel mainViewModel;
        public ProfilePage()
        {
            InitializeComponent();
            _googleManager = DependencyService.Get<IGooglemanager>();
            userService = DependencyService.Get<IUserService>();
            mainViewModel = new MainViewModel();
            this.BindingContext = mainViewModel;
            this.Appearing += LoadProfilePage_Appearing;
        }

        private async void LoadProfilePage_Appearing(object sender, EventArgs e)
        {
            await mainViewModel.RefreshCurrentUser();
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();
            BindingContext = mainViewModel;
        }
        private async void BackButton(object sender, EventArgs e)
        {
           App.Current.MainPage.Navigation.PushAsync(new MainPage());
           // await App.Current.MainPage.Navigation.PopAsync();

        }
        protected override bool OnBackButtonPressed()
        {
            Device.BeginInvokeOnMainThread(async () =>
            {
                await App.Current.MainPage.Navigation.PushAsync(new MainPage());


            });
            return true;
        }
        private void EditButton(object sender, EventArgs e)
        {
            this.Navigation.PushAsync(new EditProfilePage());

        }

        private async void Logout_button(object sender, EventArgs e)
        {
            _googleManager.Logout();
            // Clear Cache
            await userService.RemoveCurrentUser();
            await userService.RemoveCurrentGoogleUser();
            Preferences.Set("CurrentUsername", "");

            var options = mainViewModel.SnackBar("You have been Logged Out");
            await this.DisplaySnackBarAsync(options);
            App.Current.MainPage = new NavigationPage(new LoginPage());

        }

        private async void snackbar_button(object sender, EventArgs e)
        {
            var options = mainViewModel.SnackBar("Testing Snackbar");
            await this.DisplaySnackBarAsync(options);

        }
    }
}