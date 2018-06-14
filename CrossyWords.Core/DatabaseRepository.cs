using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CrossyWords.Core
{
    public class DatabaseRepository
    {
        public List<WordItem> Words { get; set; }
        public List<AlphabetItem> Alphabet { get; set; }

        public void FillWordsAndAlphabet()
        {
            using (var context = new Context())
            {
                foreach (var item in context.Words)
                    Words.Add(item);

                foreach (var item in context.Alphabet)
                    Alphabet.Add(item);
            }
        }

        public List<WordItem> GetWords()
        {
            return Words;
        }

        public List<AlphabetItem> GetAlphabet()
        {
            return Alphabet;
        }

        public bool IsWordInList(string word)
        {
            foreach (var item in Words)
                if (item.Word == word)
                    return true;
            return false;
        }

    }
}
