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

        IRepository _Repository = new Repository();
        //IRepository _dbRepository = new DatabaseRepository();

        public IRepository GetRepository<T>()
        {
            if (typeof(T) == typeof(Repository))
                return _Repository;
            //if (typeof(T) == typeof(DatabaseRepository))
            //    return _dbRepository;
            return null;
        }

        public UsersData GetUsersData()
        {
            return _usersdata;
        }
    }
}
