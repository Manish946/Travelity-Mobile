using MvvmHelpers;
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
            CurrentUsername = Preferences.Get("CurrentUsername", "");
            Groups = new ObservableRangeCollection<GroupViewModel>();
            NewGroup = new Group();
            newGroup = NewGroup;
            newGroup.groupThumbnail = "https://c4.wallpaperflare.com/wallpaper/500/442/354/outrun-vaporwave-hd-wallpaper-preview.jpg";
            LoadGroups();
        }

        async Task Refresh()
        {

            try
            {
                LoadGroups();
            }
            catch (Exception ex)
            {
                await App.Current.MainPage.DisplayAlert("Error", ex.Message, "OK");

            }


        }

        public async void AddUserToGroup(User user, Group group)
        {
            GroupUser groupUser = new GroupUser { GroupId = group.Id, UserId = user.id, UserUsername = user.username };
            await Client.AddUserToGroup(groupUser);
        }

        public async Task<string> ChangeGroupPicture(Stream mediaFile, string path)
        {
            Task<string> downloadableImage = fireStorageDB.UploadGroupPicture(mediaFile, path);
            if (await downloadableImage != null)
            {
                newGroup.groupThumbnail = await downloadableImage;
                return await downloadableImage;
            }
            else
            {
                return "";
            }
        }

        private async void LoadGroups()
        {
            CurrentUsername = Preferences.Get("CurrentUsername", "");
            var Usergroups = await Client.GetUserGroups(CurrentUsername);
            Groups.Clear();
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

        public async void CreateGroup(Group createGroup)
        {
            createGroup.groupAdmin = CurrentUsername;
            createGroup.createdTimeStamp = DateTime.Now;
            await Client.CreateGroup(createGroup);
            // Add an Admin to Group as a user.
            var recentlyCreatedGroup = await Client.GetGroupByName(createGroup.groupName);
            User AdminUser = await Client.GetUserByUsername(CurrentUsername);
            GroupUser groupUser = new GroupUser { GroupId = recentlyCreatedGroup.Id, UserUsername = AdminUser.username, UserId = AdminUser.id };
            await Client.AddUserToGroup(groupUser);
            await Refresh();
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
