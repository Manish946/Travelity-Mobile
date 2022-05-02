using System;
using System.Collections.Generic;
using System.Text;
using Travelity.Models.Chat;
using Travelity.Templates.TemplateViews;
using Travelity.Templates.TemplateViews.ChatRoomDataTemplate;
using Travelity.ViewModel.UserViewModels;
using Xamarin.Forms;

namespace Travelity.Templates
{
    public class ChatRoomDisplayNameDataTemplateSelector : DataTemplateSelector
    {
        DataTemplate DisplayFirstObject;
        DataTemplate DisplaySecondObject;
        DataTemplate noObject;
        UserViewModel userViewModel = new UserViewModel();
        public ChatRoomDisplayNameDataTemplateSelector()
        {
            this.DisplayFirstObject = new DataTemplate(typeof(ChatRoomDisplayFirstObject));
            this.DisplaySecondObject = new DataTemplate(typeof(ChatRoomDisplaySecondObject));
            this.noObject = new DataTemplate(typeof(EmptyTemplate));
        }
        protected override DataTemplate OnSelectTemplate(object item, BindableObject container)
        {
            try
            {
                ChatRoom chatRoom = item as ChatRoom;
                if (chatRoom == null)
                {
                    return noObject;
                }
                else
                {
                    if(chatRoom.Members[0] == userViewModel.CurrentUsername)
                    {
                        return DisplaySecondObject;
                    }
                    else
                    {
                        return DisplayFirstObject;
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
