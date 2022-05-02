using Firebase.Database.Query;
using Firebase.Database;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Travelity.Models.Chat;
using Travelity.ViewModel;
using System.Linq;
using System.Collections.ObjectModel;
using Travelity.Abstractions.Models;
using Travelity.ViewModel.UserViewModels;
using PropertyChanged;
using Travelity.RefitAbstractions;
using Refit;
using Xamarin.Essentials;
using Newtonsoft.Json.Linq;
using Newtonsoft.Json;

namespace Travelity.Service.FirebaseService
{
    [AddINotifyPropertyChangedInterface]

    public class FirebaseDB
    {
        // Firebase Client from NuGet
        FirebaseClient firebaseClient;

        // Travlity Web Api URL.
        static readonly string BaseAddress = DeviceInfo.Platform == DevicePlatform.Android ? "http://164.68.120.109:8020/api" : "http://164.68.120.109:8020/api";
        // Using Refit to call our Api with baseAdrress as Client.
        private ITravelityApiClient client = RestService.For<ITravelityApiClient>(BaseAddress);

        public ITravelityApiClient Client
        {
            get { return client; }
            set { client = value; }
        }

        // Firebase Database 
        public FirebaseDB()
        {
            firebaseClient = new FirebaseClient("https://travelity-auth-default-rtdb.europe-west1.firebasedatabase.app/");
        }

        // GET CHAT ROOM TO DISPLAY
        public async Task<List<ChatRoom>> GetChatRooms(string currentUser)
        {
            User usermodel = new User();
            List<ChatRoom> Rooms = (await firebaseClient.Child("Chats/")
                .OnceAsync<ChatRoom>()).Select(chatRoom => new ChatRoom
                {
                    Key = chatRoom.Key,
                    LastMessageReceived = chatRoom.Object.LastMessageReceived,
                    Members = chatRoom.Object.Members,
                    LastMessageSent = chatRoom.Object.LastMessageSent,
                    User1 = getUser1(chatRoom.Object.Members),
                    User2 = getUser2(chatRoom.Object.Members)

                }).Where(chats => chats.Members[1] == currentUser || chats.Members[0] == currentUser).ToList();

            return Rooms;
        }


        private User getUser1(string[] members)
        {
            User usermodel = new User();
            usermodel = Task.Run(() => Client.GetUserByUsername(members[0])).Result;
            return usermodel;
        }
        private User getUser2(string[] members)
        {
            User usermodel = new User();
            try
            {
                usermodel = Task.Run(() => Client.GetUserByUsername(members[1])).Result;

                return usermodel;
            }
            catch (Exception)
            {

                return null;
            }
        }
        // Check if chatroom already exists

        public async Task<ChatRoom> ChatRoomExists(string FriendUsername, string username)
        {
            if (FriendUsername != null)
            {
                ChatRoom chatroom = (await firebaseClient.Child("Chats/")
                .OnceAsync<ChatRoom>()).Select(chatRoom => new ChatRoom
                {
                    Key = chatRoom.Key,
                    LastMessageReceived = chatRoom.Object.LastMessageReceived,
                    Members = chatRoom.Object.Members,
                    LastMessageSent = chatRoom.Object.LastMessageSent,
                    User1 = getUser1(chatRoom.Object.Members),
                    User2 = getUser2(chatRoom.Object.Members)

                }).Where(chatsRoom => chatsRoom.Members.Contains(FriendUsername) && chatsRoom.Members.Contains(username)).FirstOrDefault();
                if (chatroom != null)
                {
                    return chatroom;
                }
                else
                {
                    return null;
                }
            }
            else
            {
                return null;
            }
        }


        // ADD CHATROOM TO FIREBASE DATABASE
        public async Task SaveChatRoom(ChatRoom chatRoom)
        {
            await firebaseClient.Child("Chats/").PostAsync(chatRoom);
        }

        // GET CHATROOM KEY FOR THE CHAT
        public ObservableCollection<Message> GetChatRoomMessages(string chatRoomKey)
        {
            var messages = firebaseClient.Child("Chats/" + chatRoomKey + "/Messages").AsObservable<Message>().AsObservableCollection<Message>();
            return messages;

        }

        // SEND MESSAGE TO THE CHATROOM

        public async Task SendMessage(Message message, string chatRoomKey)
        {
            await firebaseClient.Child("Chats/" + chatRoomKey + "/Messages").PostAsync(message);
            var data = JsonConvert.SerializeObject(message.Text);
            await firebaseClient.Child("Chats/" + chatRoomKey + "/LastMessageSent").PutAsync(data);
            DateTime Time = DateTime.Now;
            var CurrentDateTime = JsonConvert.SerializeObject(Time);

            await firebaseClient.Child("Chats/" + chatRoomKey + "/LastMessageReceived").PutAsync(CurrentDateTime);
        }

    }
}
