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

       

        public void ChangeUserInformation(string name = null, string password = null)
        {
            if (password !=null)
                password = GetHash(password);
            
            using (var context = new Context())
            {
                if (password == null)
                {
                    context.Users.First(u => u.Id == User.Id).Name = name;
                    User.Name = name;
                }
                else
                {
                    context.Users.First(u => u.Id == User.Id).Password = password;
                    User.Password = password;
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
                var users = context.Users.Where(u => u.Name.Contains(begginingOfName) && u.Id != User.Id && context.Battles.FirstOrDefault(b => b.User_1.Id == User.Id && b.User_2.Id == u.Id || b.User_1.Id == u.Id && b.User_2.Id == User.Id) == null).ToList();
                return users;
            }
        }

        public bool IsMakingBattleAllowable()
        {
            using (var context = new Context())
            {
                var users = context.Users.Where(u => u.Id != User.Id && context.Battles.FirstOrDefault(b => b.User_1.Id == User.Id && b.User_2.Id == u.Id || b.User_1.Id == u.Id && b.User_2.Id == User.Id) == null).ToList();
                if (users.Count == 0)
                    return false;
                else
                    return true;
            }
        }


        public bool CheckLimitationsInGames()
        {
            using (var context = new Context())
            {
                var battles = context.Battles.Where(b => b.User_1.Id == User.Id || b.User_2.Id == User.Id).ToList();

                if (battles.Count > 4)
                    return false;
                else
                    return true;
            }
        }


        public void MakeRandomBattle()
        {
            User opponent;
            using (var context = new Context())
            {
                Random r = new Random();
                do
                {
                    int random = r.Next(0, context.Users.Count());
                    opponent = context.Users.ToList()[random];
                } while (opponent.Id == User.Id || context.Battles.FirstOrDefault(b => b.User_1.Id == User.Id && b.User_2.Id == opponent.Id || b.User_1.Id == opponent.Id && b.User_2.Id == User.Id) != null);
            }
            MakeBattle(opponent);
        }

        public void MakeBattle(User opponent)
        {  
            using (var context = new Context())
            {
                context.Battles.Add(new Battle { User_1 = context.Users.First(u => u.Id == User.Id), User_2 = context.Users.First(u => u.Id == opponent.Id)});
                context.SaveChanges();
            }
        }

        public List<BattleForInfo> GetAllCurrentBattles()
        {
            using (var context = new Context())
            {
                var battles = context.Battles.Include("User_1").Include("User_2").Where(b => b.User_1.Id == User.Id || b.User_2.Id == User.Id).ToList();

                List<BattleForInfo> battleForInfos = new List<BattleForInfo>();

                foreach (var battle in battles)
                {
                    User opponent;
                    string score;
                    if (battle.User_1.Id == User.Id)
                    {
                        opponent = battle.User_2;
                        score = battle.Score_User1.ToString() + ":" + battle.Score_User2.ToString();
                    }
                    else
                    {
                        opponent = battle.User_1;
                        score = battle.Score_User2.ToString() + ":" + battle.Score_User1.ToString();
                    }

                    var batfori = new BattleForInfo
                    {
                        Opponent = opponent,
                        Score = score
                    };
                    battleForInfos.Add(batfori);
                }
                return battleForInfos;
            }
        }

        
  
    }
}
