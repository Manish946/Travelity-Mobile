using System;
using System.Threading.Tasks;
using Travelity.Models;
using Travelity.Service;
using Travelity.ViewModel;
using Travelity.Views;
using Xamarin.Essentials;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Travelity
{
    public partial class App : Application
    {
        private GoogleUserViewModel GoogleUser;
        public App()
        {
            InitializeComponent();

            var CurrentUser = Preferences.Get("CurrentUsername", "");
            var HaveAccount = Preferences.Get("HaveAccount", "");
            if (CurrentUser == "" || CurrentUser == null || HaveAccount == "False")
            {
                MainPage = new NavigationPage(new LoginPage()) { };

            }
            else
            {
                GoogleUser = new GoogleUserViewModel();
                GoogleUser.CheckUserLoggedIn();
                MainPage = new NavigationPage(new MainPage()) { };

            }



            //    MainPage = new NavigationPage(new MainPage())
            //    {
            //        BarBackgroundColor = Color.Transparent               

            //    };
        }


        protected override void OnStart()
        {

        }

        protected override void OnSleep()
        {
        }

        protected override void OnResume()
        {
        }
    }
}
