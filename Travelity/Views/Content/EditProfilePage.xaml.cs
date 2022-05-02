using Firebase.Storage;
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
using Travelity.Service.FirebaseService;
using Travelity.ViewModel.UserViewModels;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Travelity.Views.Content
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class EditProfilePage : ContentPage
    {
        public User User { get; set; }
        private EditUserViewModel editUserViewModel = new EditUserViewModel();
        MediaFile file;
        public EditProfilePage()
        {
            InitializeComponent();
            BindingContext = editUserViewModel;
        }

        private void SaveButton(object sender, EventArgs e)
        {
            //await App.Current.MainPage.Navigation.PopAsync();
            // await this.Navigation.PushAsync(new ProfilePage());
            // await Navigation.PushAsync(new ProfilePage());
        }

        private async void BackButton(object sender, EventArgs e)
        {
            await App.Current.MainPage.Navigation.PopAsync();
            //await Xamarin.Forms.Application.Current.MainPage.Navigation.PushAsync(new ProfilePage());

        }
        //protected override bool OnBackButtonPressed()
        //{
        //    Device.BeginInvokeOnMainThread(async () =>
        //    {
        //        await Xamarin.Forms.Application.Current.MainPage.Navigation.PushAsync(new ProfilePage());

        //    });
        //    return true;
        //}
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

                    editUserViewModel.ChangeProfilePicture(file.GetStream(), Path.GetFileName(file.Path));
                }

            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.Message);

            }




            //var photo = await Xamarin.Essentials.MediaPicker.PickPhotoAsync();
            //if (photo != null)
            //{

            //    var task = new FirebaseStorage("travelity-auth.appspot.com", new FirebaseStorageOptions
            //    {
            //        ThrowOnCancel = true,

            //    }).Child("ProfilePicture").Child(editUserViewModel.CurrentUsername).Child(photo.FileName).PutAsync(await photo.OpenReadAsync());


            //    var donwloadLink = await task;

            //    editUserViewModel.ChangeProfilePicture(donwloadLink);
            //}
            //else
            //{
            //    return;
            //}
        }


    }
}