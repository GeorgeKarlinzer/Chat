using System;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows;
using System.Windows.Controls;

namespace ChatClient.ViewModel
{
    internal class LoginViewModel : INotifyPropertyChanged
    {
        private readonly IDataService dataLoader;

        private readonly Page page;

        private string username;
        private string password;

        private RelayCommand loginCommand;
        private RelayCommand registerCommand;


        public string Username
        {
            get => username;
            set
            {
                username = value;
                OnPropertyChanged(nameof(Username));
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

        public RelayCommand LoginCommand => loginCommand ??= new(obj =>
        {
            // TODO: Custom DateTime TextBlock
            // TODO: Add images

            try
            {
                SessionContext.Instance.CurrentUser = dataLoader.Login(username, password);
                if (SessionContext.Instance.CurrentUser != null)
                    Window.GetWindow(page).DialogResult = true;
                else
                    MessageBox.Show("Incorrect username or password");
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Something went wrong: {ex.Message}");
            }

        }, obj => !string.IsNullOrWhiteSpace(Username) && !string.IsNullOrWhiteSpace(Password));

        public RelayCommand RegisterCommand => registerCommand ??= new(obj =>
        {
            var window = Window.GetWindow(page);

            var container = (Frame)window.FindName("Page");

            container.Source = new(@"/View/RegisterPage.xaml", System.UriKind.Relative);
        });


        public LoginViewModel(IDataService dataLoader, Page page)
        {
            this.dataLoader = dataLoader;
            this.page = page;
        }

        public event PropertyChangedEventHandler? PropertyChanged;

        public void OnPropertyChanged([CallerMemberName] string prop = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(prop));
        }
    }
}
