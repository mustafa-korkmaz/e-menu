namespace Domain.Aggregates.Product
{
    public interface IUserRepository : IRepository<User.User>
    {
        Task<User.User?> GetByUsernameAsync(string username);

        Task<User.User?> GetByEmailAsync(string email);
    }
}
