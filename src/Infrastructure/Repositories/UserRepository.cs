using Domain.Aggregates.Product;
using Domain.Aggregates.User;
using Infrastructure.Persistence.MongoDb;

namespace Infrastructure.Repositories
{
    internal class UserRepository : RepositoryBase<User>, IUserRepository
    {
        public UserRepository(IMongoContext context) : base(context)
        {

        }

        public Task<User?> GetByUsernameAsync(string username)
        {
            throw new NotImplementedException();
        }

        public Task<User?> GetByEmailAsync(string email)
        {
            throw new NotImplementedException();
        }
    }
}
