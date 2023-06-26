namespace ShareAble;

public partial class AppShell : Shell
{
	public AppShell()
	{
		InitializeComponent();

		Routing.RegisterRoute(nameof(MainPage), typeof(MainPage));
        Routing.RegisterRoute(nameof(SignUpName), typeof(SignUpName));
        Routing.RegisterRoute(nameof(HomeGridView), typeof(HomeGridView));
    }
}

