using Microsoft.Extensions.DependencyInjection;
using web_token_Di.ExceptionHandling;

namespace web_token_Di.Extensions
{
    public static class ExceptionHandlingExtensions
    {
        public static IServiceCollection AddApplicationExceptionHandling(this IServiceCollection services)
        {
            services.AddExceptionHandler<GlobalExceptionHandler>();
            services.AddProblemDetails();
            return services;
        }
    }
}
