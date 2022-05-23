using System;
using System.Collections.Generic;
using System.Text;

namespace Travelity.Abstractions.Models
{
    public class GroupUser
    {
        public int Id { get; set; }
        public int GroupId { get; set; }
        public int UserId { get; set; }
        public string UserUsername { get; set; }
    }
}
