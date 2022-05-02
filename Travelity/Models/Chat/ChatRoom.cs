using System;
using System.Collections.Generic;
using System.Text;
using Travelity.Abstractions.Models;

namespace Travelity.Models.Chat
{
    public class ChatRoom
    {
        public string LastMessageSent { get; set; } 
        public DateTime LastMessageReceived { get; set; }
        public string[] Members { get; set; }
        public string Key { get; set; }
        public User User1 { get; set; }
        public User User2 { get; set; }
    }

}
