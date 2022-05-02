using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Travelity.Models;
using Travelity.Views;
using Xamarin.Forms;
using Xamarin.CommunityToolkit.Extensions;
using System.Diagnostics;

namespace Travelity
{
    public partial class MainPage : ContentPage
    {
        
        public MainPage()
        {
            InitializeComponent();

            
        }

        
        private void OnFabTabTapped(object sender, Xamarin.CommunityToolkit.UI.Views.TabTappedEventArgs e)
        {
            DisplayAlert("Create Post", "Coming Soon!", "OK");
        }
        private bool isStarted = false;
        protected override void LayoutChildren(double x, double y, double width, double height)
        {
            base.LayoutChildren(x, y, width, height);
            if (!isStarted)
            {
                isStarted = true;
                // do something
                // App.Current.MainPage.DisplayAlert("Error", "PAGE fullly loaded", "OK");

            }
        }
        protected override bool OnBackButtonPressed()
        {
            Device.BeginInvokeOnMainThread(async () =>
            {
                var res = await this.DisplayAlert("Do you really want to exit the application?", "", "Yes", "No").ConfigureAwait(false);

                if (res) System.Diagnostics.Process.GetCurrentProcess().CloseMainWindow();
            });
            return true;
        }
    }
}
