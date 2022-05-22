using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace ChatClient.ViewModel
{
    internal class AddFriendViewModel : INotifyPropertyChanged
    {
        private readonly IDataService dataService;

        private readonly Window dialog;

        private string username;

        private RelayCommand addFriendCommand;

        public string Username
        {
            get => username;
            set
            {
                username = value;
                OnPropertyChanged(nameof(Username));
            }
        }

        public RelayCommand AddFriendCommand => addFriendCommand ??= new(obj =>
        {
            bool res = dataService.AddFriend(SessionContext.Instance.CurrentUser, Username);

            if (!res)
            {
                MessageBox.Show($"Cannot add friend {Username}");
            }
            else
            {
                dialog.DialogResult = true;
            }

        }, obj => !string.IsNullOrWhiteSpace(Username));

        public AddFriendViewModel(IDataService dataService, Window dialog)
        {
            this.dataService = dataService;
            this.dialog = dialog;
        }

        public event PropertyChangedEventHandler? PropertyChanged;

        public void OnPropertyChanged([CallerMemberName] string prop = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(prop));
        }
    }
}
