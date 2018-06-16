using CrossyWords.Core.API.OxfordDictionary;
using CrossyWords.Core.BruteForce;
using CrossyWords.Core.Users;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace CrossyWords.Core
{
    public class Cell
    {
        public int Id { get; set; }
        public string Value { get; set; }
        public Button Button { get; set; } = new Button();
    }

    public class Repository
    {
        DatabaseRepository _db = new DatabaseRepository();

        public bool GameOn { get; set; } = false;
        public int Dimension { get; set; } = 5;

        public List<WordItem> Words { get; set; } = new List<WordItem>();
        public List<AlphabetItem> Alphabet { get; set; } = new List<AlphabetItem>();

        public List<Cell> Cells { get; set; } = new List<Cell>(); // список всех ячеек

        public Repository()
        {
            FullIdCells();
        }

        public void FillBlankCells()
        {
            Random r = new Random();
            foreach (var cell in Cells)
                if (cell.Value == null)
                    cell.Value = _db.GetAlphabet()[r.Next(25)].Letter;
        }

        public List<Cell> FillAllCells(List<WordItem> list)
        {
            Random r = new Random();
            Cells = new List<Cell>();
            FullIdCells();

            List<WordItem> cellWords = new List<WordItem>();

            foreach (var word in list)
            {
                WordItem witem = list[r.Next(0, list.Count - 1)];
                if (!cellWords.Contains(witem))
                    cellWords.Add(witem);
            }

            foreach (var item in cellWords)
                if (!FillWord(item.Word))
                    break;

            FillBlankCells();
            return Cells;

        }

        public bool FillWord(string word)
        {
            Random r = new Random();
            Stack<List<Cell>> stackListCells = new Stack<List<Cell>>();
            stackListCells.Push(Cells);

            bool flag = false;

            if (GetVacantCells().Count > 1)
            {
                Cell currentCell = GetVacantCells()[r.Next(0, GetVacantCells().Count - 1)];
                currentCell.Value = word[0].ToString();

                for (int i = 1; i < word.Length; i++)
                {
                    foreach (var item in GetIds(currentCell.Id))
                    {
                        if (Cells.FirstOrDefault(x => x.Id == item).Value == null && FillLetter(word[i].ToString(), Cells.FirstOrDefault(x => x.Id == item)))
                        {
                            currentCell = Cells.FirstOrDefault(x => x.Id == item);
                            flag = true;
                            break;
                        }
                    }

                    if (!flag)
                    {
                        Cells = stackListCells.Pop();
                        return false;
                    }

                    flag = false;
                }
            }
            return true;
        }

        public bool FillLetter(string letter, Cell currentCell)
        {
            currentCell.Value = letter.ToString();

            foreach (var item in GetIds(currentCell.Id))
                if (Cells.FirstOrDefault(x => x.Id == item).Value == null)
                    return true;
            
            return false;
        }

        public List<Cell> GetVacantCells()
        {
            List<Cell> VacantCells = new List<Cell>();

            foreach (var item in Cells)
                if (item.Value == null)
                    VacantCells.Add(item);
            return VacantCells;
        }

        public List<int> GetIds(int id)
        {
            NeighboursHelper nh = new NeighboursHelper();

            return nh.GetIds(id);
        }

        public void FullIdCells()
        {
            for (int i = 1; i <= 25; i++)
                Cells.Add(new Cell { Id = i });
        }

        public List<string> ReadFromJson(string name)
        {
            try
            {
                using (var reader = new StreamReader($"../../../Words/{name}.json"))
                {
                    return JsonConvert.DeserializeObject<List<string>>(reader.ReadToEnd());
                }
            }
            catch
            {
                throw new Exception("Error reading data");
            }
        }

        public List<AlphabetItem> GetAlphabet()
        {
            List<AlphabetItem> list = new List<AlphabetItem>();

            foreach (var item in ReadFromJson("Alphabet"))
            {
                var letter = new AlphabetItem() { Letter = item };
                list.Add(letter);
            }
            return list;
        }
    }
}
