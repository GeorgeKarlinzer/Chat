using ChatData;
using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using ChatWCFContracts;
using System.Threading;
using System.Threading.Tasks;

namespace ChatWCFService
{
    public class Service : IService
    {

        private static Dictionary<int, TaskCompletionSource<Message>> newMessagesTasksMap = new Dictionary<int, TaskCompletionSource<Message>>();

        public bool AddFriend(User user, string username)
        {
            var context = new ChatDbContext();

            var friend = context.Users.Join(context.UserCredentials, u => u.Id,
                                                                     c => c.UserId,
                                                                     (u, c) => new { u, c.UserName })
                                      .FirstOrDefault(x => x.UserName == username);

            if (friend == null || friend.u.Id == user.Id)
                return false;

            context.Friends.Add(new Friend() { UserId_1 = user.Id, UserId_2 = friend.u.Id });
            context.SaveChanges();

            return true;
        }

        public List<User> GetFriends(User user)
        {
            var context = new ChatDbContext();

            if (!context.Users.Any(x => x.Id == user.Id))
                return null;

            var friendList = context.Friends.Where(x => x.UserId_1 == user.Id || x.UserId_2 == user.Id);

            var friendIdList = friendList.Select(x => x.UserId_1 == user.Id ? x.UserId_2 : x.UserId_1);

            return context.Users.Where(x => friendIdList.Contains(x.Id)).ToList();
        }

        public List<Message> GetMessages(User user1, User user2)
        {
            var context = new ChatDbContext();

            var messages = context.Messages.Where(x => (x.ReceiverId == user1.Id && x.SenderId == user2.Id)
                                                    || (x.ReceiverId == user2.Id && x.SenderId == user1.Id))
                                           .OrderBy(x => x.Date)
                                           .ToList();

            return messages;
        }

        public void ListenForNewMessages(User user)
        {
            newMessagesTasksMap[user.Id] = new TaskCompletionSource<Message>();
            newMessagesTasksMap[user.Id].Task.Wait();

            IServiceCallback callback = OperationContext.Current.GetCallbackChannel<IServiceCallback>();

            callback?.GetNewMessage(newMessagesTasksMap[user.Id].Task.Result);
        }

        public User Login(string username, byte[] passwordHash)
        {
            Console.WriteLine($"Try to login\n\tusername: {username}");

            var context = new ChatDbContext();

            var credentials = context.UserCredentials.FirstOrDefault(x => x.UserName == username);

            if (credentials == null || !credentials.PasswordHash.SequenceEqual(passwordHash))
            {
                Console.WriteLine("Login failed: incorrect username or password");
                return null;
            }

            var user = context.Users.FirstOrDefault(x => x.Id == credentials.UserId);

            Console.WriteLine($"Login succeed: username: {credentials.UserName}");
            return user;
        }

        public bool Register(string username, byte[] passwordHash, string name, byte[] image)
        {
            Console.WriteLine($"Try to register\n\tusername: {username}\n\tpassword hash: {System.Text.Encoding.Default.GetString(passwordHash)}\n\tname:{name}");

            var context = new ChatDbContext();

            if (context.UserCredentials.Any(x => x.UserName == username))
            {
                Console.WriteLine($"Registeration failed: username is unavailabe");
                return false;
            }


            var newUser = context.Users.Add(new User() { LastSeen = DateTime.Now, Name = name, ProfileImage = image });

            context.SaveChanges();

            context.UserCredentials.Add(new UserCredentials() { UserId = newUser.Id, UserName = username, PasswordHash = passwordHash });

            context.SaveChanges();

            Console.WriteLine($"Registration succeed");
            return true;
        }

        public void SendMessage(Message message)
        {
            var context = new ChatDbContext();

            message.Date = DateTime.Now;

            context.Messages.Add(message);
            context.SaveChanges();

            if (newMessagesTasksMap.ContainsKey(message.ReceiverId))
                newMessagesTasksMap[message.ReceiverId].TrySetResult(message);
        }
    }
}