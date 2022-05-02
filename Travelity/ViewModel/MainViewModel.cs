using MvvmHelpers;
using PropertyChanged;
using System;
using System.Collections.Generic;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using Travelity.Abstractions.Models;
using Travelity.Models;
using Travelity.Service;
using Xamarin.CommunityToolkit.UI.Views;
using Xamarin.Essentials;
using Xamarin.Forms;

namespace Travelity.ViewModel
{
    [AddINotifyPropertyChangedInterface]

    public class MainViewModel : GoogleUserViewModel
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string UserImage { get; set; }
        public string FriendImage { get; set; }
        public string FriendImage2 { get; set; }
        public string GroupCover { get; set; }
        public string FullName { get; set; }
        public string Email { get; set; }
        public string Aboutme { get; set; }
        public ObservableRangeCollection<GroupViewModel> Groups { get; set; }
        public ObservableRangeCollection<PostViewModel> Posts { get; set; }
        public LayoutState MainState { get; set; }

        public async Task RefreshCurrentUser()
        {
            userService = DependencyService.Get<IUserService>();
            try
            {
                CurrentUser = Task.Run(() => userService.GetCurrentUser()).Result;
                if(CurrentUser != null)
                {
                    if (CurrentUser.username != null)
                    {
                        FirstName = CurrentUser.firstName;
                        LastName = CurrentUser.lastName;
                        UserImage = CurrentUser.profilePicture;
                        FullName = FirstName + " " + LastName;
                        Email = CurrentUser.email;
                        Aboutme = CurrentUser.about;
                        //await Task.Delay(4000);
                        MainState = LayoutState.None;

                    }

                }
               
            }
            catch (WebException ex)
            {
                if (ex.Status == WebExceptionStatus.ProtocolError &&
                     ex.Response != null)
                {
                    var resp = (HttpWebResponse)ex.Response;
                    if (resp.StatusCode == HttpStatusCode.NotFound)
                    {
                    return;
                        // Do something
                    }
                    else
                    {
                    return;
                        // Do something else
                    }
                }
                else
                {
                    // Do something else
                    return;
                }
            }

        }
        public MainViewModel()
        {
            MainState = LayoutState.Loading;
            CurrentUsername = Preferences.Get("CurrentUsername", "");
            FriendImage = "https://randomuser.me/api/portraits/women/66.jpg";
            FriendImage2 = "https://randomuser.me/api/portraits/men/36.jpg";
            GroupCover = "https://i.pinimg.com/originals/68/77/f5/6877f510369f3e4b6a548d69ff307652.png";

            var post1 = new PostViewModel(
                new PostModel()
                {
                    User = new PeopleModel()
                    {
                        Name = "Alisa Ron",
                        Image = "https://randomuser.me/api/portraits/women/79.jpg"
                    },
                    ImageUrl = "https://i.pinimg.com/originals/68/77/f5/6877f510369f3e4b6a548d69ff307652.png",
                    TimeAgo = "8 min ago",
                    Caption = "Check out these cool wallpaper",
                    Likes = 232,
                    Comments = 184,
                    Shares = 96

                }
                );
            var post2 = new PostViewModel(
                new PostModel()
                {
                    User = new PeopleModel()
                    {
                        Name = "Dave Kulun",
                        Image = "https://randomuser.me/api/portraits/men/9.jpg"
                    },
                    ImageUrl = "https://c4.wallpaperflare.com/wallpaper/410/867/750/vector-forest-sunset-forest-sunset-forest-wallpaper-thumb.jpg",
                    TimeAgo = "28 min ago",
                    Caption = "This will be my Phone WallPaper",
                    Likes = 32,
                    Comments = 84,
                    Shares = 6

                }
                );

            var post3 = new PostViewModel(
               new PostModel()
               {
                   User = new PeopleModel()
                   {
                       Name = "Dave Kulun",
                       Image = "https://randomuser.me/api/portraits/men/36.jpg"
                   },
                   ImageUrl = "https://images.hdqwalls.com/download/the-galaxy-wolf-7y-1242x2688.jpg",
                   TimeAgo = "57 min ago",
                   Caption = "Made this art a year ago, i want your likes and comments.",
                   Likes = 232,
                   Comments = 184,
                   Shares = 26

               }
               );
            var post4 = new PostViewModel(
                new PostModel()
                {
                    User = new PeopleModel()
                    {
                        Name = "Monalisa blue",
                        Image = "https://randomuser.me/api/portraits/women/29.jpg"
                    },
                    ImageUrl = "https://cdn.statically.io/img/i.pinimg.com/originals/bf/82/f6/bf82f6956a32819af48c2572243e8286.jpg",
                    TimeAgo = "1 hr ago",
                    Caption = "This movie gave me chills and i would rate this spiderman movie 10/10, Highly Recommended.",
                    Likes = 1232,
                    Comments = 124,
                    Shares = 6

                }
                );
            var post5 = new PostViewModel(
                new PostModel()
                {
                    User = new PeopleModel()
                    {
                        Name = "Hanna monta",
                        Image = "https://randomuser.me/api/portraits/women/10.jpg"
                    },
                    ImageUrl = "https://cdn.wallpapersafari.com/1/31/G6FkbE.jpg",
                    TimeAgo = "2 hr ago",
                    Caption = "Travel is really my therapy, it helped me be a better person than i was before.",
                    Likes = 2232,
                    Comments = 484,
                    Shares = 6

                }
                );
            var post6 = new PostViewModel(
                new PostModel()
                {
                    User = new PeopleModel()
                    {
                        Name = "Jhon Ceha",
                        Image = "https://randomuser.me/api/portraits/men/5.jpg"
                    },
                    ImageUrl = "https://wallpapers.com/images/high/travel-wallpaper-f9u3momxicem2b3o.jpg",
                    TimeAgo = "16 hr ago",
                    Caption = "Finally, i escaped my misrable wife. Will be taking this vaccation and come back with my new wife.",
                    Likes = 132,
                    Comments = 4,
                    Shares = 2

                }
                );
            var FirstGroup = new GroupViewModel(
                new GroupModel()
                {
                    Name = "England Travel Group",
                    Destination = "London, England",
                    LocationThumbnail = "https://www.wendyperrin.com/wp-content/uploads/2014/09/evening-view-of-london-england-cr-visit-britain.jpg",
                    MapThumbnail = "https://upload.wikimedia.org/wikipedia/commons/thumb/b/bd/Greater_London_administrative_area_in_England.svg/1200px-Greater_London_administrative_area_in_England.svg.png",
                    People = new List<PeopleModel> {

                        new PeopleModel()
                    {
                        Name = "Manish",
                        Image = "https://randomuser.me/api/portraits/men/9.jpg"

                    },
                          new PeopleModel()
                    {
                        Name = "James",
                        Image = "https://randomuser.me/api/portraits/men/11.jpg"

                    },
                            new PeopleModel()
                    {
                        Name = "Adwin",
                        Image = "https://randomuser.me/api/portraits/men/85.jpg"

                    },
                            new PeopleModel()
                    {
                        Name = "Arina",
                        Image = "https://randomuser.me/api/portraits/women/79.jpg"

                    },

                            new PeopleModel()
                    {
                        Name = "Arina",
                        Image = "https://randomuser.me/api/portraits/women/79.jpg"

                    },


                            new PeopleModel()
                    {
                        Name = "Arina",
                        Image = "https://randomuser.me/api/portraits/women/79.jpg"

                    },

                            new PeopleModel()
                    {
                        Name = "Arina",
                        Image = "https://randomuser.me/api/portraits/women/79.jpg"

                    },
                    },
                    Albums = new List<AlbumModel>
                    {
                        new AlbumModel()
                        {
                            Name ="Castle",
                            Image = "https://imagesvc.meredithcorp.io/v3/mm/image?url=https%3A%2F%2Fimagesvc.meredithcorp.io%2Fv3%2Fmm%2Fimage%3Furl%3Dhttps%253A%252F%252Fstatic.onecms.io%252Fwp-content%252Fuploads%252Fsites%252F28%252F2020%252F01%252Fblenheim-palace-oxfordshire-UKCASTLE1219.jpg&q=85"

                        },
                         new AlbumModel()
                        {
                            Name ="lake",
                            Image = "https://offloadmedia.feverup.com/secretldn.com/wp-content/uploads/2016/08/18141306/walthamstow-wetlands-lakes-london.jpg"

                        }
                    }
                }
                )

            { };
            var SecondGroup = new GroupViewModel(
                new GroupModel()
                {
                    Name = "Tokyo Travel Group",
                    Destination = "Tokyo, Japan",
                    LocationThumbnail = "https://i.pinimg.com/originals/68/77/f5/6877f510369f3e4b6a548d69ff307652.png",
                    MapThumbnail = "https://upload.wikimedia.org/wikipedia/commons/thumb/b/bd/Greater_London_administrative_area_in_England.svg/1200px-Greater_London_administrative_area_in_England.svg.png",
                    People = new List<PeopleModel> {


                          new PeopleModel()
                    {
                        Name = "James",
                        Image = "https://randomuser.me/api/portraits/men/11.jpg"

                    },
                            new PeopleModel()
                    {
                        Name = "Adwin",
                        Image = "https://randomuser.me/api/portraits/men/85.jpg"

                    },
                            new PeopleModel()
                    {
                        Name = "Arina",
                        Image = "https://randomuser.me/api/portraits/women/79.jpg"

                    }
                    },
                    Albums = new List<AlbumModel>
                    {
                        new AlbumModel()
                        {
                            Name ="Castle",
                            Image = "https://res.cloudinary.com/enchanting/q_70,f_auto,c_fill,w_1600,h_650,g_face/mp-web/2021/06/Website-topbanner-classic-japan.png"

                        },
                         new AlbumModel()
                        {
                            Name ="lake",
                            Image = "https://ichef.bbci.co.uk/news/976/cpsprodpb/95EA/production/_118387383_35bb9b2c-a805-4fbf-a770-f4ed4f73da94.jpg"

                        }
                    }
                }
                )

            { };
            Groups = new ObservableRangeCollection<GroupViewModel>
            {
                FirstGroup,
                SecondGroup
            };

            Posts = new ObservableRangeCollection<PostViewModel>
            {
                post1,
                post2,
                post3,
                post4,
                post5,
                post6
            };
            Task.Run(RefreshCurrentUser);

        }


    }
}
