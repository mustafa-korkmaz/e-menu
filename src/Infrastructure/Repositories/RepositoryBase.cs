using Domain.Aggregates;
using Infrastructure.Persistence.MongoDb;
using MongoDB.Bson;
using MongoDB.Driver;

namespace Infrastructure.Repositories
{
    public class RepositoryBase<TDocument> : IRepository<TDocument> where TDocument : IDocument
    {
        private protected readonly IMongoCollection<TDocument> Collection;
        private readonly IMongoContext _mongoContext;

        public RepositoryBase(IMongoContext context)
        {
            _mongoContext = context;
            Collection = _mongoContext.GetCollection<TDocument>();
        }

        public async Task<ListDocumentResponse<TDocument>> ListAsync(int offset, int limit)
        {
            var response = new ListDocumentResponse<TDocument>();

            var docs = Collection.Find(new BsonDocument());

            response.TotalCount = await docs.CountDocumentsAsync();

            if (response.TotalCount > 0)
            {
                response.Items = await docs
                    .SortByDescending(p => p.Id)
                    .Skip(offset)
                    .Limit(limit)
                    .ToListAsync();
            }

            return response;
        }

        public Task InsertOneAsync(TDocument document)
        {
            var session = GetSession();

            if (session == null)
            {
                return Collection.InsertOneAsync(document);
            }

            return Collection.InsertOneAsync(session, document);
        }

        public Task InsertManyAsync(ICollection<TDocument> documents)
        {
            var session = GetSession();

            if (session == null)
            {
                return Collection.InsertManyAsync(documents);
            }

            return Collection.InsertManyAsync(session, documents);
        }

        public async Task ReplaceOneAsync(TDocument document)
        {
            var filter = Builders<TDocument>.Filter.Eq(doc => doc.Id, document.Id);

            var session = GetSession();

            if (session == null)
            {
                await Collection.FindOneAndReplaceAsync(filter, document);
                return;
            }

            await Collection.FindOneAndReplaceAsync(session, filter, document);
        }

        public async Task<TDocument?> GetByIdAsync(string id)
        {
            var filter = Builders<TDocument>.Filter.Eq(doc => doc.Id, id);
            return await Collection.Find(filter).SingleOrDefaultAsync();
        }

        public async Task DeleteByIdAsync(string id)
        {
            var filter = Builders<TDocument>.Filter.Eq(doc => doc.Id, id);

            var session = GetSession();

            if (session == null)
            {
                await Collection.FindOneAndDeleteAsync(filter);
                return;
            }

            await Collection.FindOneAndDeleteAsync(session, filter);
        }

        private IClientSessionHandle? GetSession()
        {
            return _mongoContext.GetSession();
        }
    }
}
