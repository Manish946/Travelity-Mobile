using System;
using System.Collections.Generic;
using System.Text;
using Travelity.Templates.TemplateViews;
using Travelity.Models.Chat;
using Travelity.Templates.TemplateViews.ChatDataTemplate;
using Xamarin.Forms;
using Travelity.ViewModel.UserViewModels;

namespace Travelity.Templates
{
    class ChatTemplateSelector : DataTemplateSelector
    {
        DataTemplate incomingDataTemplate;
        DataTemplate outgoingDataTemplate;
        DataTemplate nullDataTemplate;
        UserViewModel userViewModel = new UserViewModel();
        public ChatTemplateSelector()
        {
            this.incomingDataTemplate = new DataTemplate(typeof(IncomingViewCell));
            this.outgoingDataTemplate = new DataTemplate(typeof(OutgoingViewCell));
            this.nullDataTemplate = new DataTemplate(typeof(EmptyTemplate));
        }


        protected override DataTemplate OnSelectTemplate(object item, BindableObject container)
        {

            try
            {
                Message Message = item as Message;

                if (Message == null)
                {
                    return nullDataTemplate;
                }
                else
                {
                    if (Message.Sender == userViewModel.CurrentUsername)
                    {
                        return outgoingDataTemplate;
                    }
                    else
                    {
                        return incomingDataTemplate;
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
