using ChatClient.View;
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
        private readonly IDataService dataLoader;

        private User selectedFriend;
        private List<User> friends;

        private RelayCommand sendCommand;
        private RelayCommand searchFriend;
        private RelayCommand loadMessageCommand;
        private RelayCommand logoutCommand;
        private RelayCommand addFriend;

        public ObservableCollection<User> VisibleFriends { get; set; }

        public ObservableCollection<Message> Messages { get; set; }

        public User SelectedFriend
        {
            get => selectedFriend;
            set
            {
                if (selectedFriend != value && value != null)
                {
                    List<Message> messages = dataLoader.GetMessages(value, SessionContext.Instance.CurrentUser);
                    Messages.Fill(messages);
                }

                if(value == null)
                {
                    Messages.Clear();
                }

                selectedFriend = value;
                OnPropertyChanged(nameof(SelectedFriend));
            }
        }

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
        }, obj => !string.IsNullOrWhiteSpace(((TextBox)obj).Text.Trim()));

        public RelayCommand SearchFriend => searchFriend ??= new(obj =>
        {
            var pattern = (string)obj;

            var friendsToShow = friends.Where(x => x.Name.Contains(pattern));

            VisibleFriends.Fill(friendsToShow);

        });

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

        public RelayCommand LogoutCommand => logoutCommand ??= new(obj =>
        {
            SessionContext.Instance.CurrentUser = null;
            TryLogin();
        });

        public RelayCommand AddFriend => addFriend ??= new(obj =>
        {
            if (new AddFriendDialog().ShowDialog() == true)
            {
                LoadFriends();
            }
        });


        public ApplicationViewModel(IDataService dataLoader)
        {
            this.dataLoader = dataLoader;
            Messages = new();
            VisibleFriends = new();

            TryLogin();
        }

        private void TryLogin()
        {
            if (new LoginDialog().ShowDialog() != true)
            {
                Application.Current.Shutdown();
                return;
            }

            LoadFriends();
            SelectedFriend = VisibleFriends.FirstOrDefault();
        }

        private void LoadFriends()
        {
            friends = this.dataLoader.GetFriends(SessionContext.Instance.CurrentUser);
            VisibleFriends.Fill(friends);
        }

        public event PropertyChangedEventHandler? PropertyChanged;

        public void OnPropertyChanged([CallerMemberName] string prop = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(prop));
        }
    }
}
