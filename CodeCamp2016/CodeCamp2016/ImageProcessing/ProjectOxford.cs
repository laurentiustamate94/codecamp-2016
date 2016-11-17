using CodeCamp2016.Interfaces;
using Microsoft.ProjectOxford.Emotion;
using Microsoft.ProjectOxford.Face;
using Microsoft.ProjectOxford.Vision;
using Windows.ApplicationModel.Resources;

namespace CodeCamp2016.ImageProcessing
{
    public partial class ProjectOxford : IImageProcessing
    {
        private readonly IVisionServiceClient visionClient;
        private readonly IFaceServiceClient faceClient;
        private readonly IEmotionServiceClient emotionClient;

        public ProjectOxford()
        {
            var appSettings = ResourceLoader.GetForViewIndependentUse("AppSettings");

            visionClient = new VisionServiceClient(appSettings.GetString("VISION_API_KEY"));
            faceClient = new FaceServiceClient(appSettings.GetString("FACE_API_KEY"));
            emotionClient = new EmotionServiceClient(appSettings.GetString("EMOTION_API_KEY"));
        }
    }
}
