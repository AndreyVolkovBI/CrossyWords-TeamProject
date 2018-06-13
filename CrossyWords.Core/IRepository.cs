﻿using CrossyWords.Core.Users;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CrossyWords.Core
{
    public interface IRepository
    {

        int Dimension { get; set; }
        List<string> Words { get; set; }
        List<BasicWord> BasicWords { get; set; }
        List<Cell> Cells { get; set; }

        bool GameOn { get; set; }

        List<int> GetIds(int id);
        List<Cell> FillAllCells();
        List<BasicWord> ReadWords();
        bool IsWordInList(string word);
    }
}
