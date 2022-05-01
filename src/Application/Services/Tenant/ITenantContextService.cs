
namespace Application.Services.Tenant
{
    public interface ITenantContextService
    {
        public TenantContext TenantContext { get; set; }

        public bool CanEdit(string resourceId);
    }
}
