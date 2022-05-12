using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using ChatClient.Dto;

namespace ChatClient.ViewModel
{
    internal class LoginViewModel : INotifyPropertyChanged
    {
        private IDataLoader dataLoader;
        private Window dialog;
        private UserCredentials credentials;

        public string UserName
        {
            get => credentials.UserName;
            set
            {
                credentials.UserName = value;
                OnPropertyChanged(nameof(UserName));
            }
        }

        public string Password
        {
            get => credentials.PasswordHash;
            set
            {
                credentials.PasswordHash = value;
                OnPropertyChanged(nameof(Password));
            }
        }

        private RelayCommand loginCommand;
        public RelayCommand LoginCommand => loginCommand ??= new(obj =>
        {
            // TODO: Remove
            credentials.UserName = "Rick123";
            // 
            SessionContext.Instance.CurrentUser = dataLoader.Login(credentials);
            if (SessionContext.Instance.CurrentUser != null)
                dialog.DialogResult = true;

        });

        public LoginViewModel(IDataLoader dataLoader, Window dialog)
        {
            credentials = new();
            this.dataLoader = dataLoader;
            this.dialog = dialog;
        }

        public event PropertyChangedEventHandler? PropertyChanged;

        public void OnPropertyChanged([CallerMemberName] string prop = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(prop));
        }
    }
}
