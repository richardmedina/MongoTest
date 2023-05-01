using MongoDB.Bson;
using MongoDB.Driver;
using MongoTest.Common.Domain.Repository;
using MongoTest.Contract.Domain;
using MongoTest.Domain.Constants;
using MongoTest.Domain.Entities;

namespace MongoTest.Domain.Repository
{
    internal class UserRepository : BaseRepository, IUserRepository
    {
        private IMongoCollection<UserDocument> userCollection;
        public UserRepository(IMongoDatabase database) : base(database)
        {
            userCollection = database.GetCollection<UserDocument>(CollectionNames.User);
        }

        public async Task<IEnumerable<UserDomain>> GetAllAsync()
        {
            var documents = await userCollection.Find(new BsonDocument { })
                .ToListAsync();

            var result = documents.Select(doc => new UserDomain
            {
                Id = doc.Id,
                FirstName = doc.FirstName,
                LastName = doc.LastName,
                CreatedBy = doc.CreatedBy,
                CreatedDate = doc.CreatedDate
            });

            return result;
        }

        public async Task<UserDomain> GetAsync(string id)
        {
            await Task.CompletedTask;

            var filter = Builders<UserDocument>.Filter.Eq("id", id);
            var result = await userCollection
                .Find(filter)
                .FirstOrDefaultAsync();

            return new UserDomain
            {
                Id = result.Id,
                FirstName = result.FirstName,
                LastName = result.LastName,
                CreatedBy = result.CreatedBy,
                CreatedDate = result.CreatedDate
            };
        }

        public async Task<IEnumerable<UserDomain>> GetByFullNameAsync(string fullName)
        {
            var filter = Builders<UserDocument>.Filter.Regex(ud => ud.FirstName, new BsonRegularExpression(fullName, "i"));
            var documents = await userCollection.Find(filter)
                .ToListAsync();

            return documents.Select(doc => new UserDomain
            {
                Id = doc.Id,
                FirstName = doc.FirstName,
                LastName = doc.LastName,
                CreatedBy = doc.CreatedBy,
                CreatedDate = doc.CreatedDate
            });
        }

        public async Task<UserDomain> CreateAsync(UserDomain user)
        {
            var doc = new UserDocument
            {
                Id = user.Id,
                FirstName = user.FirstName,
                LastName = user.LastName,
                CreatedDate = DateTime.UtcNow,
                CreatedBy = user.CreatedBy,
            };

            await userCollection.InsertOneAsync(doc);

            user.Id = doc.Id;

            return user;
        }

        public async Task UpdateAsync(string id, UserDomain user)
        {
            var filter = Builders<UserDocument>.Filter.Eq("Id", id);
            var userDocument = new UserDocument
            {
                Id = user.Id,
                FirstName = user.FirstName,
                LastName = user.LastName,
                CreatedDate = DateTime.UtcNow,
                CreatedBy = user.CreatedBy
            };

            await userCollection.ReplaceOneAsync(filter, userDocument);
        }

        public async Task DeleteAsync(string id)
        {
            var filter = Builders<UserDocument>.Filter.Eq("Id", id);
            await userCollection.DeleteOneAsync(filter);
        }
    }
}