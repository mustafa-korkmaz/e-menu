using Domain.Aggregates.Product;
using Infrastructure.Persistence.MongoDb;
using MongoDB.Driver;

namespace Infrastructure.Repositories
{
    public class ProductRepository : RepositoryBase<Product>, IProductRepository
    {
        public ProductRepository(IMongoContext context) : base(context)
        {
        }

        public async Task<IReadOnlyCollection<Product>> ListByIdsAsync(string[] ids)
        {
            return await Collection.Find(p => ids.Contains(p.Id)).ToListAsync();
        }
    }
}
