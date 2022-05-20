using ChatData;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows;

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

        public byte[] Password
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
            // TODO: Custom DateTime TextBlock
            // TODO: Logout button
            // TODO: Sing in button
            // TODO: Add friend button
            // TODO: Update user's messages, when someone send to him
            // TODO: Add images

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
