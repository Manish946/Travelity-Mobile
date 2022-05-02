using MvvmHelpers;
using MvvmHelpers.Commands;
using PropertyChanged;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Travelity.Models.Chat;
using Xamarin.CommunityToolkit.UI.Views;
using Xamarin.Essentials;
using Xamarin.Forms;

namespace Travelity.ViewModel.ChatViewModels
{
    [AddINotifyPropertyChangedInterface]
    public class ChatRoomViewModel:BaseTravelityViewModel
    {
        private bool isBusy { get; set; }
        public List<ChatRoom> ChatRooms { get; set; }
        public AsyncCommand RefreshCommand { get; }
        public LayoutState MainState { get; set; }

        public ChatRoomViewModel()
        {
            CurrentUsername = Preferences.Get("CurrentUsername", "");
            // Calling Chat rooms from Firebase Database.
            GetChatRooms();
            RefreshCommand = new AsyncCommand(Refresh);
        }

        private async void GetChatRooms()
        {
            ChatRooms = await fireBaseDB.GetChatRooms(CurrentUsername);
            ChatRooms.OrderBy(chatRoom => chatRoom.LastMessageReceived).ToList();
            if (ChatRooms.Count != 0)
            {
                MainState = LayoutState.None;
            }
            else
            {
                MainState = LayoutState.Empty;
            }

        }
        async Task Refresh()
        {
            if (isBusy) {
                return;
            }
            isBusy = true;

            try
            {
                GetChatRooms();
            }
            catch (Exception ex)
            {
                await App.Current.MainPage.DisplayAlert("Error", ex.Message, "OK");

            }
            finally
            {
                isBusy = false;
            }

        }

        public async void AddChatRoom(string user2)
        {
            if (isBusy) { return; }

            try
            {
                DateTime dateTime = DateTime.Now;
                string[] ChatMembers = { CurrentUsername, user2 };
                var User1 = await Client.GetUserByUsername(ChatMembers[0]);
                var User2 = await Client.GetUserByUsername(ChatMembers[1]);

                string[] DisplayNames = { User1.fullName, User2.fullName };
                await fireBaseDB.SaveChatRoom(new ChatRoom()
                {
                    LastMessageReceived = dateTime,
                    LastMessageSent = "New Chat Room",
                    Members = ChatMembers
                });
               await Refresh();
               //ScrollToLast(ListviewChatrooms);
            }
            catch (Exception ex)
            {
                await App.Current.MainPage.DisplayAlert("Error", ex.Message, "OK");

            }
            finally
            {
                isBusy=false;
            }


        }

        // Scroll view to recently added item.
        public void ScrollToLast(ListView ListviewChatrooms)
        {
            Device.BeginInvokeOnMainThread(() =>
            {
                try
                {
                    if (ListviewChatrooms.ItemsSource != null && ListviewChatrooms.ItemsSource.Cast<object>().Count() > 0)
                    {
                        var NewChatroom = ListviewChatrooms.ItemsSource.Cast<object>().LastOrDefault();
                        if (NewChatroom != null)
                        {
                            ListviewChatrooms.ScrollTo(NewChatroom, ScrollToPosition.MakeVisible, false);
                        }
                    }

                }
                catch (Exception ex)
                {
                    System.Diagnostics.Debug.WriteLine(ex.ToString());
                }
            });
        }
    }
}
