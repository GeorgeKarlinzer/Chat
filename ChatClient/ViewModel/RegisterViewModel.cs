using ChatData;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;

namespace ChatClient.ViewModel
{
    internal class RegisterViewModel : INotifyPropertyChanged
    {
        private IDataService dataService;

        private Page page;

        private string username;
        private string password;
        private string name;

        private RelayCommand registerCommand;
        private RelayCommand loginCommand;


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

        public string Name
        {
            get => name;
            set
            {
                name = value;
                OnPropertyChanged(nameof(Name));
            }
        }

        public RelayCommand RegisterCommand => registerCommand ??= new(obj =>
        {
            try
            {
                if (dataService.Register(Username, Password, Name, null))
                {
                    LoginCommand.Execute(null);
                }
                else
                {
                    MessageBox.Show("Username is unavailable");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Something went wrong: {ex.Message}");
            }
        }, obj => !string.IsNullOrEmpty(Name) && !string.IsNullOrEmpty(Password) && !string.IsNullOrEmpty(Username));

        public RelayCommand LoginCommand => loginCommand ??= new(obj =>
        {
            var window = Window.GetWindow(page);

            var container = (Frame)window.FindName("Page");

            container.Source = new(@"/View/LoginPage.xaml", UriKind.Relative);
        });

        public RegisterViewModel(IDataService dataService, Page page)
        {
            this.dataService = dataService;
            this.page = page;
        }


        public event PropertyChangedEventHandler? PropertyChanged;

        public void OnPropertyChanged([CallerMemberName] string prop = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(prop));
        }
    }
}
