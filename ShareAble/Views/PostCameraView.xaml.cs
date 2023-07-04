using Camera.MAUI;
using ShareAble.ViewModel;

namespace ShareAble;

public partial class PostCameraView : ContentPage
{
	public PostCameraView(PostsViewModel postsViewModel)
	{

		InitializeComponent();
        BindingContext = postsViewModel;

        cameraView.CamerasLoaded += CameraView_CamerasLoaded;
    }

    private void CameraView_CamerasLoaded(object sender, EventArgs e)
    {
        if (cameraView.NumCamerasDetected > 0)
        {
            if (cameraView.NumMicrophonesDetected > 0)
                cameraView.Microphone = cameraView.Microphones.First();
            cameraView.Camera = cameraView.Cameras.First();
            MainThread.BeginInvokeOnMainThread(async () =>
            {
               
                if (await cameraView.StartCameraAsync() == CameraResult.Success)
                {
                    
                    //controlButton.Text = "Stop";
                    //playing = true;
                }
            });
        }
    }

    private void TapGestureRecognizer_Tapped(object sender, TappedEventArgs e)
    {
        Console.WriteLine(cameraView.NumCamerasDetected);
        Console.WriteLine(cameraView.Cameras.Count);
        if (cameraView.NumCamerasDetected > 0)
        {
            if (cameraView.NumMicrophonesDetected > 0)
            {


                if (cameraView.Camera == cameraView.Cameras.First())
                {
                    // If the current camera is the front camera, switch to the back camera
                    cameraView.Camera = cameraView.Cameras.Last();
                    cameraView.Microphone = cameraView.Microphones.Last();
                    
                }
                else
                {
                    // Otherwise, switch to the front camera
                    cameraView.Camera = cameraView.Cameras.First();
                    cameraView.Microphone = cameraView.Microphones.First();
                    
                }
            }
            MainThread.BeginInvokeOnMainThread(async () =>
            {
               
                if (await cameraView.StartCameraAsync() == CameraResult.Success)
                {

                    //controlButton.Text = "Stop";
                    //playing = true;
                }
            });
        }
    }
}