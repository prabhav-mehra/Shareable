using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using ShareAble.Database;
using ShareAble.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Google.Crypto.Tink.Mac;

namespace ShareAble.ViewModel
{
    [QueryProperty(nameof(Posts), "Posts")]
    public partial class PostsDetailViewModel : ObservableObject
    {
        [ObservableProperty]
        Posts posts;

        [ObservableProperty]
        string caption;

        PostsDatabase _postsDatabase;
        public PostsDetailViewModel(PostsDatabase postsDatabase)
        {
            _postsDatabase = postsDatabase;
            //Caption = Posts?.Caption;
            //Console.WriteLine("PostsDetailViewModel" + Post.PictureId);
        }

        [RelayCommand]
        private async void AddCaption()
        {
            Posts updatedPost = new Posts
            {
                PictureId = Posts.PictureId,
                UserId = Posts.UserId,
                PartnerId = Posts.PartnerId,
                PictureData = Posts.PictureData,
                Caption = Posts.Caption,
                Timestamp = Posts.Timestamp,
                UserName = Posts.UserName,
                UserImage = Posts.UserImage,
                Address = Posts.Address
            };
            await _postsDatabase.SaveItemAsync(updatedPost);
            Console.WriteLine("AddCaption" + Posts.PictureId + " " + Posts.Caption);
        }
    }
}
