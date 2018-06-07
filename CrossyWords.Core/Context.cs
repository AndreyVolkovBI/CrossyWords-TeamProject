using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity;
using CrossyWords.Core.Users;

namespace CrossyWords.Core
{
    public class Context : DbContext
    {

        public DbSet<User> Users { get; set; }

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


                //context.SaveChanges();

            }
        }
    }
}
