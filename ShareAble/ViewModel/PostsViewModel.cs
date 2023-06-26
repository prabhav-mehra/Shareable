using System;
using System.Collections.ObjectModel;
using System.Threading;
using CommunityToolkit.Mvvm.ComponentModel;

using ShareAble.Database;
using ShareAble.Model;

namespace ShareAble.ViewModel
{
	public class PostsViewModel : ObservableObject
	{
        PostsDatabase _postsDatabase;
        public ObservableCollection<Posts> PostsCollection { get; } = new();

        public int LocalUserId { get; set; }

        public PostsViewModel(PostsDatabase postsDatabase)
		{
            _postsDatabase = postsDatabase;
            Console.WriteLine("HERE");
            Initalise();

        }

		public async void Initalise()
		{
            LocalUserId = int.Parse(Preferences.Get("CurrentUserId", string.Empty));
           
            ImageSource source = ImageSource.FromFile("emoji1.png");
            Stream imageStream = await ConvertImageSourceToStream(source);
            Console.WriteLine("Image Byes" + imageStream);

            
            byte[] imageBytes = ConvertFileStreamToByteArray(imageStream);
            // Save the imageData byte array to the SQLite database

            //string imagePath = "/Users/prabhavmehra/Desktop/mauiproject/ShareAble/ShareAble/ShareAble/Resources/Images/emoji1.png";

            //byte[] imageBytes = File.ReadAllBytes(imageSource.);
            //byte[] imageBytes = await ConvertImageSourceToBytes(imageSource);

            //await _postsDatabase.DeleteAllItemAsync();
            //Console.WriteLine(source.ToString());
            //Console.WriteLine("Image Byes" + imageBytes);
            Posts post1 = new Posts
            {
                PictureId = 0,
                UserId = 4,
                PartnerId = 5,
                PictureData = imageBytes,
                Caption = "Beautiful sunset",
                Timestamp = DateTime.Now,
                UserName = "Shivani Bedi",
                UserImage = "emoji1.png"
            };

            Posts post2 = new Posts
            {
                PictureId = 0,
                UserId = 5,
                PartnerId = 4,
                PictureData = imageBytes,
                Caption = "Delicious dinner",
                Timestamp = DateTime.Now,
                UserName = "Prabhav Mehra",
                UserImage = "emoji1.png"
            };

            Posts post3 = new Posts
            {
                PictureId = 0,
                UserId = 4,
                PartnerId = 5,
                PictureData = imageBytes,
                Caption = "Adventurous hike",
                Timestamp = DateTime.Now,
                UserName = "Shivani Bedi",
                UserImage = "emoji1.png"
            };

            //await _postsDatabase.SaveItemAsync(post1);
            //await _postsDatabase.SaveItemAsync(post2);
            //await _postsDatabase.SaveItemAsync(post3);


            List<Posts> localUsers = await _postsDatabase.GetItemsAsync();
            foreach (Posts partnerLocal in localUsers)
            {
                Console.WriteLine($"ID: {partnerLocal.PictureId}");
                Console.WriteLine($"Name: {partnerLocal.Caption}");
                //Console.WriteLine($"PartnerID: {partnerLocal}");
                // Print other properties as needed
                Console.WriteLine("-------------------------");
            }
            Console.WriteLine(LocalUserId);

            List<Posts> userPosts = await _postsDatabase.GetUserPosts(LocalUserId);
            foreach (Posts posts in userPosts)
            {
                Console.WriteLine($"ID: {posts.PictureId}");
                Console.WriteLine($"Name: {posts.PictureData}");
                //Console.WriteLine($"PartnerID: {partnerLocal}");
                // Print other properties as needed
                Console.WriteLine("-------------------------");
                PostsCollection.Add(posts);
            }
        }

        public static async Task<Stream> ConvertImageSourceToStream(ImageSource imageSource)
        {
            if (imageSource is FileImageSource fileImageSource)
            {
                // Load the image file as a stream
                var imageStream = File.OpenRead(fileImageSource.File);
                return imageStream;
            }

            if (imageSource is UriImageSource uriImageSource)
            {
                // Download the image from the URI as a stream
                using (var webClient = new System.Net.WebClient())
                {
                    byte[] imageData = await webClient.DownloadDataTaskAsync(uriImageSource.Uri);
                    return new MemoryStream(imageData);
                }
            }

            if (imageSource is StreamImageSource streamImageSource)
            {
                // Retrieve the stream from the StreamImageSource
                Stream imageStream = await streamImageSource.Stream(CancellationToken.None);
                return imageStream;
            }

            return null; // Return null if the conversion is not supported for the given ImageSource
        }

        public byte[] ConvertFileStreamToByteArray(Stream fileStream)
        {
            using (var memoryStream = new MemoryStream())
            {
                fileStream.CopyTo(memoryStream);
                return memoryStream.ToArray();
            }
        }

    }
}

