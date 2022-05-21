﻿using System;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows;
using System.Windows.Controls;

namespace ChatClient.ViewModel
{
    internal class LoginViewModel : INotifyPropertyChanged
    {
        private IDataService dataLoader;

        private Page page;

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
            // TODO: Logout button
            // TODO: Sing in button
            // TODO: Add friend button
            // TODO: Update user's messages, when someone send to him
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

        }, obj => !string.IsNullOrEmpty(Username) && !string.IsNullOrEmpty(Password));

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
