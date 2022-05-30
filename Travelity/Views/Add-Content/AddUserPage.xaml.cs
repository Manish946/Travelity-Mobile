using Plugin.Media;
using Plugin.Media.Abstractions;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Travelity.Abstractions.Models;
using Travelity.ViewModel;
using Travelity.ViewModel.UserViewModels;
using Xamarin.CommunityToolkit.Extensions;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Travelity.Views.Add_Content
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class AddUserPage : ContentPage
    {
        private GoogleUserViewModel GoogleUser;
        //public User User { get; set; }
        MediaFile file;

        private AddUserViewModel addUserViewModel = new AddUserViewModel();
        public AddUserPage()
        {
            InitializeComponent();
            BindingContext = addUserViewModel;
            GoogleUser = new GoogleUserViewModel();

        }

        private void GoogleLogout_Button(object sender, EventArgs e)
        {
            this.Navigation.PushAsync(new LoginPage());

        }
        private async void ChangeProfile(object sender, EventArgs e)
        {
            await CrossMedia.Current.Initialize();
            try
            {
                file = await CrossMedia.Current.PickPhotoAsync(new PickMediaOptions
                {
                    PhotoSize = PhotoSize.Full
                });
                if (file == null)
                {
                    return;
                }
                else
                {
                    //  await firestorageDB.DeleteProfilePicture(editUserViewModel.PreviousProfilePicture());

                    addUserViewModel.ChangeProfilePicture(file.GetStream(), Path.GetFileName(file.Path));
                }

            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.Message);

            }

        }

    }
}