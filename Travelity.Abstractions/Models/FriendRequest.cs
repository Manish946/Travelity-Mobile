using System;
using System.Collections.Generic;
using System.Text;

namespace Travelity.Abstractions.Models
{
    public class FriendRequest
    {
        public int Id { get; set; }
        public string Sender { get; set; }

        public string Receiver { get; set; }
        public int Status { get; set; }
    }
}
