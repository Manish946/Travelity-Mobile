using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Travelity.Abstractions.Models;
using Travelity.Models;

namespace Travelity.Service
{
    public interface IUserService
    {
        Task AddCurrentUser(User user);
        Task RemoveCurrentUser();
        Task RemoveCurrentGoogleUser();
        Task AddCurrentGoogleUser(GoogleUser user);
        Task<User> GetCurrentUser();
        Task<GoogleUser> GetCurrentGoogleUser();
        Task updateCurrentUser(User user);

    }
}
