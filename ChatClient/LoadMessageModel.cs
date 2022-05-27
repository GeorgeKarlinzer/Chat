using System.Windows;
using System.Windows.Controls;

namespace ChatClient
{
    public class LoadMessageModel
    {
        public static DependencyProperty WidgetProperty { get; private set; }

        public Grid Grid { get; set; }
        public ListBox ListBox { get; set; }
    }
}
