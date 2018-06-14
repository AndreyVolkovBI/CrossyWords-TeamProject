using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CrossyWords.Core.Users
{
    public class BattleForInfo
    {
        public User Opponent { get; set; } //
        public string Score { get; set; }
        public string Status { get; set; } //
        public string Points { get; set; }

        const string yourRound = "your round";
        const string waiting = "waiting opponent";
        const string finished = "finished";

        const string win = "1:0";
        const string draw = "1:1";
        const string lose = "0:1";
        const string unknow = "0:0";


        public bool AllowPlayBattle()
        {
            if (Status == yourRound)
                return true;
            else
                return false;
        }


        public BattleForInfo(User user, Battle battle)
        {
            if (battle.User_1.Id == user.Id) //you are user1
            {
                Opponent = battle.User_2;

                if (battle.Points_User1 == null) //Points_User1 and Points_User2 can not be null at the same time
                    Status = yourRound;
                else if (battle.Points_User2 == null)
                    Status = waiting;
                else Status = finished;

                if (Status == finished)
                {
                    if (battle.Points_User1 > battle.Points_User2)
                    {
                        Score = win;
                    }
                    else if (battle.Points_User1 < battle.Points_User2)
                    {
                        Score = lose;
                    }
                    else
                    {
                        Score = draw;
                    }
                    Points = battle.Points_User1.ToString() + ":" + battle.Points_User2.ToString();
                }
                else if(Status == yourRound)
                {
                    Score = unknow;
                    Points = "?:?";
                }
                else //status == waiting
                {
                    Score = unknow;
                    Points = battle.Points_User1.ToString() + ":?";
                }

            }
            else //you are user2
            {
                Opponent = battle.User_1;

                if (battle.Points_User1 == null)
                    Status = waiting;
                else if (battle.Points_User2 == null)
                    Status = yourRound;
                else Status = finished;

                if (Status == finished)
                {
                    if (battle.Points_User1 > battle.Points_User2)
                    {
                        Score = lose;
                    }
                    else if (battle.Points_User1 < battle.Points_User2)
                    {
                        Score = win;
                    }
                    else
                    {
                        Score = draw;
                    }
                    Points = battle.Points_User2.ToString() + ":" + battle.Points_User1.ToString();
                }
                else if (Status == yourRound)
                {
                    Score = unknow;
                    Points = "?:?";
                }
                else
                {
                    Score = unknow;
                    Points = battle.Points_User2.ToString() + ":?";
                }

            }



        }
        

    }
}
