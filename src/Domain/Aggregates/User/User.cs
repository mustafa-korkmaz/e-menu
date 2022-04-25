
namespace Domain.Aggregates.User
{
    public class User : Document
    {
        public string Username { get; private set; }

        public string Email { get; private set; }

        public bool IsEmailConfirmed { get; private set; }

        public string PasswordHash { get; private set; }

        public byte Subscription { get; private set; }

        public DateTime SubscriptionExpiresAt { get; private set; }


        public User(string id, string username, string email, bool isEmailConfirmed, string passwordHash, byte subscription, DateTime subscriptionExpiresAt) : base(id)
        {
            Username = username;
            Email = email;
            IsEmailConfirmed = isEmailConfirmed;
            PasswordHash = passwordHash;
            Subscription = subscription;
            SubscriptionExpiresAt = subscriptionExpiresAt;
        }
    }
}
