
using ShareAble.Database;
using ShareAble.Interfaces;
using ShareAble.Model;
using ShareAble.ViewModel;
using SQLite;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;

namespace ShareAble;

public partial class MainPage : ContentPage
{
    PartnerItemDatabase database;

    public PermissionStatus PermissionStatus { get; set; }
    public IContactsService serviceContact { get; set; }

    private ContactsViewModel _customViewModel;
    LocalUsersDatabase _localDb;

    public MainPage(PartnerItemDatabase usersItemDatabase, ContactsViewModel contactsViewModel, LocalUsersDatabase localdb)
    {

        InitializeComponent();
        database = usersItemDatabase;
        _localDb = localdb;
        Loaded += MainPage_Loaded;

        BindingContext = contactsViewModel;
    }

    private async void MainPage_Loaded(object sender, EventArgs e)
    {
        PermissionStatus = await Permissions.RequestAsync<Permissions.ContactsRead>();
        await Permissions.RequestAsync<Permissions.Camera>();

        await Permissions.RequestAsync<Permissions.LocationWhenInUse>();
        Console.WriteLine(PermissionStatus);
        //Preferences.Set("CurrentUserId", "2");
        //await _localDb.DeleteAllItemAsync();

        //LocalUser localUser = new LocalUser
        //{
        //    Name = "Shivani Bedi",
        //    ID = 0,
        //    ContactNumber = 123,
        //    DOB = "07/07/1980",
        //    ImageSource = "emoji2.png",
        //    HasPartner = false,

        //};

        //LocalUser localUser1 = new LocalUser
        //{
        //    Name = "Prabhav Mehra",
        //    ID = 0,
        //    ContactNumber = 123,
        //    DOB = "07/07/1980",
        //    ImageSource = "emoji2.png",
        //    HasPartner = false,

        //};


        //await _localDb.SaveItemAsync(localUser);
        //await _localDb.SaveItemAsync(localUser1);
        //List<LocalUser> localUsers = await _localDb.GetItemsAsync();
        //foreach (LocalUser localUser123 in localUsers)
        //{
        //    Console.WriteLine($"ID: {localUser123.ID}");
        //    Console.WriteLine($"Name: {localUser123.Name}");
        //    Console.WriteLine($"HasPartner: {localUser123.HasPartner}");
        //    // Print other properties as needed
        //    Console.WriteLine("-------------------------");
        //}


        //serviceContact = new IContactsService();
        //IContactsService myInterface = DependencyService.Get<IContactsService>();

        //List<Contact> contacts = await myInterface.GetAppContacts();

        ////ContactService contactService = new();
        ////IAsyncEnumerable<string> contactValues = contactService.GetContactNames();
    }

    private void AddUser_Clicked(object sender, EventArgs e)
    {
        Button button = (Button)sender;
        button.BorderColor = Colors.Black;
        button.BackgroundColor = Colors.White;
        button.TextColor = Colors.Black;
        button.Text = "Added";
    }

    private async void TapGestureRecognizer_Tapped(System.Object sender, Microsoft.Maui.Controls.TappedEventArgs e)
    {
        await Shell.Current.GoToAsync(nameof(HomeGridView));
    }
}
