
using Application.Enums;

namespace Application.Services.Tenant
{
    public class TenantContext : ITenantContext
    {
        public string? UserId { get; set; }

        public Subscription Subscription { get; set; }

        public bool IsSubscriptionExpired { get; set; }
     
        public bool CanEdit(string resourceCreatedBy)
        {
            return resourceCreatedBy == UserId;
        }
    }
}
