
using Application.Dto.User;

namespace Application.Services.Account
{
    public interface IAccountService
    {
        /// <summary>
        /// Checks for user by username or email. Sets user info and returns a valid token
        /// </summary>
        /// <param name="userDto"></param>
        /// <param name="password"></param>
        /// <returns></returns>
        Task<string> GetTokenAsync(UserDto userDto, string password);

        /// <summary>
        /// Creates user and sets user info
        /// </summary>
        /// <param name="userDto"></param>
        /// <param name="password"></param>
        /// <returns></returns>
        Task RegisterAsync(UserDto userDto, string password);

        Task ResetAccountAsync(string emailOrUsername);

        /// <summary>
        /// validates security code and returns respective user
        /// </summary>
        /// <param name="password"></param>
        /// <param name="securityCode"></param>
        /// <returns></returns>
        Task<Guid> ConfirmPasswordResetAsync(string password, string securityCode);

        /// <summary>
        /// changes user password
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="newPassword"></param>
        /// <returns></returns>
        Task ChangePasswordAsync(string userId, string newPassword);

        Task ChangePasswordAsync(string userId, string oldPassword, string newPassword);

        /// <summary>
        /// returns current user info
        /// </summary>
        /// <returns></returns>
        Task<UserDto> GetUserAsync();

        Task<Guid?> GetUserIdAsync(string email);
    }
}
