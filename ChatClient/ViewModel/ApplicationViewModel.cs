﻿using ChatClient.View;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using ChatData;
using System.Windows;
using System.Windows.Controls;
using System.Configuration;

namespace ChatClient.ViewModel
{
    internal class ApplicationViewModel : INotifyPropertyChanged
    {
        private IDataLoader dataLoader;
        private User selectedFriend;

        private List<User> friends;
        public ObservableCollection<User> VisibleFriends { get; set; }

        public ObservableCollection<Message> Messages { get; set; }

        public User SelectedFriend
        {
            get => selectedFriend;
            set
            {
                if (selectedFriend != value && value != null)
                {
                    Messages.Clear();
                    var messages = dataLoader.GetMessages(value, SessionContext.Instance.CurrentUser);
                    foreach (var message in messages)
                        Messages.Add(message);
                }
                selectedFriend = value;
                OnPropertyChanged(nameof(SelectedFriend));
            }
        }

        private RelayCommand sendCommand;
        public RelayCommand SendCommand => sendCommand ??= new(obj =>
        {
            var maxMessageLength = int.Parse(ConfigurationManager.AppSettings.Get("MaxMessageLength"));
            
            var message = new Message
            {
                Text = ((TextBox)obj).Text,
                SenderId = SessionContext.Instance.CurrentUser.Id,
                ReceiverId = SelectedFriend.Id
            };

            message.Text = message.Text.Trim();

            while (message.Text.Length > maxMessageLength)
            {
                var newMessage = new Message();
                newMessage.SenderId = message.SenderId;
                newMessage.ReceiverId = message.ReceiverId;
                newMessage.Date = message.Date;
                newMessage.Text = message.Text[..maxMessageLength];
                message.Text = message.Text[maxMessageLength..];

                dataLoader.SendMessage(newMessage);
                Messages.Add(newMessage);
            }

            dataLoader.SendMessage(message);
            Messages.Add(message);
            
            ((TextBox)obj).Text = string.Empty;
        }, obj => !string.IsNullOrEmpty(((TextBox)obj).Text.Trim()));

        private RelayCommand searchFriend;
        public RelayCommand SearchFriend => searchFriend ??= new(obj =>
        {
            var pattern = (string)obj;

            var friendsToShow = friends.Where(x => x.Name.Contains(pattern));

            VisibleFriends.Clear();

            foreach (var friend in friendsToShow)
            {
                VisibleFriends.Add(friend);
            }
        });

        private RelayCommand loadMessageCommand;
        public RelayCommand LoadMessageCommand => loadMessageCommand ??= new(obj =>
        {
            var panel = obj as Grid;
            var senderId = int.Parse((panel.Children[0] as TextBlock).Text);

            if (senderId == SessionContext.Instance.CurrentUser.Id)
                foreach (FrameworkElement c in panel.Children)
                {
                    c.SetValue(Grid.ColumnProperty, 1);
                    c.SetValue(Grid.ColumnProperty, 1);
                }
        });

        public ApplicationViewModel(IDataLoader dataLoader)
        {
            if (SessionContext.Instance.CurrentUser == null)
            {
                var loginDialog = new LoginDialog();
                if (loginDialog.ShowDialog() == false)
                {
                    Application.Current.Shutdown();
                    return;
                }
            }

            this.dataLoader = dataLoader;
            friends = this.dataLoader.GetFriends(SessionContext.Instance.CurrentUser);
            VisibleFriends = new(friends);
            Messages = new();
            SelectedFriend = VisibleFriends.FirstOrDefault();
        }

        public event PropertyChangedEventHandler? PropertyChanged;

        public void OnPropertyChanged([CallerMemberName] string prop = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(prop));
        }

        private void SendMessage()
        {

        }

        private void UpdateMessageUI()
        {

        }
    }
}
