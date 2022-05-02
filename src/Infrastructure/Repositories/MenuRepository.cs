using Domain.Aggregates.Menu;
using Infrastructure.Persistence.MongoDb;
using MongoDB.Bson;
using MongoDB.Driver;

namespace Infrastructure.Repositories
{
    public class MenuRepository : RepositoryBase<Menu>, IMenuRepository
    {
        public MenuRepository(IMongoContext context) : base(context)
        {
        }

        public async Task<Menu?> GetByUrlSlugAsync(string urlSlug)
        {
            var filter = Builders<Menu>.Filter.Eq(doc => doc.UrlSlug, urlSlug);

            return await Collection.Find(filter).SingleOrDefaultAsync();
        }
    }
}
