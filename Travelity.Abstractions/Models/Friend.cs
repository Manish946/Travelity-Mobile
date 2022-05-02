using System;
using System.Collections.Generic;
using System.Text;

namespace Travelity.Abstractions.Models
{
    public class Friend
    {
        public int Id { get; set; }
        public string User_Username { get; set; }

        public string Friend_Username { get; set; }
    }
}
