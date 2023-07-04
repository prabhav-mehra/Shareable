using System;
using System.Collections.ObjectModel;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Microsoft.VisualBasic;
using ShareAble.Database;
using ShareAble.Model;

namespace ShareAble.ViewModel
{
    public partial class ContactsViewModel : ObservableObject
    {

        public ObservableCollection<LocalUser> UsersCollection { get; } = new();

        PartnerItemDatabase database;
        LocalUsersDatabase _localUsersDatabase;

        [ObservableProperty]
        LocalUser localUserProperty;

        [ObservableProperty]
        LocalUser _partnerLocalUser;

        private int LocalUserId { get; set; }

        public ContactsViewModel(PartnerItemDatabase usersItemDatabase, LocalUsersDatabase localUsersDatabase)
        {
            database = usersItemDatabase;
            _localUsersDatabase = localUsersDatabase;
            GetUsers(null);
            InitialiseLocalUser();
           
        }

        private async void InitialiseLocalUser()
        {
            LocalUserId = int.Parse(Preferences.Get("CurrentUserId", "0"));
            Console.WriteLine("Local User ID" + LocalUserId);
            LocalUserProperty = await _localUsersDatabase.GetItemAsync(LocalUserId);
            //List<Partner> localUsers = await database.GetItemsAsync();
            //foreach (Partner partnerLocal in localUsers)
            //{
            //    Console.WriteLine($"ID: {partnerLocal.PartnerUserId}");
            //    Console.WriteLine($"Name: {partnerLocal.UserId}");
            //    //Console.WriteLine($"PartnerID: {partnerLocal}");
            //    // Print other properties as needed
            //    Console.WriteLine("-------------------------");
            //}

         
            PartnerLocalUser = await _localUsersDatabase.GetItemAsync(LocalUserProperty.PartnerID);
        }

        [RelayCommand]
        private async void GetUsers(object sender)
        {

            //await database.DeleteAllItemAsync();
            //Users user1 = new Users
            //{
            //    ID = 0,
            //    Name = "John",
            //    ContactNumber = 1234567890,
            //    DOB = "01/01/1990",
            //    HasPartner = true,
            //    PartnerName = "Jane",
            //    PartnerContactNumber = 9876543210
            //};

            //Users user2 = new Users
            //{
            //    ID = 0,
            //    Name = "Alice",
            //    ContactNumber = 9876543210,
            //    DOB = "02/02/1995",
            //    HasPartner = false,
            //    PartnerName = "",
            //    PartnerContactNumber = -1
            //};

            //Users user3 = new Users
            //{
            //    ID = 0,
            //    Name = "Bob",
            //    ContactNumber = 5555555555,
            //    DOB = "03/03/1985",
            //    HasPartner = true,
            //    PartnerName = "Eve",
            //    PartnerContactNumber = 9999999999
            //};

            //Users user4 = new Users
            //{
            //    ID = 0,
            //    Name = "Sarah",
            //    ContactNumber = 1111111111,
            //    DOB = "04/04/2000",
            //    HasPartner = false,
            //    PartnerName = "",
            //    PartnerContactNumber = -1
            //};

            //Users user5 = new Users
            //{
            //    ID = 0,
            //    Name = "Michael",
            //    ContactNumber = 7777777777,
            //    DOB = "05/05/1992",
            //    HasPartner = true,
            //    PartnerName = "Emily",
            //    PartnerContactNumber = 2222222222
            //};

            //Users user6 = new Users
            //{
            //    ID = 0,
            //    Name = "Olivia",
            //    ContactNumber = 4444444444,
            //    DOB = "06/06/1998",
            //    HasPartner = false,
            //    PartnerName = "",
            //    PartnerContactNumber = -1
            //};

            //Users user7 = new Users
            //{
            //    ID = 0,
            //    Name = "David",
            //    ContactNumber = 6666666666,
            //    DOB = "07/07/1980",
            //    HasPartner = true,
            //    PartnerName = "Jessica",
            //    PartnerContactNumber = 6666666666
            //};

            //await database.SaveItemAsync(user1);
            //await database.SaveItemAsync(user2);
            //await database.SaveItemAsync(user3);
            //await database.SaveItemAsync(user4);
            //await database.SaveItemAsync(user5);
            //await database.SaveItemAsync(user6);
            //await database.SaveItemAsync(user7);

            
            //await _localUsersDatabase.DeleteAllItemAsync();
            LocalUser newUser = new LocalUser
            {
                ID = 0,
                Name = "Prabhav Mehra",
                DOB = "11/11/2000",
                ContactNumber = 345678,
                ImageSource = "emoji3.png",
                HasPartner = false,
                PartnerID = 2,
            };

            LocalUser newUser2 = new LocalUser
            {
                ID = 0,
                Name = "Shivani Bedi",
                DOB = "03/08/2000",
                ContactNumber = 123456,
                ImageSource = "emoji1.png",
                HasPartner = false,
                PartnerID = 1,
            };

            LocalUser newUser3 = new LocalUser
            {
                ID = 0,
                Name = "Jane Bump",
                DOB = "2/02/2001",
                ContactNumber = 09876,
                ImageSource = "emoji2.png",
                HasPartner = true,
                PartnerID = 100,
            };


            //await _localUsersDatabase.SaveItemAsync(newUser);
            //await _localUsersDatabase.SaveItemAsync(newUser2);
            //await _localUsersDatabase.SaveItemAsync(newUser3);

            List<LocalUser> users = await _localUsersDatabase.GetItemsAsync();
           
            Console.WriteLine("HEre" + users.Count);
            //ObservableCollection<Card> Cards = new ObservableCollection<Card>();
            foreach (LocalUser user in users)
            {
                if (!UsersCollection.Any(u => u.ID == user.ID))
                {

                    Console.WriteLine(user.HasPartner + " " + user.ID + " " + LocalUserId);
                    if (!user.HasPartner && (user.ID != LocalUserId))
                    {
                        UsersCollection.Add(user);
                    }
                }
            }
            if (sender != null)
            {
                Console.WriteLine(sender);
            }
            if (sender is RefreshView view)
            {
                Console.WriteLine("Refresh False");
                InitialiseLocalUser();
                // Use the button reference to access properties or perform operations
                view.IsRefreshing = false;
            }
            return;
        }

        [RelayCommand]
        private async void GoToPostsPage()
        {
            await Shell.Current.GoToAsync(nameof(HomeGridView));
        }

        [RelayCommand]
        public async Task AddPartner(LocalUser sender)
        {

            await database.DeleteAllItemAsync();

            List<LocalUser> localUsers = await _localUsersDatabase.GetItemsAsync();
            foreach (LocalUser localUser123 in localUsers)
            {
                Console.WriteLine($"ID Old: {localUser123.ID}");
                Console.WriteLine($"Name Old: {localUser123.Name}");
                Console.WriteLine($"HasPartner Old: {localUser123.HasPartner}");
                // Print other properties as needed
                Console.WriteLine("-------------------------");
            }

   
            Console.WriteLine(LocalUserId +  "--" + sender.ID);
            Console.WriteLine(sender.Name);
            //await database.AddPartnerAsync(LocalUserId, sender.ID);

            AddPartnerToLocalUser(sender.ID, LocalUserId);
            AddPartnerToPartnerUser(LocalUserId, sender.ID);

        }

        private async void AddPartnerToPartnerUser(int userId, int partnerUserId)
        {
            LocalUser localUser = await _localUsersDatabase.GetItemAsync(userId);
            Console.WriteLine(localUser.ID + " " + localUser.Name + " " + localUser.HasPartner);
           
            LocalUser updateUser = new LocalUser
            {
                ID = localUser.ID,
                Name = localUser.Name,
                DOB = localUser.DOB,
                ContactNumber = localUser.ContactNumber,
                ImageSource = localUser.ImageSource,
                HasPartner = true,
                PartnerID = partnerUserId,
            };
           

            await _localUsersDatabase.SaveItemAsync(updateUser);

            List<LocalUser> localUsers = await _localUsersDatabase.GetItemsAsync();
            foreach (LocalUser localUser123 in localUsers)
            {
                Console.WriteLine($"ID: {localUser123.ID}");
                Console.WriteLine($"Name: {localUser123.Name}");
                Console.WriteLine($"HasPartner: {localUser123.HasPartner}");
                // Print other properties as needed
                Console.WriteLine("-------------------------");
            }
        }

        private async void AddPartnerToLocalUser(int partnerUserId, int userId)
        {
         

            LocalUser partnerUser = await _localUsersDatabase.GetItemAsync(partnerUserId);
            Console.WriteLine(partnerUser.ID + " " + partnerUser.Name + " " + partnerUser.HasPartner);
       
            LocalUser updatePartnerUser = new LocalUser
            {
                ID = partnerUser.ID,
                Name = partnerUser.Name,
                DOB = partnerUser.DOB,
                ContactNumber = partnerUser.ContactNumber,
                ImageSource = partnerUser.ImageSource,
                HasPartner = true,
                PartnerID = userId,
            };
            
            await _localUsersDatabase.SaveItemAsync(updatePartnerUser);

            List<LocalUser> localUsers = await _localUsersDatabase.GetItemsAsync();
            foreach (LocalUser localUser123 in localUsers)
            {
                Console.WriteLine($"ID: {localUser123.ID}");
                Console.WriteLine($"Name: {localUser123.Name}");
                Console.WriteLine($"HasPartner: {localUser123.HasPartner}");
                // Print other properties as needed
                Console.WriteLine("-------------------------");
            }
        }
    }
}

