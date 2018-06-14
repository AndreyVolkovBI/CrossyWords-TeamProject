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

                foreach (var item in repo.GetWords())
                    context.Words.Add(item);

                foreach (var item in repo.GetAlphabet())
                    context.Alphabet.Add(item);

                context.SaveChanges();

            }
        }
    }
}
