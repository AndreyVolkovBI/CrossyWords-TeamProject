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
                    cell.Value = _db.Alphabet[r.Next(25)].Letter;
        }

        public void NewAttempt()
        {
            List<string> listFinal = new List<string>();

            Random r = new Random();
            string word = _db.Words[r.Next(0, _db.Words.Count - 1)].Word;


        }

        public bool TryFillIn(string word)
        {
            Cell currentCell = new Cell();
            foreach (var item in Cells) // если есть первая буква, то начни строить от неё
            {
                if (item.Value == word[0].ToString())
                    currentCell = item;
            }



            return false;
        }

        public List<Cell> FillAllCells()
        {
            Random r = new Random();
            string word = _db.Words[r.Next(0, _db.Words.Count - 1)].Word;

            Cells = new List<Cell>();
            FullIdCells();

            for (int l = 0; l < 5; l++)
            {
                string currentWord = _db.Words[l].Word;
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

        public static List<string> ReadFromJson(string name)
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

        public List<WordItem> GetWords()
        {
            List<WordItem> list = new List<WordItem>();

            foreach (var item in ReadFromJson("Words12000"))
            {
                var word = new WordItem() { Word = item };
                list.Add(word);
            }
            return list;
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
