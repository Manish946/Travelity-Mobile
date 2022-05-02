using MvvmHelpers;
using Refit;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using System.Threading.Tasks;
using Travelity.Abstractions.Models;

namespace Travelity.RefitAbstractions
{
    public interface ITravelityApiClient
    {
        // Client User
        [Get("/User")]
        Task<ObservableRangeCollection<User>> GetUsers();

        [Get("/User/Friendslist/{UserName}")]
        Task<ObservableRangeCollection<User>> GetUserFriends(string UserName);

        [Post("/User")]
        Task CreateUser([Body] User request);

        [Get("/User/Username/{UserName}")]
        Task<User> GetUserByUsername(string UserName);

        [Put("/User/Update/{userId}")]
        Task UpdateUser(int userId, [Body] User user);





        // Client Friend
        [Get("/User/FriendRequests/{UserName}")]
        Task<ObservableRangeCollection<User>> GetUserFriendRequests(string UserName);

        [Post("/Friend")]
        Task CreateFriend([Body] Friend friend);

        [Post("/FriendRequest")]
        Task SendFriendRequest([Body] FriendRequest request);

        [Get("/FriendRequest/Sender/{UserName}")]
        Task<ObservableRangeCollection<FriendRequest>> GetUserSentRequests(string UserName);

        [Get("/FriendRequest/Receiver/{UserName}")]
        Task<ObservableRangeCollection<FriendRequest>> GetUserReceiveRequests(string UserName);

        [Put("/FriendRequest/Update/{Id}")]
        Task UpdateFriendRequest(int Id, [Body] FriendRequest friendRequest);


        [Delete("/FriendRequest/Delete/{Id}")]
        Task DeleteFriendRequest(int Id);



    }
}
