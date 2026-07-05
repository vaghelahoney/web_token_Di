using Microsoft.AspNetCore.Http;
using System;
using System.Linq;

namespace web_token_Di.Services
{
    public class TenantService : ITenantService
    {
        private readonly IHttpContextAccessor _httpContextAccessor;
        private const string TenantHeaderName = "X-Tenant-Id";

        public TenantService(IHttpContextAccessor httpContextAccessor)
        {
            _httpContextAccessor = httpContextAccessor;
        }

        public Guid GetTenantId()
        {
            var httpContext = _httpContextAccessor.HttpContext;
            if (httpContext == null)
            {
                return Guid.Empty;
            }

            var tenantIdClaim = httpContext.User.FindFirst("TenantId")?.Value;
            if (!string.IsNullOrEmpty(tenantIdClaim) && Guid.TryParse(tenantIdClaim, out Guid tenantGuidClaim))
            {
                return tenantGuidClaim;
            }

            if (httpContext.Request.Headers.TryGetValue(TenantHeaderName, out var tenantIdValues))
            {
                var tenantIdHeader = tenantIdValues.FirstOrDefault();
                if (!string.IsNullOrEmpty(tenantIdHeader) && Guid.TryParse(tenantIdHeader, out Guid tenantGuidHeader))
                {
                    return tenantGuidHeader;
                }
            }

            return Guid.Empty;
        }
    }
}
