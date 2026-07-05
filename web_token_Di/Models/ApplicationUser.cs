using System;
using Microsoft.AspNetCore.Identity;

namespace web_token_Di.Models
{
    public class ApplicationUser : IdentityUser
    {
        public string Name { get; set; } = string.Empty;
        public string Address { get; set; } = string.Empty;
        public Guid TenantId { get; set; }
    }
}
