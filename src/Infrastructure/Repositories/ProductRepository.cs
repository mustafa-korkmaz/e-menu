using Domain.Aggregates;
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

        public async Task<ListDocumentResponse<Product>> ListAsync(ListDocumentRequest<string> request, string userId)
        {
            var response = new ListDocumentResponse<Product>();

            var filterBuilder = Builders<Product>.Filter;

            var filter = filterBuilder.Eq(doc => doc.CreatedBy, userId) &
                         filterBuilder.Eq(doc => doc.MenuId, request.SearchCriteria);

            var docs = Collection.Find(filter);

            response.TotalCount = await docs.CountDocumentsAsync();

            if (response.TotalCount > 0)
            {
                response.Items = await docs
                    .SortBy(p => p.DisplayOrder)
                    .ThenByDescending(p => p.Id)
                    .Skip(request.Offset)
                    .Limit(request.Limit)
                    .ToListAsync();
            }

            return response;
        }
    }
}
