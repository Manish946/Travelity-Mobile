using PropertyChanged;
using System;
using System.Collections.Generic;
using System.Text;
using Travelity.Abstractions.Models;

namespace Travelity.ViewModel.GroupViewModels
{
    [AddINotifyPropertyChangedInterface]
    public class BaseGroupViewModel:BaseTravelityViewModel
    {
        private Group _group;

        public Group NewGroup
        {
            get { return _group; }
            set { _group = value; }
        }
    }
}
