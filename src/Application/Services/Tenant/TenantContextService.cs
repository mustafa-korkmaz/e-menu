
namespace Application.Services.Tenant
{
    public class TenantContextService : ITenantContextService
    {
        public TenantContext TenantContext { get; set; } = new();
        public bool CanEdit(string resourceCreatedBy)
        {
            return resourceCreatedBy == TenantContext.UserId;
        }
    }
}
