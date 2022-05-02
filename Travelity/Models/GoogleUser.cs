using PropertyChanged;
using System;
using System.Collections.Generic;
using System.Text;

namespace Travelity.Models
{
    [AddINotifyPropertyChangedInterface]

    public class GoogleUser
    {
        public string Name { get; set; } 
        public string Email { get; set; }
        public string Picture { get; set; }    
    }

    // Google Manager Interface with Login and logout Function.

    public interface IGooglemanager
    {
        // Login Method holds GoogleUser Class Model and Login Complete Result.
        void Login(Action<GoogleUser, string> OnLoginComplete);
        // This Method will be used to logout.
        void Logout();
    }
}
