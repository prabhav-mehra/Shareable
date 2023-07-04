using System;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using CommunityToolkit.Mvvm.Messaging;
using ShareAble.Database;
using ShareAble.Model;

namespace ShareAble.ViewModel
{
	public partial class UsersItemViewModel : ObservableObject
	{
        LocalUsersDatabase _localUsersDatabase;

        [ObservableProperty]
		public int currentStep;
        static int MaxStep = 4;

        [ObservableProperty]
        string name;

        [ObservableProperty]
        string day;

        [ObservableProperty]
        string month;

        [ObservableProperty]
        string year;

        [ObservableProperty]
        long contactNumber;

        [ObservableProperty]
        string emoji;
        //public NavigationS NavigationService { get; set; }

        List<string> emojis = new List<string>()
        {
            "emoji1.png", "emoji2.png", "emoji3.png", "emoji4.png", "emoji5.png", "emoji6.png",
            "emoji7.png", "emoji10.png"
        };

        public List<string> Emojis { get; set; }

        public UsersItemViewModel(LocalUsersDatabase localUsersDatabase)
		{
            _localUsersDatabase = localUsersDatabase;
           
            Emojis = emojis;
        }

        [RelayCommand]
        private void EmojiFrame(object sender)
        {
            Emoji = sender.ToString();      
        }

        private async Task StoreNewUserDetails()
        {
            Console.WriteLine(ContactNumber);
            LocalUser storedNewUser = await _localUsersDatabase.GetUserFromContact(ContactNumber);
            if (storedNewUser != null)
            {
                Console.WriteLine("NEW USER" + storedNewUser.ID);
                Preferences.Set("CurrentUserId", storedNewUser.ID.ToString());
                bool signedUp = true;
                Preferences.Set("SignedUp", signedUp.ToString());
            }
        }

		[RelayCommand]
		private async void SaveUser(object sender)
		{
            //await _localUsersDatabase.DeleteAllItemAsync();
            //bool signedUp = false;
            //Preferences.Set("SignedUp", signedUp.ToString());
            Console.WriteLine("Clicked" + CurrentStep + " " + sender);
            if (sender is Button button)
            {
                // Use the button reference to access properties or perform operations
                button.IsEnabled = false;
                button.BackgroundColor = button.IsEnabled.CompareTo(true) == 0 ? Color.FromRgb(255, 106, 233) : Colors.Gray;
            }
            
            if (CurrentStep == MaxStep - 1)
            {
               
              
                Console.WriteLine($"Name {Name} Date {Day}/{Month}/{Year} Contact {ContactNumber} Emoji {Emoji}");

                LocalUser newUser = new LocalUser
                {
                    ID = 0,
                    Name = Name,
                    DOB = $"{Day}/{Month}/{Year}",
                    ContactNumber = ContactNumber,
                    ImageSource = Emoji,
                    HasPartner = false,
                   
                };

                LocalUser newUser2 = new LocalUser
                {
                    ID = 0,
                    Name = "Shivani Bedi",
                    DOB = "03/08/2000",
                    ContactNumber = 123456,
                    ImageSource = "emoji1.png",
                    HasPartner = false,
                   
                };

                LocalUser newUser3 = new LocalUser
                {
                    ID = 0,
                    Name = "Jane Bump",
                    DOB = "2/02/2001",
                    ContactNumber = 09876,
                    ImageSource = "emoji2.png",
                    HasPartner = true,
                    
                };

                LocalUser newUser4 = new LocalUser
                {
                    ID = 0,
                    Name = "ROnald rist",
                    DOB = "05/02/1985",
                    ContactNumber = 123456,
                    ImageSource = "emoji1.png",
                    HasPartner = false,
                   
                };

                LocalUser newUser5 = new LocalUser
                {
                    ID = 0,
                    Name = "Shane Dawson",
                    DOB = "12/12/2001",
                    ContactNumber = 09876,
                    ImageSource = "emoji6.png",
                    HasPartner = true,
                   
                };

                LocalUser newUser6 = new LocalUser
                {
                    ID = 0,
                    Name = "Hinam Mehra",
                    DOB = "13/11/1995",
                    ContactNumber = 123456,
                    ImageSource = "emoji3.png",
                    HasPartner = false,
                   
                };



                await _localUsersDatabase.SaveItemAsync(newUser);
                await _localUsersDatabase.SaveItemAsync(newUser2);
                await _localUsersDatabase.SaveItemAsync(newUser3);
                await _localUsersDatabase.SaveItemAsync(newUser4);
                await _localUsersDatabase.SaveItemAsync(newUser5);
                await _localUsersDatabase.SaveItemAsync(newUser6);
                

                List<LocalUser> localUsers = await _localUsersDatabase.GetItemsAsync();
                foreach (LocalUser partnerLocal in localUsers)
                {
                    Console.WriteLine($"ID: {partnerLocal.ID}");
                    Console.WriteLine($"Name: {partnerLocal.Name}");
                    //Console.WriteLine($"PartnerID: {partnerLocal}");
                    // Print other properties as needed
                    Console.WriteLine("-------------------------");
                }
                await StoreNewUserDetails();

                // TODO: SET CURRENT USER ID
                await Shell.Current.GoToAsync(nameof(MainPage));
                return;
            }
            if (CurrentStep < MaxStep)
            {
                CurrentStep++;
            }
        }
	}
}

