using System;
using CodeCamp2016.ImageProcessing;
using CodeCamp2016.ImageSource;
using CodeCamp2016.Interfaces;
using CodeCamp2016.Storage;
using Windows.Media.Capture;
using Windows.Media.SpeechSynthesis;
using Windows.UI.Xaml.Controls;

// The Blank Page item template is documented at http://go.microsoft.com/fwlink/?LinkId=402352&clcid=0x409

namespace CodeCamp2016
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class MainPage : Page
    {
        private SpeechSynthesizer textToSpeech;
        private MediaCapture mediaDevice;
        private readonly string PHOTO_FILE_NAME = "photo.jpg";

        private IImageProcessing imageProcessing;
        private IPhotoStorage localPhotoStorage;
        private IImageSource localCameraImageSource;

        public MainPage()
        {
            InitializeComponent();
            InitializeApplication();
        }

        private async void InitializeApplication()
        {
            textToSpeech = new SpeechSynthesizer();
            mediaDevice = new MediaCapture();

            imageProcessing = new ProjectOxford();
            localPhotoStorage = new LocalPhotoStorage(mediaDevice);
            localCameraImageSource = new LocalCameraImageSource(localPhotoStorage, mediaDevice);

            await localCameraImageSource.InitializeDevice();

            interogationResult.Text = "Hello my dear friend !!";

            PlayText(interogationResult.Text);

            cameraElement.Source = mediaDevice;

            await mediaDevice.StartPreviewAsync();
        }

        private async void PlayText(string text)
        {
            var stream = await textToSpeech.SynthesizeTextToStreamAsync(text);

            mediaElementUI.SetSource(stream, stream.ContentType);
            mediaElementUI.Play();
        }

        private async void GetShirtColorButton_Click(object sender, Windows.UI.Xaml.RoutedEventArgs e)
        {
            await localPhotoStorage.Save(PHOTO_FILE_NAME);

            var result = await imageProcessing.GetDominantForegroundColor(localPhotoStorage.GetLastPhotoSaved());

            interogationResult.Text = string.Format("Your shirt is {0} !!", result);

            PlayText(interogationResult.Text);
        }

        private async void ReadMeSomeTextButton_Click(object sender, Windows.UI.Xaml.RoutedEventArgs e)
        {
            await localPhotoStorage.Save(PHOTO_FILE_NAME);

            var result = await imageProcessing.RecognizeText(localPhotoStorage.GetLastPhotoSaved());

            interogationResult.Text = result;

            PlayText(interogationResult.Text);
        }

        private async void AmIFamiliarButton_Click(object sender, Windows.UI.Xaml.RoutedEventArgs e)
        {
            await localPhotoStorage.Save(PHOTO_FILE_NAME);

            var result = await imageProcessing.RecognizeFace(localPhotoStorage.GetLastPhotoSaved());

            if (result)
                interogationResult.Text = string.Format("Hello !! It's glad to see you !!");
            else
                interogationResult.Text = string.Format("I do not recognize you, I'm sorry !!");

            PlayText(interogationResult.Text);
        }

        private async void WhatsMyMoodButton_Click(object sender, Windows.UI.Xaml.RoutedEventArgs e)
        {
            await localPhotoStorage.Save(PHOTO_FILE_NAME);

            var result = await imageProcessing.RecognizeEmotion(localPhotoStorage.GetLastPhotoSaved());

            interogationResult.Text = string.Format("Your emotion is: {0}", result);

            PlayText(interogationResult.Text);
        }

        private void ResetButton_Click(object sender, Windows.UI.Xaml.RoutedEventArgs e)
        {
            interogationResult.Text = "Hello my dear friend !!";

            mediaElementUI.Stop();
            PlayText(interogationResult.Text);
        }
    }
}
