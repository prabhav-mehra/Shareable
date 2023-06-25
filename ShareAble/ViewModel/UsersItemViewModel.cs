using System;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using CommunityToolkit.Mvvm.Messaging;

namespace ShareAble.ViewModel
{
	public partial class UsersItemViewModel : ObservableObject
	{
		[ObservableProperty]
		public int currentStep;
        static int MaxStep = 3;
        //public NavigationS NavigationService { get; set; }

        public UsersItemViewModel()
		{
		}

		[RelayCommand]
		private void SaveUser(object sender)
		{
            Console.WriteLine("Clicked" + CurrentStep + " " + sender);
            if (sender is Button button)
            {
                // Use the button reference to access properties or perform operations
                button.IsEnabled = false;
                button.BackgroundColor = button.IsEnabled.CompareTo(true) == 0 ? Color.FromRgb(255, 106, 233) : Colors.Gray;
            }
            
            if (CurrentStep == MaxStep - 1)
            {
                //UsersModel user1 = new UsersModel
                //{
                //    ID = 0,
                //    Name = "John",
                //    ContactNumber = 1234567890,
                //    DOB = "01/01/1990",
                //    HasPartner = true,
                //    PartnerName = "Jane",
                //    PartnerContactNumber = 9876543210
                //};

                //UsersModel user2 = new UsersModel
                //{
                //    ID = 0,
                //    Name = "Alice",
                //    ContactNumber = 9876543210,
                //    DOB = "02/02/1995",
                //    HasPartner = false,
                //    PartnerName = "",
                //    PartnerContactNumber = -1
                //};

                //UsersModel user3 = new UsersModel
                //{
                //    ID = 0,
                //    Name = "Bob",
                //    ContactNumber = 5555555555,
                //    DOB = "03/03/1985",
                //    HasPartner = true,
                //    PartnerName = "Eve",
                //    PartnerContactNumber = 9999999999
                //};

                //UsersModel user4 = new UsersModel
                //{
                //    ID = 0,
                //    Name = "Sarah",
                //    ContactNumber = 1111111111,
                //    DOB = "04/04/2000",
                //    HasPartner = false,
                //    PartnerName = "",
                //    PartnerContactNumber = -1
                //};

                //UsersModel user5 = new UsersModel
                //{
                //    ID = 0,
                //    Name = "Michael",
                //    ContactNumber = 7777777777,
                //    DOB = "05/05/1992",
                //    HasPartner = true,
                //    PartnerName = "Emily",
                //    PartnerContactNumber = 2222222222
                //};

                //UsersModel user6 = new UsersModel
                //{
                //    ID = 0,
                //    Name = "Olivia",
                //    ContactNumber = 4444444444,
                //    DOB = "06/06/1998",
                //    HasPartner = false,
                //    PartnerName = "",
                //    PartnerContactNumber = -1
                //};

                //UsersModel user7 = new UsersModel
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
                WeakReferenceMessenger.Default.Send(new MyMessage("NavigateToMainPage"));
                //await Navigation.PushAsync(new MainPage(database));
                return;
            }
            if (CurrentStep < MaxStep)
            {
                CurrentStep++;
            }
        }
	}
}

