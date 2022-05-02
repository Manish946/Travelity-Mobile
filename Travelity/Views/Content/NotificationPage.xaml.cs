using PropertyChanged;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Travelity.ViewModel.UserViewModels;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Travelity.Views.Content
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    [AddINotifyPropertyChangedInterface]

    public partial class NotificationPage : ContentPage, INotifyPropertyChanged
    {
        UserViewModel userViewModel = new UserViewModel();
        public NotificationPage()
        {
            InitializeComponent();

        }
        public NotificationPage(int NotificationId)
        {
            InitializeComponent();
            this.Focus();
            if (NotificationId == 1)
            {
                NotificationTabView.SelectedIndex = 1;

            }
            else
            {
                NotificationTabView.SelectedIndex = 0;

            }
        }
        private void BackButton(object sender, EventArgs e)
        {
            App.Current.MainPage.Navigation.PopAsync();
        }
    }
}