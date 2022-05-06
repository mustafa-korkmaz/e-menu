
using Application.Enums;

namespace Application.Services.Tenant
{
    public interface ITenantContext
    {
        string? UserId { get; set; }

        Subscription Subscription { get; set; }

        bool IsSubscriptionExpired { get; set; }

        bool CanEdit(string resourceId);
    }
}
