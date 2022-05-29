﻿using MvvmHelpers;
using Newtonsoft.Json;
using PropertyChanged;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.IO;
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

    public class GroupViewModel : BaseGroupViewModel
    {
        public ObservableRangeCollection<GroupViewModel> Groups { get; set; }
        public Command<int> LoadGroupUsers { get; }
        public Command CreateGroupCommand { get; }
        private Group newGroup;
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
            CreateGroupCommand = new Command(CreateGroup);
            CurrentUsername = Preferences.Get("CurrentUsername", "");
            Groups = new ObservableRangeCollection<GroupViewModel>();
            NewGroup = new Group();
            newGroup = NewGroup;

            LoadGroups();
        }

        public async void ChangeGroupPicture(Stream mediaFile, string path)
        {
            Task<string> downloadableImage = fireStorageDB.UploadGroupPicture(mediaFile,path);
            if(await downloadableImage != null)
            {
                newGroup.groupThumbnail = await downloadableImage;                
            }
            else
            {
                return;
            }
        }

        private async void LoadGroups()
        {
            var Usergroups = await Client.GetUserGroups(CurrentUsername);

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

        public async void CreateGroup(object obj)
        {
            newGroup.groupAdmin = CurrentUsername;
            newGroup.createdTimeStamp = DateTime.Now;
            await Client.CreateGroup(newGroup);

        }



        readonly int peopleToShow = 3;

        public string PeopleAtGroup
        {
            get
            {
                var firstPerson = group.Users.FirstOrDefault();
                var peopleCount = group.Users.Count;

                if (peopleCount == 1)
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
