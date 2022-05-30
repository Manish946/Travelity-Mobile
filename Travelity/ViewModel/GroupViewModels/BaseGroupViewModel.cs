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
        public Group NewGroup{ get; set; }
    }
}
