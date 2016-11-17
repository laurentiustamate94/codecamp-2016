using System.Threading.Tasks;

namespace CodeCamp2016.Interfaces
{
    public interface IImageSource
    {
        IPhotoStorage Storage { get; set; }

        Task InitializeDevice();

        void TakePhoto(string filename);
    }
}
