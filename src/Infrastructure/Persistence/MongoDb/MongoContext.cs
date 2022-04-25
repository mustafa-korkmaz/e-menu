using Infrastructure.Configuration;
using Microsoft.Extensions.Options;
using MongoDB.Driver;

namespace Infrastructure.Persistence.MongoDb
{
    public class MongoContext : IMongoContext
    {
        private readonly IMongoDatabase _database;
        private IClientSessionHandle? _session;

        private readonly IMongoClient _mongoClient;

        public MongoContext(IMongoClient mongoClient, IOptions<MongoDbConfig> dbConfig)
        {
            _mongoClient = mongoClient;
            _database = _mongoClient.GetDatabase(dbConfig.Value.DatabaseName);
        }

        public IMongoCollection<TDocument> GetCollection<TDocument>()
        {
            return _database.GetCollection<TDocument>(GetCollectionName(typeof(TDocument)));
        }

        public async Task SaveTransactionalChangesAsync(Func<Task> transactionBody)
        {
            using (_session = await _mongoClient.StartSessionAsync())
            {
                try
                {
                    _session.StartTransaction();

                    await transactionBody();

                    await _session.CommitTransactionAsync();
                }
                catch (Exception)
                {
                    await _session.AbortTransactionAsync();
                    throw;
                }
            }
        }

        private protected string GetCollectionName(Type documentType)
        {
            var shortType = documentType.Name;

            char[] array = shortType.ToCharArray();

            array[0] = char.ToLower(array[0]);

            var singularCollectionName = new string(array);

            var pluralCollectionName = array[array.Length - 1] == 's' ? singularCollectionName + "es" : singularCollectionName + "s";

            return pluralCollectionName;
        }

        public IClientSessionHandle? GetSession()
        {
            return _session;
        }

        public void Dispose()
        {
            _session?.Dispose();
            GC.SuppressFinalize(this);
        }
    }
}
