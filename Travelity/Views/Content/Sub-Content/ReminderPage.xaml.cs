using Firebase.Storage;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Travelity.ViewModel.UserViewModels;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Travelity.Views.Content.Sub_Content
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class ReminderPage : ContentView
    {


        public ReminderPage()
        {
            InitializeComponent();

        }

        private async void Button_Clicked(object sender, EventArgs e)
        {
            Xamarin.Essentials.FileResult photo = await Xamarin.Essentials.MediaPicker.PickPhotoAsync();

            if (photo != null)
            {
                FirebaseStorageTask task = new FirebaseStorage("travelity-auth.appspot.com", new FirebaseStorageOptions
                {
                    ThrowOnCancel = true,
                    
                }).Child("ProfilePicture").Child("manishestha").Child(photo.FileName).PutAsync(await photo.OpenReadAsync());
                task.Progress.ProgressChanged += (sender, args) =>
                {
                    ProgressBar.Progress = args.Percentage;
                    ProgressRingBar.Progress = args.Percentage;
                    
                };

                var donwloadLink = await task;
                downloadLink.Text = donwloadLink;
            }
            else
            {
                return;
            }
        }
    }
}