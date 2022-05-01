using System.Globalization;
using Application.Enums;
using Application.Services.Tenant;
using Infrastructure.Services;

namespace Presentation.Middlewares
{
    /// <summary>
    /// for all types of unexpected errors
    /// </summary>
    public class TenantMiddleware
    {
        private readonly RequestDelegate _next;

        /// <summary>
        /// ctor
        /// </summary>
        /// <param name="next"></param>
        public TenantMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        /// <summary>
        /// tenant middleware handler
        /// </summary>
        /// <param name="context"></param>
        /// <param name="tenantContextService"></param>
        /// <returns></returns>
        public async Task Invoke(HttpContext context, ITenantContextService tenantContextService)
        {
            var userId = GetUserId(context);

            if (userId != null)
            {
                tenantContextService.TenantContext.UserId = userId;
                tenantContextService.TenantContext.Subscription = GetSubscription(context);
                tenantContextService.TenantContext.IsSubscriptionExpired = IsSubscriptionExpired(context);
            }

            await _next(context);
        }

        private string? GetUserId(HttpContext context)
        {
            var userIdClaim = context.User.Claims.FirstOrDefault(p => p.Type == "id");

            var userId = userIdClaim?.Value;

            return userId;
        }

        private Subscription GetSubscription(HttpContext context)
        {
            var claim = context.User.Claims.FirstOrDefault(p => p.Type == "subscription");

            return claim!.Value.ToEnum<Subscription>();
        }

        private bool IsSubscriptionExpired(HttpContext context)
        {
            var claim = context.User.Claims.FirstOrDefault(p => p.Type == "subscription_exp");

            CultureInfo provider = CultureInfo.InvariantCulture;

            string dateString = claim!.Value;
            string format = "MMddyyyy";

            DateTime result = DateTime.ParseExact(dateString, format, provider);

            return result < DateTime.UtcNow;
        }
    }
}
