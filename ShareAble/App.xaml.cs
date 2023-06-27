using ShareAble.Database;
using ShareAble.Model;
using ShareAble.ViewModel;

namespace ShareAble;

public partial class App : Application
{
	public App()
	{
		InitializeComponent();

		MainPage = new AppShell();
	}

    protected override async void OnStart()
    {
        bool signedUp = InitAsync();
        Console.WriteLine(signedUp);
        if (signedUp)
        {
            await Shell.Current.GoToAsync(nameof(MainPage));
            return;
        }

        await Shell.Current.GoToAsync("///" + nameof(SignUpName));
    }

    private bool InitAsync()
    {
        bool signedUp = bool.Parse(Preferences.Get("SignedUp", "false"));
        return signedUp;
    }
}

