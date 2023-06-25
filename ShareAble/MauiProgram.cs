using CommunityToolkit.Maui;
using CommunityToolkit.Maui.Core;
using Microsoft.Extensions.Logging;
using ShareAble.Database;
using ShareAble.Interfaces;
using ShareAble.Services;
using ShareAble.ViewModel;

namespace ShareAble;

public static class MauiProgram
{
	public static MauiApp CreateMauiApp()
	{
		var builder = MauiApp.CreateBuilder();
		builder
			.UseMauiApp<App>()
			.UseMauiCommunityToolkit()
			.ConfigureFonts(fonts =>
			{
				fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular");
				fonts.AddFont("OpenSans-Semibold.ttf", "OpenSansSemibold");
			});

#if DEBUG
		builder.Logging.AddDebug();
#endif
		builder.UseMauiApp<App>().UseMauiCommunityToolkitCore();
		builder.Services.AddSingleton<MainPage>();
        builder.Services.AddTransient<SignUpName>();
        builder.Services.AddSingleton<HomeGridView>();
        builder.Services.AddSingleton<UsersItemDatabase>();
		builder.Services.AddTransient<UsersItemViewModel>();
        builder.Services.AddSingleton<IContactsService, ContactsService>();

        return builder.Build();
	}
}

