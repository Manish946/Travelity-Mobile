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
        public ObservableRangeCollection<GroupViewModel> Groups { get; set; }
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
            Groups = new ObservableRangeCollection<GroupViewModel>();
            LoadGroups();
        }



        private async void LoadGroups()
        {
            var Usergroups = await Client.GetGroups();

            for (int i = 0; i < Usergroups.Count(); i++)
            {
                // Add groups in groupView model
                var users = await GetGroupUsers(Usergroups[i].Id);
                Usergroups[i].Users = users;
                var group = new GroupViewModel(Usergroups[i]);
                Groups.Add(group);
            }


        }

        public async Task<List<User>> GetGroupUsers(int GroupId)
        {

            // return range collection.
            return await Client.GetGroupUsers(GroupId);


        }




        readonly int peopleToShow = 2;

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
