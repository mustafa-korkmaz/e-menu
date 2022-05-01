using Domain.Aggregates.Product;
using Domain.Aggregates.User;
using Infrastructure.Persistence.MongoDb;
using MongoDB.Driver;

namespace Infrastructure.Repositories
{
    internal class UserRepository : RepositoryBase<User>, IUserRepository
    {
        public UserRepository(IMongoContext context) : base(context)
        {

        }

        public Task<User> GetByUsernameAsync(string username)
        {
            var filter = Builders<User>.Filter.Eq(doc => doc.Username, username);
            return Collection.Find(filter).SingleOrDefaultAsync();
        }

        public Task<User> GetByEmailAsync(string email)
        {
            var filter = Builders<User>.Filter.Eq(doc => doc.Email, email);
            return Collection.Find(filter).SingleOrDefaultAsync();
        }
    }
}
