using ShareAble.ViewModel;

namespace ShareAble;

public partial class HomeGridView : ContentPage
{
    PostsViewModel _postsViewModel;
	public HomeGridView(PostsViewModel postsViewModel)
	{
        _postsViewModel = postsViewModel;
        InitializeComponent();
        BindingContext = postsViewModel;
    }

    private async void CapturePhotoButton_Clicked(object sender, EventArgs e)
    {
        Console.WriteLine("Clicked");
        try
        {
            var options = new MediaPickerOptions
            {
                Title = "Select or Capture Photo"
            };

            var photo = await MediaPicker.CapturePhotoAsync(options);
            //var photo = await MediaPicker.CapturePhotoAsync();

            if (photo != null)
            {
                // Use the captured photo
                ImageSource imageSource = ImageSource.FromFile(photo.FullPath);
                byte[] imageBytes = File.ReadAllBytes(photo.FullPath);
                //MyImage.Source = imageSource;
            }
        }
        catch (Exception ex)
        {
            // Handle any exceptions that occur during photo capture
            Console.WriteLine($"Error capturing photo: {ex.Message}");
        }
    }

    void TapGestureRecognizer_Tapped(System.Object sender, Microsoft.Maui.Controls.TappedEventArgs e)
    {
        Console.WriteLine("Clicked22");
    }

    void Button_Clicked(System.Object sender, System.EventArgs e)
    {
    }
}