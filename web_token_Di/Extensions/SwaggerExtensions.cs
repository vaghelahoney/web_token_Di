using Microsoft.Extensions.DependencyInjection;

namespace web_token_Di.Extensions
{
    public static class SwaggerExtensions
    {
        public static IServiceCollection AddApplicationSwagger(this IServiceCollection services)
        {
            services.AddEndpointsApiExplorer();
            services.AddSwaggerGen();
            services.AddOpenApi();

            return services;
        }
    }
}
