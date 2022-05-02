
namespace Domain.Aggregates
{
    public interface IRepository<TDocument> where TDocument : IDocument
    {
        Task<TDocument?> GetByIdAsync(string id);
        Task InsertOneAsync(TDocument document);
        Task InsertManyAsync(ICollection<TDocument> documents);
        Task ReplaceOneAsync(TDocument document);
        Task DeleteByIdAsync(string id);
    }
}
