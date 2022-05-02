using System;
using System.Collections.Generic;
using System.Text;

namespace Travelity.Models
{
    public class PostModel
    {
       public string PostId { get; set; }
        public PeopleModel User { get; set; }
        public string Caption { get; set; } 
        public string TimeAgo { get; set; }
        public string ImageUrl { get; set; }
        public int Likes { get; set; }
        public int Comments { get; set; }
        public int Shares { get; set; }

    }
}
