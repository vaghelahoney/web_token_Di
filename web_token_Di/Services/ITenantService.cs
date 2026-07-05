using System;

namespace web_token_Di.Services
{
    public interface ITenantService
    {
        Guid GetTenantId();
    }
}
