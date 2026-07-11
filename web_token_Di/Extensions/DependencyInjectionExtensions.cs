using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using web_token_Di.Repositories;
using web_token_Di.Services;

namespace web_token_Di.Extensions
{
    public static class DependencyInjectionExtensions
    {
        public static IServiceCollection AddApplicationDependencies(this IServiceCollection services)
        {
            services.AddHttpContextAccessor();
            services.AddScoped<ITenantService, TenantService>();
            services.AddScoped<IAuthRepository, AuthRepository>();
            services.AddScoped<IEmployeeRepositories, EmployeeRepositories>();

            return services;
        }
    }
}
