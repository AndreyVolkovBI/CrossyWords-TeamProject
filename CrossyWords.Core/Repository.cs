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

    public class Repository : IRepository
    {
        public bool GameOn { get; set; } = false;

        public int Dimension { get; set; } = 5;

        public List<WordItem> Words { get; set; } = new List<WordItem>();

        public List<Cell> Cells { get; set; } = new List<Cell>(); // список всех ячеек

        public Repository()
        {
            //FilterWords();

            //BasicWords = ReadJson("Words");
            FullIdCells();
        }

        //public void FillGaps()
        //{
        //    Random r = new Random();
        //    string word = Words[r.Next(0, Words.Count - 1)].Word;
        //    Word_DB currentWord = new Word_DB { Value = word, Count = word.Length };
        //}

        public void FillBlankCells()
        {
            Random r = new Random();
            foreach (var cell in Cells)
                if (cell.Value == null)
                    cell.Value = ReadJson("Aplphabet")[r.Next(25)];
        }

        public List<Cell> FillAllCells()
        {
            Random r = new Random();
            string word = Words[r.Next(0, Words.Count - 1)].Word;

            for (int l = 0; l < 5; l++)
            {
                string currentWord = Words[l].Word;
                Cell currentCell = new Cell();

                foreach (var item in Cells) // если есть первая буква, то начни строить от неё
                {
                    if (item.Value == currentWord[0].ToString())
                        currentCell = item;
                }

                do // проверка на занятость ячейки
                {
                    currentCell = Cells[r.Next(25)];

                } while (currentCell.Value != null);


                for (int i = 0; i < currentWord.Length; i++)
                {
                    currentCell.Value = currentWord[i].ToString();
                    List<int> idOfCurrentCell = GetIds(currentCell.Id);

                    Stack<List<Cell>> st = new Stack<List<Cell>>();


                    for (int k = 0; k < idOfCurrentCell.Count; k++)
                    {
                        if (Cells[idOfCurrentCell[k] - 1].Value == null)
                        {
                            currentCell = Cells[idOfCurrentCell[k] - 1];
                            break;
                        }
                    }
                }
            }

            FillBlankCells();
            return Cells;
        }

        public bool IsWordInList(string word)
        {
            foreach (var item in Words)
                if (item.Word == word)
                    return true;
            return false;
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

        public List<string> ReadJson(string name)
        {
            try
            {
                using (var reader = new StreamReader($"../../../{name}.json"))
                {
                    return JsonConvert.DeserializeObject<List<string>>(reader.ReadToEnd());
                }
            }
            catch
            {
                throw new Exception("Error reading data");
            }
        }

        public List<WordItem> GetWords()
        {
            List<WordItem> list = new List<WordItem>();

            foreach (var item in ReadJson("Words"))
            {
                var word = new WordItem() { Word = item };
                list.Add(word);
            }
            return list;
        }

        public List<AlphabetItem> GetAlphabet()
        {
            List<AlphabetItem> list = new List<AlphabetItem>();

            foreach (var item in ReadJson("Alphabet"))
            {
                var letter = new AlphabetItem() { Letter = item };
                list.Add(letter);
            }
            return list;
        }
    }
}
