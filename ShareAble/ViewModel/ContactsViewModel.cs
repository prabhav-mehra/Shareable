using System;
using System.Collections.ObjectModel;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
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
        LocalUser _localUser;

        [ObservableProperty]
        LocalUser _partnerLocalUser;

        private int LocalUserId { get; set; }

        public ContactsViewModel(PartnerItemDatabase usersItemDatabase, LocalUsersDatabase localUsersDatabase)
        {
            database = usersItemDatabase;
            _localUsersDatabase = localUsersDatabase;
            InitialiseLocalUser();
            GetUsers();
        }

        private async void InitialiseLocalUser()
        {
            LocalUserId = int.Parse(Preferences.Get("CurrentUserId", string.Empty));
            LocalUser = await _localUsersDatabase.GetItemAsync(LocalUserId);
            List<Partner> localUsers = await database.GetItemsAsync();
            foreach (Partner partnerLocal in localUsers)
            {
                Console.WriteLine($"ID: {partnerLocal.PartnerUserId}");
                Console.WriteLine($"Name: {partnerLocal.UserId}");
                //Console.WriteLine($"PartnerID: {partnerLocal}");
                // Print other properties as needed
                Console.WriteLine("-------------------------");
            }

            Partner _partnerUser = await database.GetItemAsync(LocalUserId);
            Console.WriteLine("USER ID" + _partnerUser.PartnerUserId);

            PartnerLocalUser = await _localUsersDatabase.GetItemAsync(_partnerUser.PartnerUserId);
        }

        [RelayCommand]
        private async void GetUsers()
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


            List<LocalUser> users = await _localUsersDatabase.GetItemsAsync();
           
            Console.WriteLine("HEre" + users.Count);
            //ObservableCollection<Card> Cards = new ObservableCollection<Card>();
            foreach (LocalUser user in users)
            {
                if (!user.HasPartner && LocalUserId != user.ID)
                {
                    UsersCollection.Add(user);
                }
               
                //Console.WriteLine(user.Name);
            }
        }

        [RelayCommand]
        private async void GoToPostsPage()
        {
            await Shell.Current.GoToAsync(nameof(HomeGridView));
        }

        [RelayCommand]
        public async Task AddPartner(LocalUser sender)
        {
            //List<LocalUser> localUsers = await _localUsersDatabase.GetItemsAsync();
            //foreach (LocalUser localUser123 in localUsers)
            //{
            //    Console.WriteLine($"ID: {localUser123.ID}");
            //    Console.WriteLine($"Name: {localUser123.Name}");
            //    Console.WriteLine($"PartnerID: {localUser123.HasPartner}");
            //    // Print other properties as needed
            //    Console.WriteLine("-------------------------");
            //}


            //if (LocalUser == null || LocalUser.HasPartner)
            //    return;

            Console.WriteLine(LocalUserId +  "--" + sender.ID);
            Console.WriteLine(sender.Name);
            //await database.AddPartnerAsync(LocalUserId, sender.ID);

            List<Partner> localUsers = await database.GetItemsAsync();
            foreach (Partner partnerLocal in localUsers)
            {
                Console.WriteLine($"ID: {partnerLocal.PartnerUserId}");
                Console.WriteLine($"Name: {partnerLocal.UserId}");
                //Console.WriteLine($"PartnerID: {partnerLocal}");
                // Print other properties as needed
                Console.WriteLine("-------------------------");
            }

            //LocalUser.PartnerName = sender.Name;
            //LocalUser.HasPartner = true;
            //LocalUser.PartnerContactNumber = sender.ContactNumber;
            //LocalUser.PartnetID = sender.ID;

            //int localUserResult = await _localUsersDatabase.SaveItemAsync(LocalUser);

            ////Partner users = await database.GetItemAsync(sender.ID);
            ////users.PartnerName = LocalUser.Name;
            ////users.HasPartner = true;
            ////users.PartnerContactNumber = LocalUser.ContactNumber;

            //int usersResult = await database.SaveItemAsync(users);

            //Console.WriteLine("Add partner" +  sender.Name + sender.PartnerName);
            //Console.WriteLine(sender.ID);

        }
    }
}

