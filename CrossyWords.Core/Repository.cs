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

    public class Word_DB
    {
        public string Value { get; set; }
        public int Count { get; set; }
    }

    public class Repository : IRepository
    {
        public int Dimension { get; set; } = 5;

        public List<string> Words { get; set; } = new List<string>(); // список всех слов

        public List<Cell> Cells { get; set; } = new List<Cell>(); // список всех ячеек

        public Repository()
        {
            ReadWords();
            FullIdCells();
            FillGaps();
        }

        public void FillGaps()
        {
            Random r = new Random();
            string word = Words[r.Next(0, 5)];
            Word_DB currentWord = new Word_DB { Value = word, Count = word.Length };
        }

        public List<Cell> FillAllCells()
        {
            Random r = new Random();
            string word = Words[r.Next(0, 5)];

            for (int l = 0; l < Words.Count; l++)
            {
                string currentWord = Words[l];
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
            return Cells;
        }

        public bool IsWordInList(string word)
        {
            foreach (var item in Words)
                if (item == word)
                    return true;
            return false;
        }

        public List<int> GetIdsNew(int id) // новая функция для id соседей для матриц разной размерности
        {
            List<int> list = new List<int>();

            if (Dimension == 2)
            {
                for (int i = id + 1; i <= 4; i++)
                    list.Add(i);
                for (int i = 1; i >= id - 1; i++)
                    list.Add(i);
            }
            else if (Dimension == 3)
            {

            }
            else if (Dimension == 4)
            {

            }
            else if (Dimension == 5)
            {

            }
            else if (Dimension == 6)
            {

            }

            return new List<int>();
        }


        public List<int> GetIds(int id) // возвращает список id ячеек
        {
            List<int> ids = new List<int>();

            if (NeighbourCount(id) == 9)
            {
                for (int i = 6; i > 3; i--) // сверху
                    ids.Add(id - i);
                ids.Add(id + 1); // спарва
                for (int j = 6; j > 3; j--) // снизу
                    ids.Add(id + j);
                ids.Add(id - 1); // слева

            }
            else if (NeighbourCount(id) == 5)
            {
                if (id > 1 && id < 5)
                {
                    ids.Add(id + 1);
                    for (int i = 6; i > 3; i--)
                        ids.Add(id + i);
                    ids.Add(id - 1);
                }
                else if (id > 21 && id < 25)
                {
                    ids.Add(id - 1);
                    for (int i = 6; i > 3; i--)
                        ids.Add(id - i);
                    ids.Add(id + 1);
                }
                else if (id == 10 || id == 15 || id == 20)
                {
                    ids.Add(id - 5);
                    for (int i = -6; i < 5; i += 5)
                        ids.Add(id + i);
                    ids.Add(id + 5);
                }
                else if (id == 6 || id == 11 || id == 16)
                {
                    ids.Add(id - 5);
                    for (int i = -4; i < 11; i += 5)
                        ids.Add(id + i);
                    ids.Add(id + 5);
                }

            }
            else if (NeighbourCount(id) == 3)
            {
                if (id == 1)
                {
                    ids.Add(2); ids.Add(7); ids.Add(6);
                }
                else if (id == 5)
                {
                    ids.Add(4); ids.Add(9); ids.Add(10);
                }
                else if (id == 21)
                {
                    ids.Add(16); ids.Add(17); ids.Add(22);
                }
                else if (id == 25)
                {
                    ids.Add(20); ids.Add(19); ids.Add(24);
                }
            }
            return ids;
        }

        public int NeighbourCount(int id) // количество соседей у ячейки
        {
            if (IsCell3(id))
                return 3;
            else if (IsCell5(id))
                return 5;
            else
                return 9;
        }

        public bool IsCell3(int id)
        {
            if (id == 1 || id == 5 || id == 21 || id == 25)
                return true;
            return false;
        }

        public bool IsCell5(int id)
        {
            // (10, 15, 20) || (6, 11, 16) || (2, 3, 4) || (22, 23, 24)
            if ((id == 10 || id == 15 || id == 20) ||
               (id == 6 || id == 11 || id == 16) ||
               (id > 1 && id < 5) ||
               (id > 21 && id < 25))
                return true;
            return false;
        }

        public void FullIdCells()
        {
            for (int i = 1; i <= 25; i++)
                Cells.Add(new Cell { Id = i });
        }

        public List<string> ReadWords()
        {
            using (var sr = new StreamReader("../../../basic.txt", encoding: Encoding.GetEncoding(1251)))
            {
                string line = sr.ReadLine();
                do
                {
                    Words.Add(line);
                    line = sr.ReadLine();

                } while (!string.IsNullOrWhiteSpace(line));
            }

            return Words;
        }
    }
}
