using System;
using System.ComponentModel.DataAnnotations;

namespace web_token_Di.Models
{
    public interface IMustHaveTenant
    {
        public Guid TenantId { get; set; }
    }
}
