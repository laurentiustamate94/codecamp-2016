using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using CodeCamp2016.Interfaces;
using Microsoft.ProjectOxford.Face.Contract;

namespace CodeCamp2016.ImageProcessing
{
    public partial class ProjectOxford : IImageProcessing
    {
        private const string FACE_DATABASE_PATH = "Resources";

        private async Task<Face> DetectFace(string imagePath)
        {
            return await Task.Run(() =>
            {
                using (var fileStream = File.OpenRead(imagePath))
                {
                    var faces = faceClient.DetectAsync(fileStream);

                    return faces.Result[0];
                }
            });
        }

        private async Task<Face[]> GetFaceDatabase()
        {
            var faces = new List<Face>();

            if (!Directory.Exists(FACE_DATABASE_PATH))
                return null;

            var files = Directory.GetFiles(FACE_DATABASE_PATH);

            foreach (var file in files)
                faces.Add(await DetectFace(file));

            return faces.ToArray();
        }

        public async Task<bool> RecognizeFace(string imagePath)
        {
            var faceDatabase = await GetFaceDatabase();
            var current = await DetectFace(imagePath);

            foreach (var face in faceDatabase)
            {
                var result = await faceClient.VerifyAsync(face.FaceId, current.FaceId);

                if (result.IsIdentical)
                    return true;
            }

            return false;
        }
    }
}
