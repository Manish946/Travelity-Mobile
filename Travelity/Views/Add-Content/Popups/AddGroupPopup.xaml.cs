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
using Travelity.ViewModel.GroupViewModels;
using Xamarin.CommunityToolkit.UI.Views;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Travelity.Views.Add_Content.Popups
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class AddGroupPopup : Popup
    {
        GroupViewModel groupViewModel = new GroupViewModel();
        MediaFile file;
        public Group NewGroup { get; set; }
        public AddGroupPopup()
        {
            InitializeComponent();
            this.BindingContext = groupViewModel;
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

                    groupViewModel.ChangeGroupPicture(file.GetStream(), Path.GetFileName(file.Path));
                }

            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.Message);

            }
        }

        private void CreateGroup(object sender, EventArgs e)
        {
           
                Dismiss("");
            
        }
    }
}