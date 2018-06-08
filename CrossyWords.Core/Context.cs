﻿using System;
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
        public DbSet<WordsResult> Words { get; set; }

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
                var r = new RequestManager();

                foreach(var item in r.GetWords())
                    context.Words.Add(item);

                context.SaveChanges();

            }
        }

        public static void DeleteSpaces(List<WordsResult> list) // delete spaces in words
        {
            for (int i = 0; i < list.Count - 1; i++)
            {
                foreach (var el in list[i].Word)
                {
                    if (el == ' ' || el == ',')
                    {
                        list.Remove(list[i]);
                        DeleteSpaces(list);
                    }
                }
            }

        }
    }
}
