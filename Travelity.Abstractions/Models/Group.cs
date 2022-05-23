using System;
using System.Collections.Generic;
using System.Text;

namespace Travelity.Abstractions.Models
{
    public class Group
    {
        public int Id { get; set; }
        public string groupName { get; set; }
        public string destination { get; set; }
        public string groupAdmin { get; set; }
        public string groupThumbnail { get; set; }
        public DateTime createdTimeStamp { get; set; }
    }
}
