namespace Domain.Aggregates.User
{
    public interface IUserRepository : IRepository<User>
    {
        Task<User> GetByUsernameAsync(string username);

        Task<User> GetByEmailAsync(string email);
    }
}
