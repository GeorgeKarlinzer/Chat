using ChatData;
using System;
using System.Collections.Generic;
using System.Linq;

namespace ChatWCFService
{
    public class Service : IService
    {
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

        public User Login(string username, byte[] passwordHash)
        {
            var context = new ChatDbContext();

            var credentials = context.UserCredentials.FirstOrDefault(x => x.UserName == username);

            if (credentials == null || !credentials.PasswordHash.SequenceEqual(passwordHash))
                return null;

            return context.Users.FirstOrDefault(x => x.Id == credentials.UserId);
        }

        public bool Register(string username, byte[] passwordHash, string name, byte[] image)
        {
            var context = new ChatDbContext();

            if (context.UserCredentials.Any(x => x.UserName == username))
                return false;


            var newUser = context.Users.Add(new User() { LastSeen = DateTime.Now, Name = name, ProfileImage = image });

            context.SaveChanges();

            context.UserCredentials.Add(new UserCredentials() { UserId = newUser.Id, UserName = username, PasswordHash = passwordHash });

            context.SaveChanges();

            return true;
        }

        public void SendMessage(Message message)
        {
            var context = new ChatDbContext();

            message.Date = DateTime.Now;

            context.Messages.Add(message);
            context.SaveChanges();

            // TODO: Add notifications
        }
    }
}