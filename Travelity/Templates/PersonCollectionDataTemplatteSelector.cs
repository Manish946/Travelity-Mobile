using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;
using Travelity.Models;

namespace Travelity.Templates
{

    public class PersonCollectionDataTemplatteSelector : DataTemplateSelector
    {
        public DataTemplate PersonTemplate { get; set; }
        public DataTemplate CounterTemplate { get; set; }

        protected override DataTemplate OnSelectTemplate(object item, BindableObject container)
        {
           if(item is Models.PeopleModel)
            {
                return PersonTemplate;
            }
            else
            {
                return CounterTemplate;
            }
        }

    }
}
