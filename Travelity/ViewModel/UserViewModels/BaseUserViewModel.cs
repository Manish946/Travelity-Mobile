using MvvmHelpers;
using MvvmHelpers.Commands;
using PropertyChanged;
using Refit;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using Travelity.Abstractions.Models;
using Travelity.RefitAbstractions;
using Xamarin.Essentials;


namespace Travelity.ViewModel.UserViewModels
{
    [AddINotifyPropertyChangedInterface]

    public class BaseUserViewModel: BaseTravelityViewModel
    {

        private User _user;

        public User User
        {
            get { return _user; }
            set { _user = value; }
        }

    }
}
