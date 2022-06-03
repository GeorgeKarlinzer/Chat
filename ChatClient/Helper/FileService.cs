using System.Drawing;
using System.Windows.Media.Imaging;

namespace ChatClient.Helper
{
    internal class FileService : IFileService
    {
        public BitmapSource OpenImage(string path)
        {
            var image = Image.FromFile(path);

            var bitmap = new BitmapImage();
            bitmap.BeginInit();
            var memoryStream = new System.IO.MemoryStream();
            image.Save(memoryStream, System.Drawing.Imaging.ImageFormat.Png);
            memoryStream.Seek(0, System.IO.SeekOrigin.Begin);
            bitmap.StreamSource = memoryStream;
            bitmap.EndInit();

            return bitmap;
        }
    }
}
