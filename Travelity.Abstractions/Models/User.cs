using PropertyChanged;
using SQLite;
using SQLiteNetExtensions.Attributes;
using System.Collections.Generic;

namespace Travelity.Abstractions.Models
{
    [AddINotifyPropertyChangedInterface]

    public class User
    {
        [PrimaryKey, AutoIncrement]
        public int id { get; set; }

        public string username { get; set; }
        public string firstName { get; set; }
        public string lastName { get; set; }
        public string fullName { get; set; }
        public string email { get; set; }
        public string password { get; set; }
        public string about { get; set; }
        public string profilePicture { get; set; }
        public bool Status { get; set; }
        [TextBlob("reminderBlobbed")]
        public List<int> reminder { get; set; }
        public int reminderBlobbed { get; set; }

        [TextBlob("likeBlobbed")]
        public List<int> like { get; set; }

        public int likeBlobbed { get; set; }
        [TextBlob("groupBlobbed")]
        public List<int> group { get; set; }
        public int groupBlobbed { get; set; }


        [TextBlob("adminBlobbed")]

        public List<int> admin { get; set; }
        public int adminBlobbed { get; set; }
        [TextBlob("chatsBlobbed")]

        public List<int> chats { get; set; }
        public int chatsBlobbed { get; set; }
        [TextBlob("commentsBlobbed")]

        public List<int> comments { get; set; }
        public int commentsBlobbed { get; set; }
        [TextBlob("picturesBlobbed")]
        public List<int> pictures { get; set; }
        public int picturesBlobbed { get; set; }
        [TextBlob("friendsBlobbed")]
        public List<int> friends { get; set; }
        public int friendsBlobbed { get; set; }
        [TextBlob("avBudgetBlobbed")]
        public List<int> avBudget { get; set; }
        public int avBudgetBlobbed { get; set; }

    }
}
