using System;
using System.Collections.Generic;
using System.Text;

namespace Travelity.Models.Chat
{
    public class Message
    {
        public string Text { get; set; }
        public DateTime dateTime { get; set; }
        public string Sender { get; set; }
    }
}
