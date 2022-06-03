using System.Windows.Media.Imaging;

namespace ChatClient.Helper
{
    internal interface IFileService
    {
        BitmapSource OpenImage(string path);
    }
}
