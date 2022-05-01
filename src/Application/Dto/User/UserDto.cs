
using Application.Enums;

namespace Application.Dto.User
{
    public class UserDto : DtoBase
    {
        public string Username { get; set; } = string.Empty;

        public string Email { get; set; } = string.Empty;

        public bool IsEmailConfirmed { get; set; }

        public string PasswordHash { get; set; } = string.Empty;

        public Subscription Subscription { get; set; }

        public DateTime SubscriptionExpiresAt { get; set; }
    }
}
