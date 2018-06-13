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


        public Battle MakeRandomBattle()
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
                    User opponent;
                    string score;
                    string points;
                    string status = null;

                    if (battle.IsPlayedUser1 && battle.IsPlayedUser2)
                    {
                        status = "finished";
                    }

                    if (battle.User_1.Id == User.Id)
                    {
                        opponent = battle.User_2;
                        score = battle.Score_User1.ToString() + ":" + battle.Score_User2.ToString();
                        points = battle.Points_User1.ToString() + ":" + battle.Points_User2.ToString();
                        if (string.IsNullOrWhiteSpace(status) && battle.IsPlayedUser1)
                            status = "waiting opponent";
                        else if (string.IsNullOrWhiteSpace(status))
                            status = "your raund";
                            
                    }
                    else
                    {
                        opponent = battle.User_1;
                        score = battle.Score_User2.ToString() + ":" + battle.Score_User1.ToString();
                        points = battle.Points_User2.ToString() + ":" + battle.Points_User1.ToString();
                        if (string.IsNullOrWhiteSpace(status) && battle.IsPlayedUser2)
                            status = "waiting opponent";
                        else if(string.IsNullOrWhiteSpace(status))
                            status = "your round";
                    }


                    var batfori = new BattleForInfo
                    {
                        Opponent = opponent,
                        Score = score,
                        Points = points,
                        Status = status
                    };
                    battleForInfos.Add(batfori);
                }
                return battleForInfos;
            }
        }

        public Battle FindCertainBattle(BattleForInfo battleForInfo) 
        {
            using (var context = new Context())
            {
                var battle = context.Battles.First(b => b.User_1.Id == User.Id && b.User_2.Id == battleForInfo.Opponent.Id || b.User_1.Id == battleForInfo.Opponent.Id && b.User_2.Id == User.Id);
                return battle;
            }
        }

        public void SaveAllInformationAboutBattle(Battle battle, int points, string allwords = null) //know user1, user2, dateofchallenge
        {
            using (var context = new Context())
            {

                if (context.Battles.Include("User_1").First(b => b.Id == battle.Id).User_1.Id == User.Id)
                {
                    context.Battles.First(b => b.Id == battle.Id).Points_User1 = points;
                    context.Battles.First(b => b.Id == battle.Id).IsPlayedUser1 = true;
                }
                else
                {
                    context.Battles.First(b => b.Id == battle.Id).Points_User2 = points;
                    context.Battles.First(b => b.Id == battle.Id).IsPlayedUser2 = true;
                }

                if (allwords != null)
                {
                    context.Battles.First(b => b.Id == battle.Id).AllWords = allwords;
                }
                else
                {

                    User user1 = context.Battles.Include("User_1").First(b => b.Id == battle.Id).User_1;
                    User user2 = context.Battles.Include("User_2").First(b => b.Id == battle.Id).User_2;
                    if (context.Battles.First(b => b.Id == battle.Id).Points_User1 < context.Battles.First(b => b.Id == battle.Id).Points_User2)
                    {
                        context.Battles.First(b => b.Id == battle.Id).Score_User2 = 1;
                        context.Users.First(u => u.Id == user1.Id).Lose = user1.Lose + 1;
                        context.Users.First(u => u.Id == user2.Id).Win = user2.Win + 1;
                        
                    }
                    else if(context.Battles.First(b => b.Id == battle.Id).Points_User1 > context.Battles.First(b => b.Id == battle.Id).Points_User2)
                    {
                        context.Battles.First(b => b.Id == battle.Id).Score_User1 = 1;
                        context.Users.First(u => u.Id == user1.Id).Win = user1.Win + 1;
                        context.Users.First(u => u.Id == user2.Id).Lose = user2.Lose + 1;
                       
                    }
                    else
                    {
                        context.Battles.First(b => b.Id == battle.Id).Score_User2 = 1;
                        context.Battles.First(b => b.Id == battle.Id).Score_User1 = 1;

                        context.Users.First(u => u.Id == user1.Id).Draw = user1.Draw + 1;
                        context.Users.First(u => u.Id == user2.Id).Draw = user2.Draw + 1;
                        
                    }


                    context.SaveChanges();
                    UpdateRating(user1);
                    UpdateRating(user2);
                }
                context.SaveChanges();
                
            }
        }

        private void UpdateRating(User user)
        {
            using (var context = new Context())
            {
                User _user = context.Users.First(u => u.Id == user.Id);
                context.Users.First(u => _user.Id == u.Id).Rating = 3 * _user.Win + _user.Draw - 2 * _user.Lose;
                context.SaveChanges();
            }
            UpdateUser();
        }

        private void UpdateUser()
        {
            using (var context = new Context())
            {
                User = context.Users.First(u => u.Id == User.Id);
            }
        }


        public List<RatingUser> GetRatingOfUsers()
        {
            using (var context = new Context())
            {
                List<RatingUser> topUsers = new List<RatingUser>();
                int count = context.Users.Count();

                for (int i = 0; i < 20; i++)
                {
                    var user = context.Users.OrderByDescending(u => u.Rating).ToList()[i];
                    topUsers.Add(new RatingUser { Name = user.Name, Number = i + 1, Rating = user.Rating });
                    if (count <= i + 1)
                        break;
                }
                return topUsers;

            }
        }

        
  
    }
}
