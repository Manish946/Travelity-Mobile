using MvvmHelpers;
using MvvmHelpers.Commands;
using PropertyChanged;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Travelity.Abstractions.Models;
using Travelity.Models;
using Xamarin.Essentials;
using Xamarin.Forms;

namespace Travelity.ViewModel
{
    [AddINotifyPropertyChangedInterface]

    public class GroupViewModel:BaseTravelityViewModel
    {
        public ObservableRangeCollection<Group> Groups { get; set; }
        public ObservableRangeCollection<User> GroupUsers { get; set; }

        private GroupModel group;

        public GroupViewModel(GroupModel group)
        {
            Group = group;
            Groups = new ObservableRangeCollection<Group>();
            GetGroups();
        }

        public GroupViewModel()
        {
            Groups = new ObservableRangeCollection<Group>();
            GetGroups();
        }

        public GroupModel Group
        {
            get => group;
            set => group = value;
        }

        private async void GetGroups()
        {
            try
            {
                Groups = await Client.GetGroups();
            }
            catch (Exception ex)
            {

                await App.Current.MainPage.DisplayAlert("Error", ex.Message, "OK");

            }
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
                var firstPerson = group.People.FirstOrDefault();
                var peopleCount = group.People.Count;

                if (firstPerson == null)
                {
                    return "It's just you";

                }

                var names = group.People.Select(x => x.Name).OrderBy(o => o).Take(3).ToList();
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
                var peopleCount = group.People.Count;

                List<object> returnList = new List<object>();
                returnList.AddRange(group.People.Take(peopleToShow));
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
                var peopleCount = group.People.Count;

                List<object> returnList = new List<object>();
                returnList.AddRange(group.People);
                returnList.Add(peopleCount);
                return returnList;
            }
        }

    }
}
