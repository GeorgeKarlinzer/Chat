using ChatClient.View;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using ChatClient.Dto;
using System.Windows;

namespace ChatClient.ViewModel
{
    internal class ApplicationViewModel : INotifyPropertyChanged
    {
        private IDataLoader dataLoader;
        private User selectedFriend;

        public ObservableCollection<User> Friends { get; set; }

        public User SelectedFriend
        {
            get => selectedFriend;
            set
            {
                selectedFriend = value;
                OnPropertyChanged(nameof(SelectedFriend));
            }
        }

        public ApplicationViewModel(IDataLoader dataLoader)
        {
            if(SessionContext.Instance.CurrentUser == null)
            {
                var loginDialog = new LoginDialog();
                if(loginDialog.ShowDialog() == false)
                {
                    Application.Current.Shutdown();
                    return;
                }
            }

            this.dataLoader = dataLoader;
            Friends = new(this.dataLoader.GetFriends(SessionContext.Instance.CurrentUser));
        }

        public event PropertyChangedEventHandler? PropertyChanged;

        public void OnPropertyChanged([CallerMemberName] string prop = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(prop));
        }
    }
}
