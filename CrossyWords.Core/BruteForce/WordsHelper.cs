using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CrossyWords.Core.BruteForce
{
    public class WordsHelper
    {
        Dictionary<int, Dictionary<int, List<List<string>>>> lists = new Dictionary<int, Dictionary<int, List<List<string>>>>();

        public List<string> Words { get; set; } = new List<string>();

        Algorithm al = new Algorithm();

        public WordsHelper()
        {
            lists = al.Get();
        }

        public bool IsContains(int external, int i, string word)
        {
            foreach (var item in lists[external][i])
                if (item.FirstOrDefault(x => x == word) != null)
                    return true;
            return false;
        }

        public string FillLists(int indexFirst, int indexSecond)
        {
            int num = 0;
            string max = "";

            for (int i = 0; i < Words.Count; i++)
            {
                List<string> temp = new List<string>();
                string currentWord = Words[i];

                string letters = currentWord.Remove(indexSecond);
                letters = letters.Remove(0, indexFirst - 1);

                foreach (var item in Words) // добавляет в список temp все слова с такой же 1-ой буквой
                {
                    string test = item.Remove(indexSecond);
                    test = test.Remove(0, indexFirst - 1);
                    if (test == letters)
                        temp.Add(item);
                }

                if (temp.Count > num)
                {
                    num = temp.Count;
                    max = currentWord;
                }

            }
            return max;
        }

        public List<string> FillListsFinal(int external, int index, string word)
        {
            List<string> list = new List<string>();
            string letters;

            if (word.Length > index)
            {
                letters = word.Remove(index);

                foreach (var item in Words) // добавляет в список temp все слова с такой же 1-ой буквой
                    if (item.Length > index)
                    {
                        if (item.Remove(index) == letters && !IsContains(external, index, item))
                            list.Add(item);
                    }
            }
            return list;
        }

        public void GetMinMax(out int maxLength, out int minLength)
        {
            maxLength = 0;
            foreach (var item in Words)
                if (maxLength < item.Length)
                    maxLength = item.Length;

            minLength = maxLength;
            foreach (var item in Words)
                if (minLength > item.Length)
                    minLength = item.Length;
        }

        public List<string> ReadWords()
        {
            using (var sr = new StreamReader("../../../wordsTest.txt", encoding: Encoding.GetEncoding(1251)))
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
