using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity;
using CrossyWords.Core.Users;
using CrossyWords.Core.API.OxfordDictionary;

namespace CrossyWords.Core
{
    public class Context : DbContext
    {
        public DbSet<User> Users { get; set; }
        public DbSet<Battle> Battles { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<Review> Reviews { get; set; }

        public DbSet<Level> Levels { get; set; }
        public DbSet<WordItem> Words { get; set; }
        public DbSet<AlphabetItem> Alphabet { get; set; }

        // To specify an explicit connection or DB name call the base class constructor
        public Context()
            // To specify an explicit connection or DB name call the base class constructor
            : base("DefaultConnection")
        { }

        static Context()
        {
            Database.SetInitializer(new Initializer());
        }

        public class Initializer : CreateDatabaseIfNotExists<Context>
        {
            protected override void Seed(Context context)
            {
                RequestManager request = new RequestManager();
                Repository repo = Factory.Default.GetRepository();

                foreach (var item in request.GetCategories())
                    context.Categories.Add(item);

                context.SaveChanges();

                context.Levels.Add(new Level { Id = 1, Name = "Beginner - 1000 words" });
                context.Levels.Add(new Level { Id = 2, Name = "Intermediate - 5000 words" });

                context.SaveChanges();

                foreach (var item in repo.ReadFromJson("Words1000"))
                {
                    context.Words.Add(new WordItem { Word = item, Level = context.Levels.First(l => l.Name == "Beginner - 1000 words") });
                }

                context.SaveChanges();

                foreach (var item in repo.ReadFromJson("Words5000"))
                {
                    context.Words.Add(new WordItem { Word = item, Level = context.Levels.First(l => l.Name == "Intermediate - 5000 words") });
                }

                context.SaveChanges();

                //foreach (var item in repo.GetWords("Words12000"))
                //{
                //    context.Words.Add(new WordItem { Word = item, Level = context.Levels.First(l => l.Name == "Advanced - 12000 words") });
                //}

                context.SaveChanges();

                foreach (var item in repo.GetAlphabet())
                    context.Alphabet.Add(item);

                context.SaveChanges();

            }
        }
    }
}
