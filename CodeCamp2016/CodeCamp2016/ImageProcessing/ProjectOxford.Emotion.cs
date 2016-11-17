using System.IO;
using System.Threading.Tasks;
using CodeCamp2016.Interfaces;
using Microsoft.ProjectOxford.Emotion.Contract;

namespace CodeCamp2016.ImageProcessing
{
    public partial class ProjectOxford : IImageProcessing
    {
        public async Task<string> RecognizeEmotion(string imagePath)
        {
            var result = string.Empty;
            Emotion[] emotionResult = null;

            return await Task.Run(() =>
            {
                if (!File.Exists(imagePath))
                    return null;

                using (var stream = File.Open(imagePath, FileMode.Open))
                    emotionResult = emotionClient.RecognizeAsync(stream).Result;

                return GetHighestEmotion(emotionResult[0].Scores);
            });
        }

        private string GetHighestEmotion(Scores scores)
        {
            var points = new float[]
            {
                scores.Anger,
                scores.Contempt,
                scores.Disgust,
                scores.Fear,
                scores.Happiness,
                scores.Neutral,
                scores.Sadness,
                scores.Surprise
            };

            int pos = GetEmotionPosition(points);

            return ConvertEmotionToString(pos);
        }

        private int GetEmotionPosition(float[] points)
        {
            float max = -100;

            for (int i = 0; i < points.Length; i++)
                if (points[i] > max)
                    max = points[i];

            int pos = -1;

            for (int i = 0; i < points.Length; i++)
                if (max == points[i])
                {
                    pos = i;
                    break;
                }

            return pos;
        }

        private string ConvertEmotionToString(int pos)
        {
            if (pos == 0)
                return "Anger";

            if (pos == 1)
                return "Contempt";

            if (pos == 2)
                return "Disgust";

            if (pos == 3)
                return "Fear";

            if (pos == 4)
                return "Happiness";

            if (pos == 5)
                return "Neutral";

            if (pos == 6)
                return "Sadness";

            if (pos == 7)
                return "Surprise";

            return "Unknown";
        }
    }
}
