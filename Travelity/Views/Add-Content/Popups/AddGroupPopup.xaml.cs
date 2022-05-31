using dotMorten.Xamarin.Forms;
using Plugin.Media;
using Plugin.Media.Abstractions;
using System;
using System.Collections;
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
        private List<string> LocationList;
        public AddGroupPopup()
        {
            InitializeComponent();
            this.BindingContext = groupViewModel;
            GroupNameEntry.TextChanged += UpdatePreview;
            GetWorldCitiesFromFile();
        }

        private void GetWorldCitiesFromFile()
        {
           using(var location = typeof(AddGroupPopup).Assembly.GetManifestResourceStream("Travelity.Data.Location.worldcities.txt"))
            {
                LocationList = new StreamReader(location).ReadToEnd().Split('\n').Select(t=>t.Trim()).ToList();
            }
        }

        // Update live Group name Preview.
        private void UpdatePreview(object sender, TextChangedEventArgs e)
        {
            GroupPreview.Text = GroupNameEntry.Text;

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
                    var ChangedImage = groupViewModel.ChangeGroupPicture(file.GetStream(), Path.GetFileName(file.Path));
                    string NewImage = await ChangedImage;
                    image.Source = NewImage;
                }

            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.Message);

            }
        }

        private async void CreateGroup(object sender, EventArgs e)
        {
            
            if (groupViewModel.NewGroup.groupName == null || groupViewModel.NewGroup.destination == null)
            {
                await App.Current.MainPage.DisplayAlert("Create Group Error", "Please Fill Out Before Creating A Group!", "OK");

            }
            else
            {
                Dismiss(groupViewModel.NewGroup);
            }
        }

        protected override object GetLightDismissResult()
        {
            return "";
        }

        private void LocationTextChange(object sender, dotMorten.Xamarin.Forms.AutoSuggestBoxTextChangedEventArgs e)
        {
            AutoSuggestBox input = (AutoSuggestBox)sender;
            input.ItemsSource = GetSuggestions(input.Text);
        }

        private List<string> GetSuggestions(string text)
        {
            return string.IsNullOrWhiteSpace(text)?null: LocationList.Where(location=>location.StartsWith(text, StringComparison.InvariantCultureIgnoreCase)).ToList();
        }

        private void LocationQuerySubmitted(object sender, dotMorten.Xamarin.Forms.AutoSuggestBoxQuerySubmittedEventArgs e)
        {
            // Options to be able to picked location to another label or text.
        }
    }
}