﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CrossyWords.Core.Users
{
    public class Battle
    {

        public int Id { get; set; }

        public DateTime DateOfChallenge { get; set; }
        public User User_1 { get; set; }
        public User User_2 { get; set; }
        public string AllWords { get; set; }
        public int? Points_User1 { get; set; }
        public int? Points_User2 { get; set; }
    }
}
