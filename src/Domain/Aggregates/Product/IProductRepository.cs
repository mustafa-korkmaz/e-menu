namespace Domain.Aggregates.Product
{
    public interface IProductRepository : IRepository<Product>
    {
        Task<IReadOnlyCollection<Product>> ListByMenuIdAsync(string menuId);

        Task<IReadOnlyCollection<Product>> ListByCategoryIdAsync(string categoryId);

        Task<ListDocumentResponse<Product>> ListAsync(ListDocumentRequest<string> request, string userId);
    }
}
