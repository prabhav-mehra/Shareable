using System;
using System.Collections.ObjectModel;
using System.Security.AccessControl;
using System.Threading;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using ShareAble.Database;
using ShareAble.Model;


namespace ShareAble.ViewModel
{
	public partial class PostsViewModel : ObservableObject
	{
        PostsDatabase _postsDatabase;
        LocalUsersDatabase _localUserDatabase;
        public ObservableCollection<Posts> PostsCollection { get; } = new();

        public int LocalUserId { get; set; }
        public string LocalUserName { get; set; }
        public string LocalImageSource { get; set; }
        public int PartnerUserId { get; set; }

        public PostsViewModel(PostsDatabase postsDatabase, LocalUsersDatabase localUserDatabase)
		{
            _postsDatabase = postsDatabase;
            _localUserDatabase = localUserDatabase;
           
            Console.WriteLine("HERE");
            GetPosts(null);
            InitialisePartnerDetails();

        }

        [RelayCommand]
        private async void PhotoClicked()
        {
            //await _postsDatabase.DeleteAllItemAsync();
            Console.WriteLine("Photo clicked");
            try
            {
                var options = new MediaPickerOptions
                {
                    Title = "Select or Capture Photo"
                };

                var photo = await MediaPicker.CapturePhotoAsync(options);
                //var photo = await MediaPicker.CapturePhotoAsync();

                if (photo != null)
                {
                    // Use the captured photo
                    ImageSource imageSource = ImageSource.FromFile(photo.FullPath);
                    byte[] imageBytes = File.ReadAllBytes(photo.FullPath);
                    Console.WriteLine(imageBytes.Length);
                    string capturedAddress = await GetCurrentLocation();

                    Posts newPost = new Posts
                    {
                        PictureId = 0,
                        UserId = LocalUserId,
                        PartnerId = PartnerUserId,
                        PictureData = imageBytes,
                        Caption = "Beautiful sunset",
                        Timestamp = DateTime.Now,
                        UserName = LocalUserName,
                        UserImage = LocalImageSource,
                        Address = capturedAddress
                    };
         
                    await _postsDatabase.SaveItemAsync(newPost);
                    //MyImage.Source = imageSource;
                }
            }
            catch (Exception ex)
            {
                // Handle any exceptions that occur during photo capture
                Console.WriteLine($"Error capturing photo: {ex.Message}");
            }
        }

        private async void InitialisePartnerDetails()
        {
            LocalUser localUser = await _localUserDatabase.GetItemAsync(LocalUserId);
            PartnerUserId = localUser.PartnerID;
            LocalUserName = localUser.Name;
            LocalImageSource = localUser.ImageSource;
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

        public async Task<string> GetCurrentLocation()
        {
            var request = new GeolocationRequest(GeolocationAccuracy.Best);
            var location = await Geolocation.GetLocationAsync(request);
            string address = "";

            if (location != null)
            {
                // Location found
                double latitude = location.Latitude;
                double longitude = location.Longitude;
                var placemarks = await Geocoding.GetPlacemarksAsync(latitude, longitude);
                if (placemarks != null && placemarks.Any())
                {
                    var placemark = placemarks.FirstOrDefault();
                    address = placemark.Thoroughfare + ", " + placemark.Locality + ", " + placemark.AdminArea + " " + placemark.PostalCode;
                    // Use the address as needed
                    Console.WriteLine(address);
                   
                }
                // ...
            }
            else
            {
               
                // Unable to get location
                // Handle the error or show a message to the user
            }



            return address;
        }

        [RelayCommand]
        private async void GetPosts(object sender)
		{
            LocalUserId = int.Parse(Preferences.Get("CurrentUserId", string.Empty));

            //ImageSource source = ImageSource.FromFile("emoji1.png");
            //Stream imageStream = await ConvertImageSourceToStream(source);
            //Console.WriteLine("Image Byes" + imageStream);
            //Stream imageStream = null;
            
            //byte[] imageBytes = ConvertFileStreamToByteArray(imageStream);
            // Save the imageData byte array to the SQLite database

            //string imagePath = "/Users/prabhavmehra/Desktop/mauiproject/ShareAble/ShareAble/ShareAble/Resources/Images/emoji1.png";

            //byte[] imageBytes = File.ReadAllBytes(imageSource.);
            //byte[] imageBytes = await ConvertImageSourceToBytes(imageSource);

            //await _postsDatabase.DeleteAllItemAsync();
            //Console.WriteLine(source.ToString());
            //Console.WriteLine("Image Byes" + imageBytes);
            //Posts post1 = new Posts
            //{
            //    PictureId = 0,
            //    UserId = 4,
            //    PartnerId = 5,
            //    PictureData = imageBytes,
            //    Caption = "Beautiful sunset",
            //    Timestamp = DateTime.Now,
            //    UserName = "Shivani Bedi",
            //    UserImage = "emoji1.png"
            //};

            //Posts post2 = new Posts
            //{
            //    PictureId = 0,
            //    UserId = 5,
            //    PartnerId = 4,
            //    PictureData = imageBytes,
            //    Caption = "Delicious dinner",
            //    Timestamp = DateTime.Now,
            //    UserName = "Prabhav Mehra",
            //    UserImage = "emoji1.png"
            //};

            //Posts post3 = new Posts
            //{
            //    PictureId = 0,
            //    UserId = 4,
            //    PartnerId = 5,
            //    PictureData = imageBytes,
            //    Caption = "Adventurous hike",
            //    Timestamp = DateTime.Now,
            //    UserName = "Shivani Bedi",
            //    UserImage = "emoji1.png"
            //};

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
                if (!PostsCollection.Any(u => u.PictureId == posts.PictureId) &&
                    posts.UserId == LocalUserId &&
                    posts.PartnerId == PartnerUserId)
                {
                    PostsCollection.Add(posts);
                }
            }
            Console.WriteLine("Posts Here");
            if (sender !=  null)
            {
                Console.WriteLine(sender);
            }
            if (sender is RefreshView view)
            {
                Console.WriteLine("Refresh False");
                // Use the button reference to access properties or perform operations
                view.IsRefreshing = false;
            }
        }
    }
}

