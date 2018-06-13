using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CrossyWords.Core.Users
{
    public class BattleForInfo
    {
        public User Opponent { get; set; }
        public string Score { get; set; }
        public string Status { get; set; }
        public string Points { get; set; }
    }
}
