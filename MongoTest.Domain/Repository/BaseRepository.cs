using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MongoTest.Domain.Repository
{
    internal abstract class BaseRepository
    {
        protected readonly IMongoDatabase Database;
        internal BaseRepository(IMongoDatabase database)
        {
            Database = database;
        }
    }
}
