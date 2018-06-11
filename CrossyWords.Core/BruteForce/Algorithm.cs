using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CrossyWords.Core.BruteForce
{
    public class Algorithm
    {
        WordsHelper helper = new WordsHelper();

        public List<string> Words { get; set; } = new List<string>();

        Dictionary<int, Dictionary<int, List<List<string>>>> lists = new Dictionary<int, Dictionary<int, List<List<string>>>>();
        // External Dictionary
        // Key - порядковый номер буквы с которой ведётся расчёт
        // Value - словарь с такими параметрами:
        // Internal Dictionary
        // Key - порядковый номер буквы по которую ведётся расчёт
        // Value - список списка максимаьлных совпадений для комбинации букв

        public Dictionary<int, Dictionary<int, List<List<string>>>> Get()
        {
            return lists;
        }


        public Dictionary<int, Dictionary<int, List<List<string>>>> GetDictionary()
        {
            helper.GetMinMax(out int maxLength, out int minLength);

            for (int w = 1; w <= maxLength; w++)
            {
                lists.Add(w, new Dictionary<int, List<List<string>>>());

                for (int i = 1; i <= maxLength; i++)
                {
                    if (w <= i)
                    {
                        lists[w].Add(i, new List<List<string>>());

                        for (int j = 1; j < minLength; j++)
                        {
                            if (w <= j)
                                lists[w][i].Add(helper.FillListsFinal(external: w, index: i, word: helper.FillLists(indexFirst: w, indexSecond: j)));
                        }
                    }
                }
            }

            return lists;
        }
    }
}
