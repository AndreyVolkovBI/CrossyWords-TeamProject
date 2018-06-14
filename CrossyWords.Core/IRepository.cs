using CrossyWords.Core.Users;
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
        List<WordItem> Words { get; set; }
        List<Cell> Cells { get; set; }

        bool GameOn { get; set; }

        List<int> GetIds(int id);
        List<Cell> FillAllCells();
        List<string> ReadJson(string name);
        bool IsWordInList(string word);

        List<WordItem> GetWords();
        List<AlphabetItem> GetAlphabet();
    }
}
