using System;

namespace web_token_Di.Models
{
    public interface IMustHaveTenant
    {
        Guid TenantId { get; set; }
    }
}
