namespace Domain.Aggregates.Product
{
    public interface IProductRepository : IRepository<Product>
    {
        Task<IReadOnlyCollection<Product>> ListByIdsAsync(string[] ids);

        Task<ListDocumentResponse<Product>> ListAsync(ListDocumentRequest<string> request, string userId);
    }
}
