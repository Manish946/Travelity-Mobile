using MvvmHelpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Travelity.Abstractions.Models;
using Travelity.Templates.TemplateViews;
using Travelity.Templates.TemplateViews.FriendsListDataTemplate;
using Travelity.ViewModel.FriendsViewModels;
using Travelity.ViewModel.UserViewModels;
using Xamarin.Forms;

namespace Travelity.Templates
{
    public class FriendsListDataTemplateSelector : DataTemplateSelector
    {
        DataTemplate Add_FriendDataTemplate;
        DataTemplate Sent_FriendDataTemplate;
        DataTemplate null_FriendDataTemplate;
        DataTemplate AlreadyFriends_FriendDataTemplate;
        DataTemplate Response_FriendDataTemplate;
        UserViewModel userViewModel = new UserViewModel();
        FriendViewModel friendViewModel = new FriendViewModel();


        public FriendsListDataTemplateSelector()
        {
            this.Add_FriendDataTemplate = new DataTemplate(typeof(FriendsListDataTemplateAdd));
            this.Sent_FriendDataTemplate = new DataTemplate(typeof(FriendsListDataTemplateSent));
            this.AlreadyFriends_FriendDataTemplate = new DataTemplate(typeof(FriendsListDataTemplateFriends));
            this.Response_FriendDataTemplate = new DataTemplate(typeof(FriendsListDataTemplateResponse));
            this.null_FriendDataTemplate = new DataTemplate(typeof(EmptyTemplate));
            userViewModel.GetSentFriendRequests();
            // RESPONSE IF ELSE DATA TEMPLATE
        }
        protected override DataTemplate OnSelectTemplate(object item, BindableObject container)
        {
            try
            {
                User user = item as User;

                if (user == null)
                {
                    return null_FriendDataTemplate;
                }
                else
                {
                    // if user Found as a friend then return Friend Template.
                    bool checkFriends = friendViewModel.IsFriend(user.username);
                    if (checkFriends == false)
                    {
                        ObservableRangeCollection<FriendRequest> SentRequests = userViewModel.SentFriendRequests;
                        if (SentRequests != null)
                        {

                            FriendRequest SentUser = SentRequests.Where(sent => sent.Receiver == user.username && sent.Sender == userViewModel.CurrentUsername).FirstOrDefault();
                            if (SentUser != null)
                            {
                                if (SentUser.Status == 0)
                                {
                                    return Sent_FriendDataTemplate;
                                }
                                else
                                {
                                    return AlreadyFriends_FriendDataTemplate;
                                }
                            }
                            else
                            {
                                
                                return Add_FriendDataTemplate;

                            }


                        }
                        else
                        {
                            var ReceiveRequest = userViewModel.UserFriendRequests.Where(friend => friend.username == user.username);

                            if (ReceiveRequest != null)
                            {
                                return Response_FriendDataTemplate;
                            }
                            else
                            {
                                return Add_FriendDataTemplate;

                            }

                        }

                    }
                    
                    else
                    {
                        return AlreadyFriends_FriendDataTemplate;
                    }


                }

            }
            catch (Exception)
            {

                return null;
            }
        }
    }
}
