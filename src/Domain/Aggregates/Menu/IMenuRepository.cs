namespace Domain.Aggregates.Menu
{
    public interface IMenuRepository : IRepository<Menu>
    {
        Task<Menu?> GetByUrlSlugAsync(string urlSlug);
    }
}
