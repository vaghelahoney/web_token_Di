using Microsoft.Extensions.DependencyInjection;

namespace web_token_Di.Extensions
{
    public static class CorsExtensions
    {
        public const string CorsPolicyName = "_myAllowSpecificOrigins";

        public static IServiceCollection AddApplicationCors(this IServiceCollection services)
        {
            services.AddCors(options =>
            {
                options.AddPolicy(name: CorsPolicyName,
                    policy =>
                    {
                        policy.WithOrigins("http://localhost:4200")
                              .AllowAnyHeader()
                              .AllowAnyMethod();
                    });
            });

            return services;
        }
    }
}
