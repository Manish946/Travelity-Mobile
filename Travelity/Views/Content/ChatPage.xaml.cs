using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Travelity.Models.Chat;
using Travelity.Service.FirebaseService;
using Travelity.ViewModel.ChatViewModels;
using Travelity.ViewModel.UserViewModels;
using Xamarin.CommunityToolkit.UI.Views;
using Xamarin.Essentials;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Travelity.Views.Content
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class ChatPage : ContentPage, IHasListView
    {

        UserViewModel userViewModel = new UserViewModel();
        private bool isFirstTimeAppearing = true;
        ChatViewModel chatViewModel = new ChatViewModel();


        ListView IHasListView.ListView => MessagesListView;

        public ChatPage()
        {
            InitializeComponent();
            this.BindingContext = chatViewModel;
            //MessagingCenter.Subscribe<ChatRoomPage, ChatRoom>(this, "RoomProp", async (page, ChatRoomData) =>
            //{
            //    chatRoom = ChatRoomData;

            //    await Task.Run(() =>
            //    {
            //        messages = firebaseDB.GetChatRoomMessages(chatRoom.Key);

            //    });
            //    MessagesListView.BindingContext = messages;
            //    MessagingCenter.Unsubscribe<ChatRoomPage, ChatRoom>(this, "RoomProp");
            //    ScrollToLast();


            //});
            // waitForDataload();


        }

        protected override void OnBindingContextChanged()
        {
            if (this.BindingContext is IHasListViewModel hasListViewModel)
            {
                hasListViewModel.View = this;
            }
            base.OnBindingContextChanged();
        }


        protected async override void OnAppearing()
        {
            if (isFirstTimeAppearing)
            {
                isFirstTimeAppearing = false;
                try
                {
                    await Task.Run(() =>
                    {
                        ScrollToLast();
                    });
                    string ChatRoomKey = Preferences.Get("RoomProp", "");
                    //      chatViewModel.GetChatMessages(ChatRoomKey);
                    ScrollToLast();

                }
                catch (Exception ex)
                {
                    await App.Current.MainPage.DisplayAlert("Error", ex.Message, "OK");

                    await Navigation.PopAsync();
                }
            }

        }

        private void SendMessage_Button(object sender, EventArgs e)
        {
            if (Entry_SendMessage.Text == "")
            {
                return;
            }
            DateTime currentTime = DateTime.Now;
            Message message = new Message { Text = Entry_SendMessage.Text, Sender = userViewModel.CurrentUsername, dateTime = currentTime };
            string ChatRoomKey = Preferences.Get("RoomProp", "");
            chatViewModel.SendMessage(message);
            Entry_SendMessage.Text = "";
            //ScrollToLast();
        }


        private async void BackButton(object sender, EventArgs e)
        {
            await App.Current.MainPage.Navigation.PopAsync();

        }

        // Scroll MessageView to Last Message
        private async void waitForDataload()
        {
            if (MessagesListView.ItemsSource != null && MessagesListView.ItemsSource.Cast<object>().Count() > 0)
            {
                ScrollToLast();

            }
            else
            {
                await Task.Run(() => { return Task.Delay(1500); });
                ScrollToLast();


            }
        }

        public void ScrollToLast()
        {
            Device.BeginInvokeOnMainThread(() =>
            {
                try
                {
                    if (MessagesListView.ItemsSource != null && MessagesListView.ItemsSource.Cast<object>().Count() > 0)
                    {
                        var message = MessagesListView.ItemsSource.Cast<object>().LastOrDefault();
                        if (message != null)
                        {
                            MessagesListView.ScrollTo(message, ScrollToPosition.End, false);
                        }
                    }
                }
                catch (Exception ex)
                {

                    App.Current.MainPage.DisplayAlert("Error", ex.Message, "OK");

                }
            });
        }


    }
}