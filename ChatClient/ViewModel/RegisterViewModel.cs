using ChatClient.Helper;
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
using System.Windows.Media;
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
        private ImageSource image;

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

        public ImageSource Image
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
                BitmapEncoder encoder = BitmapEncoder.Create();

                byte[] bytes = null;
                var bitmapSource = imageSource as BitmapSource;

                if (bitmapSource != null)
                {
                    encoder.Frames.Add(BitmapFrame.Create(bitmapSource));

                    using (var stream = new MemoryStream())
                    {
                        encoder.Save(stream);
                        bytes = stream.ToArray();
                    }
                }


                if (dataService.Register(Username, Password, Name, Image))
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
