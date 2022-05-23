using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Travelity.Models;
using Travelity.ViewModel;
using Travelity.ViewModel.GroupViewModels;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Travelity.Views.Content
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class TravelPage : ContentPage
    {
        public TravelPage()
        {
            InitializeComponent();
            
        }
        public TravelPage(GroupViewModel selectedGroup)
        {
            InitializeComponent();
            this.BindingContext = selectedGroup;
            this.OverViewPageView.BindingContext = selectedGroup;
        }
        private void BackButton(object sender, EventArgs e)
        {
            App.Current.MainPage.Navigation.PopAsync();
        }
    }
}