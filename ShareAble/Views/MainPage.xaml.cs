
using ShareAble.Database;
using ShareAble.Interfaces;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;

namespace ShareAble;

public partial class MainPage : ContentPage
{
    UsersItemDatabase database;

    public PermissionStatus PermissionStatus { get; set; }
    public IContactsService serviceContact { get; set; }

    public MainPage(UsersItemDatabase usersItemDatabase)
    {

        InitializeComponent();
        database = usersItemDatabase;
        Loaded += MainPage_Loaded;

        // Create sample cards
        Cards = new ObservableCollection<Card>
            {
                new Card { ImageSource = "emoji1.png", Name = "Card 1", PartnerName = "Prabhav" },
                new Card { ImageSource = "emoji2.png", Name = "Card 2", PartnerName = "Prabhav" },
                new Card { ImageSource = "emoji3.png", Name = "Card 1", PartnerName = "Prabhav"},
                new Card { ImageSource = "emoji4.png", Name = "Card 2", PartnerName = "Prabhav" },
                new Card {ImageSource = "emoji5.png",  Name = "Card 1", PartnerName = "Prabhav" },
                new Card {  ImageSource = "emoji6.png", Name = "Card 2",PartnerName = "Prabhav"},
                new Card {   ImageSource = "emoji7.png", Name = "Card 1", PartnerName = "Prabhav" },
                new Card {  ImageSource = "emoji10.png",Name = "Card 2", PartnerName = "Prabhav" },
                new Card { ImageSource = "emoji1.png", Name = "Card 1", PartnerName = "Prabhav"},
                new Card {  ImageSource = "emoji1.png",Name = "Card 2", PartnerName = "Prabhav" },
                new Card {  ImageSource = "emoji1.png",Name = "Card 1", PartnerName = "Prabhav" },
                new Card { ImageSource = "emoji1.png", Name = "Card 2",PartnerName = "Prabhav"},
                // Add more cards as needed
            };

        BindingContext = this;
    }

    private async void MainPage_Loaded(object sender, EventArgs e)
    {
        PermissionStatus = await Permissions.RequestAsync<Permissions.ContactsRead>();
        await Permissions.RequestAsync<Permissions.Camera>();
        Console.WriteLine(PermissionStatus);

        //serviceContact = new IContactsService();
        //IContactsService myInterface = DependencyService.Get<IContactsService>();
       
        //List<Contact> contacts = await myInterface.GetAppContacts();

        ////ContactService contactService = new();
        ////IAsyncEnumerable<string> contactValues = contactService.GetContactNames();

        //foreach (Contact value in contacts)
        //{
        //    // Process each value here
        //    Console.WriteLine(value.FamilyName);
        //}

        List<UsersModel> users = await database.GetItemsAsync();
        Debug.WriteLine("HEre" + users.Count);
        foreach (UsersModel user in users)
        {

            Debug.WriteLine(user.Name);
        }
    }

    public ObservableCollection<Card> Cards { get; }
  

    private void OnCounterClicked(object sender, EventArgs e)
	{
		//count++;

		//if (count == 1)
		//	CounterBtn.Text = $"Clicked {count} time";
		//else
		//	CounterBtn.Text = $"Clicked {count} times";

		//SemanticScreenReader.Announce(CounterBtn.Text);
	}

    private void AddUser_Clicked(object sender, EventArgs e)
    {
        Button button = (Button)sender;
        button.BorderColor = Colors.Black;
        button.BackgroundColor = Colors.White;
        button.TextColor = Colors.Black;
        button.Text = "Added";
    }

    private async void  TapGestureRecognizer_Tapped(System.Object sender, Microsoft.Maui.Controls.TappedEventArgs e)
    {
        await Navigation.PushAsync(new HomeGridView());
    }
}

public class Card
{
    public string ImageSource { get; set; }
    public string Name { get; set; }
    public string PartnerName { get; set; }
}

