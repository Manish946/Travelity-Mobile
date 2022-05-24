using MvvmHelpers;
using Newtonsoft.Json;
using PropertyChanged;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Input;
using Travelity.Abstractions.Models;
using Travelity.Models;
using Xamarin.Essentials;
using Xamarin.Forms;

namespace Travelity.ViewModel.GroupViewModels
{
    [AddINotifyPropertyChangedInterface]

    public class GroupViewModel : BaseTravelityViewModel
    {
        public ObservableRangeCollection<Group> Groups { get; set; }
        public ObservableRangeCollection<User> GroupUsers { get; set; }
        public Command<int> LoadGroupUsers { get; }


        private Group group;
        public Group Group
        {
            get => group;
            set => group = value;
        }
        public GroupViewModel(Group group)
        {
            Group = group;

        }

        public GroupViewModel()
        {
            CurrentUsername = Preferences.Get("CurrentUsername", "");
            Groups = new ObservableRangeCollection<Group>();
            LoadGroupUsers = new Command<int>(GetGroupUsers);
            LoadGroups();
        }

        

        private async void LoadGroups()
        {
            Groups = await Client.GetGroups();
            
        }

        public async void GetGroupUsers(int GroupId)
        {
            try
            {
                GroupUsers = await Client.GetGroupUsers(GroupId);
            }
            catch (Exception ex)
            {

                await App.Current.MainPage.DisplayAlert("Error", ex.Message, "OK");

            }
        }




        readonly int peopleToShow = 3;

        public string PeopleAtGroup
        {
            get
            {
                var firstPerson = group.Users.FirstOrDefault();
                var peopleCount = group.Users.Count;

                if (firstPerson == null)
                {
                    return "It's just you";

                }

                var names = group.Users.Select(x => x.firstName).OrderBy(o => o).Take(3).ToList();
                string nameList = string.Join(", ", names);

                if (peopleCount > peopleToShow)
                {
                    return $"{nameList} and {peopleCount - peopleToShow} others";
                }
                else
                {
                    return nameList;
                }

            }
        }


        public List<object> PeopleImages
        {
            get
            {
                var peopleCount = group.Users.Count;

                List<object> returnList = new List<object>();
                returnList.AddRange(group.Users.Take(peopleToShow));
                if (peopleCount > peopleToShow)
                {
                    returnList.Add(peopleCount - peopleToShow);

                }
                return returnList;
            }
        }

        public List<object> TotalPeopleImages
        {
            get
            {
                var peopleCount = group.Users.Count;

                List<object> returnList = new List<object>();
                returnList.AddRange(group.Users);
                returnList.Add(peopleCount);
                return returnList;
            }
        }
    }
}
