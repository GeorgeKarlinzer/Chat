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
        private IDataService dataService;

        private Window dialog;

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

        });

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
