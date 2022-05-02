using MvvmHelpers;
using PropertyChanged;
using Refit;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using Travelity.Abstractions.Models;
using Travelity.Models;
using Travelity.RefitAbstractions;
using Travelity.Service;
using Travelity.Service.FirebaseService;
using Xamarin.CommunityToolkit.UI.Views.Options;
using Xamarin.Essentials;
using Xamarin.Forms;
namespace Travelity.ViewModel
{
    // This is a Base view model which will be used to pass function and variable that should be used multiple times.
    /// A base view model that fires Property Changed events as needed
    [AddINotifyPropertyChangedInterface]
    public class BaseTravelityViewModel : BindableObject
    {
        // Firebase Client
        public FirebaseDB fireBaseDB = new FirebaseDB();
        public FirestorageDB fireStorageDB = new FirestorageDB();


        // Travlity Web Api URL.
        static readonly string BaseAddress = DeviceInfo.Platform == DevicePlatform.Android ? "http://164.68.120.109:8020/api" : "http://164.68.120.109:8020/api";
        // Using Refit to call our Api with baseAdrress as Client.
        private ITravelityApiClient client = RestService.For<ITravelityApiClient>(BaseAddress);
        IUserService UserService;


        public IUserService userService
        {
            get { return UserService; }
            set { UserService = value; }
        }
        public ITravelityApiClient Client
        {
            get { return client; }
            set { client = value; }
        }
        // Current User
        public GoogleUser CurrentGoogleUser { get; set; } 
        public string CurrentUsername { get;  set; }

        //public GoogleUser CurrentGoogleUser
        //{
        //    get { return currentGoogleUser; }
        //    set { SetProperty(ref currentGoogleUser, value); }
        //}
        public User CurrentUser { get; set; }

        // Boolean to check if thread is busy while calling API.

        public bool IsBusy{get;set;}

        //public event PropertyChangedEventHandler PropertyChanged;

        public SnackBarOptions SnackBar(string message)
        {
            var messageText = new MessageOptions()
            {
                Padding = 10,
                Message = message
            };
            var actions = new SnackBarActionOptions
            {
                Text = "OK",
                ForegroundColor = Color.DarkGray
            };

            var options = new SnackBarOptions
            {
                MessageOptions = new MessageOptions
                {
                    Foreground = Color.White,
                    Message = messageText.Message,
                    Padding = 10

                },
                BackgroundColor = Color.FromHex("#191D2D"),
                CornerRadius = 40,
                Duration = TimeSpan.FromSeconds(1),
                Actions = new[] {actions}
                

            };
            return  options;
        }


    }
}
