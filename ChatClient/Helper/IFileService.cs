using System.Windows.Media;

namespace ChatClient.Helper
{
    internal interface IFileService
    {
        ImageSource OpenImage(string path);
    }
}
