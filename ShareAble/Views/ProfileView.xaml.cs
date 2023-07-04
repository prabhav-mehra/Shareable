using ShareAble.ViewModel;

namespace ShareAble;

public partial class ProfileView : ContentPage
{
	public ProfileView(ProfileViewModel profileViewModel)
	{
		InitializeComponent();
		BindingContext = profileViewModel;
	}
}