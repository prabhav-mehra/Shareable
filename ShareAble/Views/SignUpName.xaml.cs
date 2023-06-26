
using CommunityToolkit.Maui.Core.Platform;
using CommunityToolkit.Maui.Extensions;
using CommunityToolkit.Mvvm.Messaging;
using ShareAble.Database;
using ShareAble.ViewModel;
using System.ComponentModel;
using System.Diagnostics;
using System.Globalization;


namespace ShareAble;
public partial class SignUpName : ContentPage
{
    UsersItemViewModel _usersItemViewModel;
    PartnerItemDatabase database;

    public SignUpName(PartnerItemDatabase usersItemDatabase, UsersItemViewModel usersViewModel)
    {
       
		entryName.ShowKeyboardAsync(CancellationToken.None);
        InitializeComponent();

        _usersItemViewModel = usersViewModel;
        database = usersItemDatabase;

        BindingContext = usersViewModel;

        WeakReferenceMessenger.Default.Register<MyMessage>(this, (r, m) =>
        {
            OnMessageReceived(m.Value);
        });
    }

    private async void OnMessageReceived(string value)
    {
        await Shell.Current.GoToAsync(nameof(MainPage));
        //await Navigation.PushAsync(new MainPage(database));
    }

    
    private void OnEntryChanged(object sender, TextChangedEventArgs e)
    {
        if(_usersItemViewModel.CurrentStep == 0)
        {
            buttonContinue.IsEnabled = !string.IsNullOrWhiteSpace(e.NewTextValue);
            buttonContinue.BackgroundColor = buttonContinue.IsEnabled.CompareTo(true) == 0 ? Color.FromRgb(255, 106, 233) : Colors.Gray;
        }
    }

    private void VerifyDateOfBirth()
    {
        string dateString = $"{dayEntry.Text}/{monthEntry.Text}/{yearEntry.Text}";
        DateTime date;

        if (DateTime.TryParseExact(dateString, "dd/MM/yyyy", CultureInfo.InvariantCulture, DateTimeStyles.None, out date))
        {
            Debug.WriteLine("Date is valid");
            buttonContinue.IsEnabled = true;
           
        }
        else
        {
            Debug.WriteLine("date invalid");
            buttonContinue.IsEnabled = false;
           
        }
        buttonContinue.BackgroundColor = buttonContinue.IsEnabled.CompareTo(true) == 0 ? Color.FromRgb(255, 106, 233) : Colors.Gray;

    }

    private void yearEntry_TextChanged(object sender, TextChangedEventArgs e)
    {
        VerifyDateOfBirth();
    }

    private void monthEntry_TextChanged(object sender, TextChangedEventArgs e)
    {
        VerifyDateOfBirth();
        if (monthEntry.Text.Length >= 2)
        {
            yearEntry.Focus();
        }
    }

    private void dayEntry_TextChanged(object sender, TextChangedEventArgs e)
    {
        VerifyDateOfBirth();
        if (dayEntry.Text.Length >= 2)
        {
            monthEntry.Focus();
        }
    }

    private void contactEntry_TextChanged(object sender, TextChangedEventArgs e)
    {
        buttonContinue.IsEnabled = true;
        buttonContinue.BackgroundColor = buttonContinue.IsEnabled.CompareTo(true) == 0 ? Color.FromRgb(255, 106, 233) : Colors.Gray;
    }
}
