using ChatData;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows;

namespace ChatClient.ViewModel
{
    internal class LoginViewModel : INotifyPropertyChanged
    {
        private IDataService dataLoader;
        private Window dialog;
        private string username;
        private string password;

        public string UserName
        {
            get => username;
            set
            {
                username = value;
                OnPropertyChanged(nameof(UserName));
            }
        }

        public string Password
        {
            get => password;
            set
            {
                password = value;
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

            SessionContext.Instance.CurrentUser = dataLoader.Login(username, password);
            if (SessionContext.Instance.CurrentUser != null)
                dialog.DialogResult = true;

        });

        public LoginViewModel(IDataService dataLoader, Window dialog)
        {
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
