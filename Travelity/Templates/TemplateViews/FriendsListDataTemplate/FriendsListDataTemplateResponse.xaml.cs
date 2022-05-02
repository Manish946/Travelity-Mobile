using Rg.Plugins.Popup.Services;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Travelity.Views.Add_Content.Popups;
using Travelity.Views.Content;
using Travelity.Views.Content.Notification_Content;
using Xamarin.CommunityToolkit.Extensions;
using Xamarin.CommunityToolkit.UI.Views;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Travelity.Templates.TemplateViews.FriendsListDataTemplate
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class FriendsListDataTemplateResponse : ViewCell
    {

        public FriendsListDataTemplateResponse()
        {
            InitializeComponent();
        }

        private async void ResponseRequest(object sender, EventArgs e)
        {
            
            await Application.Current.MainPage.Navigation.PushAsync(new NotificationPage(1));
        }
     
    }
}