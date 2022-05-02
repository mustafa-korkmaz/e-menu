
namespace Presentation.ViewModels.User
{
    public class UserViewModel : ViewModelBase
    {
        public string Username { get; set; } = string.Empty;

        public string Email { get; set; } = string.Empty;

        public bool IsEmailConfirmed { get; set; }

        public string Subscription { get; set; } = string.Empty;

        public DateTime SubscriptionExpiresAt { get; set; }
    }
}
