using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using ShareAble.Database;
using ShareAble.Model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShareAble.ViewModel
{
    public partial  class ProfileViewModel : ObservableObject
    {
        [ObservableProperty]
        LocalUser localUserProperty;

        public ObservableCollection<Posts> PostsCollection { get; } = new();

        PostsDatabase _postsDatabase;
        LocalUsersDatabase _localUsersDatabase;

        private int LocalUserId { get; set; }

        public ProfileViewModel(PostsDatabase postsDatabase, LocalUsersDatabase localUsersDatabase)
        {
            _postsDatabase = postsDatabase;
            _localUsersDatabase = localUsersDatabase;
            GetLocalUser();
            GetPosts();
        }

        public async void GetLocalUser()
        {
            LocalUserId = int.Parse(Preferences.Get("CurrentUserId", "0"));
            LocalUser localUser = await _localUsersDatabase.GetItemAsync(LocalUserId);
            LocalUserProperty = localUser;
        }

        [RelayCommand]
        private async void GoToPostDetails(Posts post)
        {
            Console.WriteLine("Clicked");
            Console.WriteLine(post.PictureId);

            if (post == null)
                return;
            Console.WriteLine("Clicked2s");
            Console.WriteLine(post.PictureId);
            await Shell.Current.GoToAsync(nameof(ImageDetailsView), true, new Dictionary<string, object>
            {
                {nameof(Posts), post }
            });

            //await Shell.Current.GoToAsync(nameof(ImageDetailsView));
        }


        public async void GetPosts()
        {
            List<Posts> userPosts = await _postsDatabase.GetLocalUserPosts(LocalUserId);
            foreach (Posts posts in userPosts)
            {
                Console.WriteLine($"ID: {posts.PictureId}");
                Console.WriteLine($"Name: {posts.PictureData}");
                //Console.WriteLine($"PartnerID: {partnerLocal}");
                // Print other properties as needed
                Console.WriteLine("-------------------------");
                if (!PostsCollection.Any(u => u.PictureId == posts.PictureId) &&
                    posts.UserId == LocalUserId)
                {
                    PostsCollection.Add(posts);
                }
            }
        }

    }
}
