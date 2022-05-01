using Application.Enums;

namespace Application.Services.Tenant
{
    public class TenantContext
    {
        public string? UserId { get; set; }

        public Subscription Subscription { get; set; }

        public bool IsSubscriptionExpired { get; set; }
    }
}
