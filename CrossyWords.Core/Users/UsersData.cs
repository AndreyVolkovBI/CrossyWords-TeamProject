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

        public bool AllowChanges(string name)
        {
            if (name == User.Name)
                return true;
            else if (UniqueName(name))
                return true;
            else
                return false;
        }

        public void ChangeUserInformation(string name = null, string password = null)
        {

            if (password !=null)
            {
                password = GetHash(password);
            }
            
            using (var context = new Context())
            {
                if (name != null && password != null)
                {
                    context.Users.First(u => u.Id == User.Id).Name = name;
                    context.Users.First(u => u.Id == User.Id).Password = password;
                }
                else if(name !=null)
                {
                    context.Users.First(u => u.Id == User.Id).Name = name;
                }
                else if(password !=null)
                {
                    context.Users.First(u => u.Id == User.Id).Password = password;
                }
                context.SaveChanges();
            }
            
        }

        public void SendReview(string message)
        {
            Review _review = new Review { Text = message };
            using (var context = new Context())
            {
                context.Reviews.Add(_review);
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

        public List<User> FindOpponents(string begginingOfName)
        {
            using (var context = new Context())
            {
                var users = context.Users.Where(s => s.Name.Contains(begginingOfName)).ToList();
                return users;
            }
        }

        public void CreateBattle(User opponent)
        {
            var Battle = new Battle
            {
                User_1 = User,
                User_2 = opponent

            };

            using (var context = new Context())
            {
                context.Battles.Add(Battle);
                context.SaveChanges();
            }
        }

        public List<Battle> GetAllCurrentBattles()
        {
            using (var context = new Context())
            {
                var battles = context.Battles.Include("User_1").Include("User_2").Where(b => b.User_1.Id == User.Id || b.User_2.Id == User.Id).ToList();
                return battles;
            }
        }
        
  
    }
}
