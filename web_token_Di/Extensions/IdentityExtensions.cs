using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection;
using web_token_Di.Data;
using web_token_Di.Models;

namespace web_token_Di.Extensions
{
    public static class IdentityExtensions
    {
        public static IServiceCollection AddApplicationIdentity(this IServiceCollection services)
        {
            services.AddIdentity<ApplicationUser, IdentityRole>()
                .AddEntityFrameworkStores<ApplicationDbContext>()
                .AddDefaultTokenProviders();

            return services;
        }
    }
}
