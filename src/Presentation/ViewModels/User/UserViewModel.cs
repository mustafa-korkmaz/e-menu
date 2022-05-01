
namespace Presentation.ViewModels.User
{
    public class UserViewModel : ViewModelBase
    {
        public string? Username { get; set; }

        public string? Email { get; set; }

        public bool IsEmailConfirmed { get; set; }

        public string? Subscription { get; set; }

        public DateTime SubscriptionExpiresAt { get; set; }
    }
}
