using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Travelity.Abstractions.Models;
using Travelity.Models.Chat;
using Travelity.Service.FirebaseService;
using Travelity.ViewModel.ChatViewModels;
using Travelity.Views.Add_Content;
using Travelity.Views.Add_Content.Popups;
using Travelity.Views.Content;
using Xamarin.CommunityToolkit.Extensions;
using Xamarin.Essentials;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Travelity.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class ChatRoomPage : ContentView
    {
        ChatRoomViewModel ChatRoomVM = new ChatRoomViewModel();
        FirebaseDB fireBaseDB = new FirebaseDB();
        public ChatRoomPage()
        {
            InitializeComponent();
            this.BindingContext = ChatRoomVM;
        }

        private async  void Add_NewChat_Button(object sender, EventArgs e)
        {
           var result = await Navigation.ShowPopupAsync(new AddChatRoomPopup()
            {
                BackgroundColor = Color.Transparent,
                Color = Color.Transparent

            });
            if(result.ToString() == "Cancel" || result.ToString() == "")
            {
                return;
            }
            else
            {
                var friend = result as User;
                ChatRoomVM.AddChatRoom(friend.username);

                // SnackBar
                var options = ChatRoomVM.SnackBar("New Chat Has Been Created With " + friend.fullName);
                await Application.Current.MainPage.DisplaySnackBarAsync(options);
            }
            //Navigation.ShowPopup(new AddChatRoomPopup()
            //{
            //    BackgroundColor = Color.Transparent,
            //    Color = Color.Transparent

            //});

            //ChatRoomVM.ScrollToLast(ListviewChatrooms);
        }

        private async void View_FriendsList(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new FriendsList());
        }
        private void ChatRoom_TextChanged(object sender, TextChangedEventArgs e)
        {
           if(ChatRoom_Search.Text != "")
            {
                var searchChatRoom = ChatRoomVM.ChatRooms.Where(chatroom => chatroom.Members[1].ToLower().Contains(ChatRoom_Search.Text.ToLower()));
                ListviewChatrooms.ItemsSource = searchChatRoom;
            }
            else
            {
                ListviewChatrooms.ItemsSource = ChatRoomVM.ChatRooms;

            }
        }

        private async void ChatRoomSelected(object sender, SelectedItemChangedEventArgs e)
        {
            if (ListviewChatrooms.SelectedItem != null)
            {
                ChatRoom SelectedChatRoom = (ChatRoom)ListviewChatrooms.SelectedItem;
                Preferences.Set("RoomProp", SelectedChatRoom.Key);

                await Navigation.PushAsync(new ChatPage());
               // MessagingCenter.Send<ChatRoomPage, ChatRoom>(this, "RoomProp", SelectedChatRoom);
            }
        }
    }
}