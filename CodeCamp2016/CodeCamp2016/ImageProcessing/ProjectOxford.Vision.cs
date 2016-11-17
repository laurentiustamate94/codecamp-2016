using System.IO;
using System.Threading.Tasks;
using CodeCamp2016.Interfaces;
using Microsoft.ProjectOxford.Vision.Contract;

namespace CodeCamp2016.ImageProcessing
{

    public partial class ProjectOxford : IImageProcessing
    {
        public async Task<object> AnalyzeImage(string imagePath)
        {
            var result = string.Empty;
            AnalysisResult analysisResult = null;

            return await Task.Run(() =>
            {
                if (!File.Exists(imagePath))
                    return null;

                using (var stream = File.Open(imagePath, FileMode.Open))
                {
                    analysisResult = visionClient.AnalyzeImageAsync(stream, new string[] { "Color" }).Result;

                    return analysisResult;
                }
            });
        }

        public async Task<string> RecognizeText(string imagePath)
        {
            var result = string.Empty;
            OcrResults ocrResult = null;

            return await Task.Run(() =>
            {
                if (!File.Exists(imagePath))
                    return null;

                using (FileStream stream = File.Open(imagePath, FileMode.Open))
                {
                    ocrResult = visionClient.RecognizeTextAsync(stream, LanguageCodes.AutoDetect, true).Result;
                }

                return ocrResult.GetNaturalText();
            });
        }

        public async Task<string> GetDominantForegroundColor(string imagePath)
        {
            return ((AnalysisResult)await AnalyzeImage(imagePath)).Color.DominantColorForeground;
        }
    }

    public static class OcrResultExtentions
    {
        public static string GetNaturalText(this OcrResults result)
        {
            var newLine = '\n';
            var text = string.Empty;

            if (result == null && result.Regions == null)
                return null;

            foreach (var item in result.Regions)
            {
                foreach (var line in item.Lines)
                {
                    foreach (var word in line.Words)
                        text += string.Format("{0} ", word.Text);

                    text += newLine;
                }

                text += newLine;
            }

            return text;
        }
    }
}
