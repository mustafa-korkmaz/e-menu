
using MongoDB.Driver;

namespace Infrastructure.Persistence.MongoDb
{
    public interface IMongoContext : IDisposable
    {
        /// <summary>
        /// in order to support transactional changes
        /// </summary>
        /// <param name="transactionBody"></param>
        /// <returns></returns>
        Task SaveTransactionalChangesAsync(Func<Task> transactionBody);
        IMongoCollection<TDocument> GetCollection<TDocument>();

        /// <summary>
        /// will be used if a transactional session is created
        /// </summary>
        /// <returns></returns>
        IClientSessionHandle? GetSession();
    }
}
