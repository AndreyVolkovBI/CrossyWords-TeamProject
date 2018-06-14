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

        
        public void UpdateUser()
        {
            using (var context = new Context())
            {
                User = context.Users.First(u => u.Id == User.Id);
            }
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
                var user = context.Users.FirstOrDefault(u => u.Id == 12);

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
                context.SaveChanges();
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
                if (context.Users.Where(u => u.Id != User.Id && context.Battles.FirstOrDefault(b => (b.User_1.Id == User.Id && b.User_2.Id == u.Id || b.User_1.Id == u.Id && b.User_2.Id == User.Id) && (b.Points_User1 == null || b.Points_User2 == null)) == null).Count() == 0)
                    return false;
                else
                    return true;
            }
        }


        public bool CheckLimitationsInGames()
        {
            using (var context = new Context())
            {
                if (context.Battles.Where(b => (b.User_1.Id == User.Id || b.User_2.Id == User.Id) && (b.Points_User1 == null || b.Points_User2 == null)).Count() > 4)
                    return false;
                else
                    return true;
            }
        }


        public Battle MakeRandomBattle()
        {
            User opponent;
            using (var context = new Context())
            {
                Random r = new Random();                
                int randomMax = r.Next(0, context.Users.Where(u => u.Id != User.Id && context.Battles.FirstOrDefault(b => (b.User_1.Id == User.Id && b.User_2.Id == u.Id || b.User_1.Id == u.Id && b.User_2.Id == User.Id) && (b.Points_User1 == null || b.Points_User2 == null)) == null).Count());
                opponent = context.Users.Where(u => u.Id != User.Id && context.Battles.FirstOrDefault(b => (b.User_1.Id == User.Id && b.User_2.Id == u.Id || b.User_1.Id == u.Id && b.User_2.Id == User.Id) && (b.Points_User1 == null || b.Points_User2 == null)) == null).ToList()[r.Next(0, randomMax)];
            }
            return MakeBattle(opponent);
        }

        public Battle MakeBattle(User opponent)
        {  
            using (var context = new Context())
            {
                
                context.Battles.Add(new Battle { User_1 = context.Users.First(u => u.Id == User.Id), User_2 = context.Users.First(u => u.Id == opponent.Id), DateOfChallenge = DateTime.Now });
                context.SaveChanges();
                Battle createdbattle = context.Battles.Include("User_1").Include("User_2").First(b => b.User_1.Id == User.Id && b.User_2.Id == opponent.Id || b.User_1.Id == opponent.Id && b.User_2.Id == User.Id);
                return createdbattle;
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
                    battleForInfos.Add(new BattleForInfo(User, battle));
                }
                return battleForInfos;
            }
        }

        public Battle FindCertainBattle(BattleForInfo battleForInfo) 
        {
            using (var context = new Context())
            {
                var battle = context.Battles.First(b => b.Id == battleForInfo.Id);
                return battle;
            }
        }

        public void SaveAllInformationAboutBattle(Battle battle, int points, string allwords = null) //know user1, user2, dateofchallenge
        {
            using (var context = new Context())
            {

                if (context.Battles.Include("User_1").First(b => b.Id == battle.Id).User_1.Id == User.Id)
                    context.Battles.First(b => b.Id == battle.Id).Points_User1 = points;
                else
                    context.Battles.First(b => b.Id == battle.Id).Points_User2 = points;

                if (allwords != null)
                    context.Battles.First(b => b.Id == battle.Id).AllWords = allwords;
                else
                {
                    User user1 = context.Battles.Include("User_1").First(b => b.Id == battle.Id).User_1;
                    User user2 = context.Battles.Include("User_2").First(b => b.Id == battle.Id).User_2;
                    if (context.Battles.First(b => b.Id == battle.Id).Points_User1 < context.Battles.First(b => b.Id == battle.Id).Points_User2)
                    {                       
                        AddWIn(user2);
                        AddLose(user1);
                    }
                    else if(context.Battles.First(b => b.Id == battle.Id).Points_User1 > context.Battles.First(b => b.Id == battle.Id).Points_User2)
                    {
                        AddWIn(user1);
                        AddLose(user2);                       
                    }
                    else
                        AddDraws(user1, user2);
                    UpdateUser();
                }
                context.SaveChanges();
                
            }
        }


        private void AddWIn(User user)
        {
            using (var context = new Context())
            {
                context.Users.First(u => u.Id == user.Id).Win = user.Win + 1;
                context.SaveChanges();
            }
        }

        private void AddDraws(User user1, User user2)
        {
            AddDraw(user1);
            AddDraw(user2);
        }

        private void AddDraw(User user)
        {
            using (var context = new Context())
            {
                context.Users.First(u => u.Id == user.Id).Draw = user.Draw + 1;
                context.SaveChanges();
            }
        }

        private void AddLose(User user)
        {
            using (var context = new Context())
            {
                context.Users.First(u => u.Id == user.Id).Lose = user.Lose + 1;
                context.SaveChanges();
            }
        }
        
        public List<RatingUser> GetRatingOfUsers()
        {
            using (var context = new Context())
            {
                List<RatingUser> topUsers = new List<RatingUser>();
                int count = context.Users.Count();
               
                for (int i = 0; i < 21; i++)
                {
                    var user = context.Users.OrderByDescending(r => r.Win * 3 + r.Draw - 2 * r.Lose).ToList()[i];
                    topUsers.Add(new RatingUser { Name = user.Name, Number = i + 1, Rating = user.Win * 3 + user.Draw - 2 * user.Lose });
                    if (count <= i + 1)
                        break;
                }

                if (topUsers.FirstOrDefault(u => u.Name == User.Name) == null)
                      topUsers.Add(new RatingUser {Name = User.Name, Rating = 3 * User.Win + User.Draw - 2 * User.Lose, Number = context.Users.OrderByDescending(r => r.Win * 3 + r.Draw - 2 * r.Lose).ToList().FindIndex(u => u.Id == User.Id)});

                return topUsers;

            }
        }

        
  
    }
}
