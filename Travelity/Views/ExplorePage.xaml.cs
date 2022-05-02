using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Travelity.Views.Content;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using Travelity.ViewModel;
using Travelity.Models;

namespace Travelity.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class ExplorePage : ContentView
    {
        public ExplorePage()
        {
            InitializeComponent();
           
        }

        private void ProfileButton_Click(object sender, EventArgs e)
        {
            this.Navigation.PushAsync(new ProfilePage());
           
            
        }

        private void LikeButton_Click(object sender, EventArgs e)
        {                                                                                                                                      
            var content = (ImageButton)sender;
            var currentImage = content.Source.ToString();
            var likedImage = "icon_favorite.png";
            var UnlikedImage = "icon_favorite_outlined.png";
            if (currentImage.Contains(UnlikedImage) == true)
            {
                content.Source = likedImage;

            }
            else if (currentImage.Contains(likedImage)==true)
            {
                content.Source = UnlikedImage;

            }

           


        }

        private void Notification_Click(object sender, EventArgs e)
        {
            Navigation.PushAsync(new NotificationPage());
        }
    }
}