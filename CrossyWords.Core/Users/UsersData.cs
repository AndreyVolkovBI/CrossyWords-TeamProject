using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace CrossyWords.Core.Users
{
    public class UsersData
    {
        public UsersData()
        {

        }

        public void AddNewUser(string nick, string password)
        {
            var user = new User
            {
                NickName = nick,
                Password = GetHash(password)
            };

            using (var context = new Context())
            {
                context.Users.Add(user);
                context.SaveChanges();
            }

        }

        public bool UniqueNickName(string nickname)
        {
            using (var context = new Context())
            {
                if (context.Users.FirstOrDefault(u => u.NickName == nickname) == null)
                    return true;
                else
                    return false;
            }
        }

        public User FindUser(string nick, string password)
        {
            password = GetHash(password);
            using (var context = new Context())
            {
                var user = context.Users.FirstOrDefault(u => u.NickName == nick && u.Password == password);
                return user;
            }

        }
        private static string GetHash(string password)
        {
            var md5 = MD5.Create();
            var hash = md5.ComputeHash(Encoding.UTF8.GetBytes(
            password));
            return Convert.ToBase64String(hash);
        }
    }
}
