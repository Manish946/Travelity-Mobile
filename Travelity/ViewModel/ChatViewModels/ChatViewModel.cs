using MvvmHelpers.Commands;
using PropertyChanged;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Travelity.Models.Chat;
using Travelity.Service.FirebaseService;
using Travelity.ViewModel.UserViewModels;
using Xamarin.CommunityToolkit.UI.Views;
using Xamarin.Essentials;
using Xamarin.Forms;

namespace Travelity.ViewModel.ChatViewModels
{
    [AddINotifyPropertyChangedInterface]

    public class ChatViewModel : BaseTravelityViewModel, IHasListViewModel
    {
        FirebaseDB firebaseDB = new FirebaseDB();

        public ObservableCollection<Message> Messages { get; set; }
        UserViewModel userViewModels = new UserViewModel();
        public LayoutState MainState { get; set; }
        private string ChatRoomKey { get; set; }
        public IHasListView View { get; set; }

        public ChatViewModel()
        {
            ChatRoomKey = Preferences.Get("RoomProp", "");

            Messages = new ObservableCollection<Message>();
            GetChatMessages();
            //  Messages.CollectionChanged += EventHandler(ScrolltoLast);
        }

        private void ScrolltoLast()
        {
            Device.BeginInvokeOnMainThread(() =>
            {
                try
                {
                    if (View.ListView.ItemsSource != null && View.ListView.ItemsSource.Cast<object>().Count() > 0)
                    {
                        var message = View.ListView.ItemsSource.Cast<object>().LastOrDefault();
                        if (message != null)
                        {
                            View.ListView.ScrollTo(message, ScrollToPosition.End, false);
                        }
                    }
                }
                catch (Exception ex)
                {

                    App.Current.MainPage.DisplayAlert("Error", ex.Message, "OK");

                }
            });
        }

        public async void SendMessage(Message Message)
        {
            await firebaseDB.SendMessage(Message, ChatRoomKey);
            ScrolltoLast();

        }

        public async void GetChatMessages()
        {
            Messages = await Task.Run(() => firebaseDB.GetChatRoomMessages(ChatRoomKey));

            await Task.Delay(1000);
            ScrolltoLast();


        }


    }
}
