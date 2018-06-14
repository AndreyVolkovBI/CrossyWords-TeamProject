using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CrossyWords.Core
{
    public class Factory
    {
        static Factory _factory;

        public static Factory Default
        {
            get
            {
                if (_factory == null)
                    _factory = new Factory();
                return _factory;
            }
        }

        UsersData _usersdata = new UsersData(); //repo ?

        Repository _Repository = new Repository();
        DatabaseRepository _dbRepository = new DatabaseRepository();

        public UsersData GetUsersData()
        {
            return _usersdata;
        }

        public Repository GetRepository()
        {
            return _Repository;
        }

        public DatabaseRepository GetDatabaseRepository()
        {
            return _dbRepository;
        }
    }
}
