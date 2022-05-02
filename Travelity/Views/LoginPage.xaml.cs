using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Travelity.Models;
using Travelity.ViewModel;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Travelity.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class LoginPage : ContentPage
    {
        //// Implementing Google Manager Interface.
        //private readonly IGooglemanager _googleManager;

        //// Creating new GoogleUser Object.
        //GoogleUser GoogleUser = new GoogleUser();

        public string FirstName { get; set; }
        //// Checks if the user is already Logged in.
        //public bool IsLoggedIn { get; set; }
        private GoogleUserViewModel GoogleUser;
     
        public LoginPage()
        {
            // DependencyService To fetch data.
            //_googleManager = DependencyService.Get<IGooglemanager>();
            //CheckUserLoggedIn();
            InitializeComponent();
            GoogleUser = new GoogleUserViewModel();
            //GoogleUser.CheckUserLoggedIn();
           //GoogleUser.GoogleLoginCommand.Execute(GoogleUser);
            
        }

        protected override bool OnBackButtonPressed()
        {
            Device.BeginInvokeOnMainThread(async () =>
            {
                var res = await this.DisplayAlert("Do you really want to exit the application?", "", "Yes", "No").ConfigureAwait(false);

                if (res) System.Diagnostics.Process.GetCurrentProcess().CloseMainWindow();
            });
            return true;
        }

        //private void CheckUserLoggedIn()
        //{
        //    _googleManager.Login(OnLoginComplete);
        //}

        //// Checks if user has completed the login with Google Account.
        //private void OnLoginComplete(GoogleUser googleUser, string message)
        //{
        //   if(googleUser != null)
        //    {
        //        // Here we will get the API from Google and we will assign  to our Object GoogleUser.
        //        GoogleUser = googleUser;
        //        txtName.Text = GoogleUser.Name;
        //        txtEmail.Text = GoogleUser.Email;
        //        imgProfile.Source = GoogleUser.Picture;
        //        IsLoggedIn = true;
        //        this.Navigation.PushAsync(new MainPage()
        //        {
        //         //BarBackgroundColor = Color.Transparent               

        //        });

        //    }
        //    // If some error shows then Display alert will be triggered.
        //    else
        //    {
        //        DisplayAlert("Message", message, "OK");
        //    }
        //}

        //// Login Button Click Method to call Login method from Google Manager.
        //private void btnLogin_Clicked(object sender, EventArgs e)
        //{
        //    _googleManager.Login(OnLoginComplete);
        //}
        //// Logout Button Click Method to call Logout method from Google Manager.
        //private void btnLogout_Clicked(object sender, EventArgs e)
        //{
        //    _googleManager.Logout();
        //    txtName.Text = "Name : ";
        //    txtEmail.Text = "Email : ";
        //    imgProfile.Source = "";
        //}
    }
}