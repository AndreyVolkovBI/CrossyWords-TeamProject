namespace CrossyWords.Core.Migrations
{
    using CrossyWords.Core.API.OxfordDictionary;
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;

    internal sealed class Configuration : DbMigrationsConfiguration<CrossyWords.Core.Context>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
            ContextKey = "CrossyWords.Core.Migrations.Configuration"; //change it!!!!!
        }

        protected override void Seed(CrossyWords.Core.Context context)
        {
            //  This method will be called after migrating to the latest version.

            //  You can use the DbSet<T>.AddOrUpdate() helper extension method 
            //  to avoid creating duplicate seed data.
        }
    }
}
