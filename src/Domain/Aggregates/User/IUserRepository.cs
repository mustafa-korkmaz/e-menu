namespace Domain.Aggregates.Product
{
    public interface IProductRepository : IRepository<Product>
    {
        Task<IReadOnlyCollection<Product>> ListByIdsAsync(string[] ids);
    }
}
