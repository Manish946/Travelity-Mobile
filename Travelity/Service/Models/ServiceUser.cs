using SQLite;
using System;
using System.Collections.Generic;
using System.Text;

namespace Travelity.Service.Models
{
    public class ServiceUser
    {
        [PrimaryKey, AutoIncrement]
        public int id { get; set; }
        public int UserId { get; set; }
        public string username { get; set; }
        public string firstName { get; set; }
        public string lastName { get; set; }
        public string fullName { get; set; }
        public string email { get; set; }
        public string password { get; set; }
        public string about { get; set; }
        public string profilePicture { get; set; }
        public bool Status { get; set; }
        public List<int> reminder { get; set; }
        public List<int> like { get; set; }
        public List<int> group { get; set; }
        public List<int> admin { get; set; }
        public List<int> chats { get; set; }
        public List<int> comments { get; set; }
        public List<int> pictures { get; set; }
        public List<int> friends { get; set; }
        public List<int> avBudget { get; set; }
    }
}
