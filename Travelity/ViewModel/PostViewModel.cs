using PropertyChanged;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Travelity.Models;
using Travelity.Service;
using Xamarin.Forms;

namespace Travelity.ViewModel
{
    [AddINotifyPropertyChangedInterface]

    public class PostViewModel:BaseTravelityViewModel
    {
        private PostModel post;

        public PostModel Post
        {
            get => post;
            set => post = value;
        }

        public PostViewModel(PostModel post)
        {
            this.post = post;
        }

        public void PostLike(PostModel post)
        {
           this.Post.Likes = post.Likes + 1;
        }


       
    }
}
