using CrossyWords.Core.Users;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace CrossyWords.Core
{
    public class UsersData
    {
        public User User { get; set; }

        public UsersData()
        {

        }

        public void AddNewUser(string name, string password)
        {
            var user = new User
            {
                Name = name,
                Password = GetHash(password)
            };

            using (var context = new Context())
            {
                context.Users.Add(user);
                context.SaveChanges();
            }

        }

        public bool UniqueName(string name)
        {

            using (var context = new Context())
            {
                if (context.Users.FirstOrDefault(u => u.Name == name) == null)
                    return true;
                else
                    return false;
            }

        }


        public bool FindUser(string name, string password)
        {
            password = GetHash(password);
            using (var context = new Context())
            {
                var user = context.Users.FirstOrDefault(u => u.Name == name && u.Password == password);
                if (user == null)                
                    return false;
                else
                {
                    User = user;
                    return true;
                }
                
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
