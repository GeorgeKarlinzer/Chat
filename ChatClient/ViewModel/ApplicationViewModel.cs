using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace ChatClient.ViewModel
{
    internal class ApplicationViewModel : INotifyPropertyChanged
    {
        public ObservableCollection<User> Friends { get; set; }

        public User SelectedFriend { get; set; }

        public ApplicationViewModel()
        {
            var localDL = new LocalDataLoader();
            Friends = new(localDL.GetFriends(null));
        }

        public event PropertyChangedEventHandler? PropertyChanged;

        public void OnPropertyChanged([CallerMemberName] string prop = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(prop));
        }
    }
}
