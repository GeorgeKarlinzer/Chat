using ChatClient.Helper;
using System;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media.Imaging;

namespace ChatClient.ViewModel
{
    internal class RegisterViewModel : INotifyPropertyChanged
    {
        private readonly IDataService dataService;
        private readonly IFileService fileService;
        private readonly IDialogService dialogService;

        private readonly Page page;

        private string username;
        private string password;
        private string name;
        private BitmapSource image;

        private RelayCommand registerCommand;
        private RelayCommand loginCommand;
        private RelayCommand loadImageCommand;

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

        public BitmapSource Image
        {
            get => image;
            set
            {
                image = value;
                OnPropertyChanged(nameof(Image));
            }
        }

        public RelayCommand RegisterCommand => registerCommand ??= new(obj =>
        {
            try
            {
                var imageBytes = Image.ToBitmap().GetBytes();

                if (dataService.Register(Username, Password, Name, imageBytes))
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
        }, obj => !string.IsNullOrWhiteSpace(Name) 
                && !string.IsNullOrWhiteSpace(Password) 
                && !string.IsNullOrWhiteSpace(Username)
        );

        public RelayCommand LoginCommand => loginCommand ??= new(obj =>
        {
            var window = Window.GetWindow(page);

            var container = (Frame)window.FindName("Page");

            container.Source = new(@"/View/LoginPage.xaml", UriKind.Relative);
        });

        public RelayCommand LoadImageCommand => loadImageCommand ??= new(obj =>
        {
            try
            {
                if (dialogService.OpenFileDialog() == true)
                {
                    Image = fileService.OpenImage(dialogService.FilePath);

                    dialogService.ShowMessage("Loaded");
                }
            }
            catch (Exception ex)
            {
                dialogService.ShowMessage(ex.Message);
            }
        });

        public RegisterViewModel(IDataService dataService, Page page)
        {
            this.dataService = dataService;
            this.page = page;
            this.dialogService = new DialogService();
            this.fileService = new FileService();
        }


        public event PropertyChangedEventHandler? PropertyChanged;

        public void OnPropertyChanged([CallerMemberName] string prop = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(prop));
        }
    }
}
