using ShareAble.ViewModel;

namespace ShareAble;

public partial class ImageDetailsView : ContentPage
{
	public ImageDetailsView(PostsDetailViewModel postsDetailViewModel)
	{
		InitializeComponent();
		BindingContext = postsDetailViewModel;
	}
}