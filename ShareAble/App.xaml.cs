

namespace ShareAble;

public partial class App : Application
{
	public App()
	{
		InitializeComponent();

        SetupHandlers();

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
    private void SetupHandlers()
    {
        Microsoft.Maui.Handlers.EntryHandler.Mapper.AppendToMapping("MyCustomization", (handler, view) =>
        {
            if (view is BorderlessEntry)
            {
#if ANDROID
                //handler.PlatformView.BackgroundTintList =
                //    Android.Content.Res.ColorStateList.ValueOf(Colors.Transparent.ToAndroid());
#endif
#if IOS || MACCATALYST
                handler.PlatformView.BorderStyle = UIKit.UITextBorderStyle.None;
#endif
            }
        });
    }

    private bool InitAsync()
    {
        bool signedUp = bool.Parse(Preferences.Get("SignedUp", "false"));
        return signedUp;
    }
}

public class BorderlessEntry : Entry
{
    public BorderlessEntry()
    {
    }
}

