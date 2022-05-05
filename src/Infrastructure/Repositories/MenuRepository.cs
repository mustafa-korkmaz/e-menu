using Domain.Aggregates;
using Domain.Aggregates.Menu;
using Infrastructure.Persistence.MongoDb;
using MongoDB.Driver;

namespace Infrastructure.Repositories
{
    public class MenuRepository : RepositoryBase<Menu>, IMenuRepository
    {
        public MenuRepository(IMongoContext context) : base(context)
        {
        }

        public async Task<ListDocumentResponse<Menu>> ListAsync(ListDocumentRequest<string> request)
        {
            var response = new ListDocumentResponse<Menu>();

            var filter = Builders<Menu>.Filter.Eq(doc => doc.CreatedBy, request.SearchCriteria);

            var docs = Collection.Find(filter);

            response.TotalCount = await docs.CountDocumentsAsync();

            if (response.TotalCount > 0)
            {
                response.Items = await docs
                    .SortByDescending(p => p.Id)
                    .Skip(request.Offset)
                    .Limit(request.Limit)
                    .ToListAsync();
            }

            return response;
        }

        public async Task<Menu?> GetByUrlSlugAsync(string urlSlug)
        {
            var filter = Builders<Menu>.Filter.Eq(doc => doc.UrlSlug, urlSlug);

            return await Collection.Find(filter).SingleOrDefaultAsync();
        }
    }
}
