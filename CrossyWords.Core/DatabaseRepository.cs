using CrossyWords.Core.API.OxfordDictionary;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CrossyWords.Core
{
    public class DatabaseRepository
    {
        RequestManager rm = new RequestManager();

        public List<WordItem> Words { get; set; } = new List<WordItem>(); // the words are currently in use

        public List<Level> Levels { get; set; } = new List<Level>();
        public List<Category> Categories { get; set; } = new List<Category>();

        public Level SelectedLevel { get; set; } = new Level { Id = 1, Name = "Beginner - 1000 words" }; // chosen level or category
        public Category SelectedCategory { get; set; }

        public List<WordItem> GetListChosenWords()
        {
            if (SelectedCategory != null)
            {
                Words = new List<WordItem>();

                foreach (var item in rm.GetWords(SelectedCategory.Name))
                    Words.Add(new WordItem() { Word = item.Word });
            }
            else
            {
                Words = new List<WordItem>();

                foreach(var item in GetWords())
                    if (item.Level != null && item.Level.Id == SelectedLevel.Id)
                        Words.Add(new WordItem() { Word = item.Word });
            }
            return Words;
        }

        public void FillCategories()
        {
            using (var context = new Context())
            {
                foreach (var item in context.Categories)
                    Categories.Add(item);
            }
        }

        public void FillLevels()
        {
            Levels.Add(new Level { Id = 1, Name = "Beginner - 1000 words" });
            Levels.Add(new Level { Id = 2, Name = "Intermediate - 5000 words" });
        }

        public List<WordItem> GetWords()
        {
            using (var context = new Context())
            {
                return context.Words.Include("Level").ToList();
            }
        }

        public List<AlphabetItem> GetAlphabet()
        {
            using (var context = new Context())
            {
                return context.Alphabet.ToList();
            }
        }

        public bool IsWordInList(string word)
        {
            using (var context = new Context())
            {
                foreach (var item in context.Words)
                    if (item.Word == word)
                        return true;
                return false;
            }
        }

    }
}
