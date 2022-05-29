using Firebase.Storage;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Essentials;

namespace Travelity.Service.FirebaseService
{
    public class FirestorageDB
    {
        FirebaseStorage firebaseStorage;
        public string CurrentUserName { get; set; }
        public FirestorageDB()
        {
            firebaseStorage = new FirebaseStorage("travelity-auth.appspot.com", new FirebaseStorageOptions
            {
                ThrowOnCancel = true,
            });
            CurrentUserName = Preferences.Get("CurrentUsername", "");
        }



        public async Task<string> UploadProfilePicture(Stream fileStream, string fileName)
        {
            try
            {

                var imageUrl = await firebaseStorage.Child("ProfilePicture").Child(CurrentUserName).Child(CurrentUserName+" Profile.png").PutAsync(fileStream);
                
                return imageUrl;

            }
            catch (Exception)
            {

                throw;
            }


        }

        public async Task<string> UploadGroupPicture(Stream fileStream, string fileName)
        {
            try
            {

                var imageUrl = await firebaseStorage.Child("Groups").Child("GroupsThumbnail").Child(fileName + "Group.png").PutAsync(fileStream);

                return imageUrl;

            }
            catch (Exception)
            {

                throw;
            }


        }


        //// Delete profile picture from fire store.
        //public async Task DeleteProfilePicture(string fileName)
        //{
        //    if (fileName != null)
        //    {
        //        try
        //        {

        //            await firebaseStorage.Child("ProfilePicture").Child(CurrentUserName).Child(fileName).DeleteAsync();

        //        }
        //        catch (Exception ex)
        //        {

        //            await App.Current.MainPage.DisplayAlert("Error", ex.Message, "OK");

        //        }

        //    }
        //    else
        //    {
        //        return;
        //    }
        //}
    }
}
