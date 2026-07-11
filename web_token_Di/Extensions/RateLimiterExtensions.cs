using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.RateLimiting;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Threading.RateLimiting;

namespace web_token_Di.Extensions
{
    public static class RateLimiterExtensions
    {
        public static IServiceCollection AddApplicationRateLimiter(this IServiceCollection services)
        {
            services.AddRateLimiter(options =>
            {
                options.RejectionStatusCode = StatusCodes.Status429TooManyRequests;
                options.OnRejected = async (context, token) =>
                {
                    context.HttpContext.Response.ContentType = "application/json";
                    await context.HttpContext.Response.WriteAsJsonAsync(new
                    {
                        statusCode = 429,
                        message = "Too many requests. Please try again later."
                    }, cancellationToken: token);
                };

                options.AddFixedWindowLimiter(policyName: "ApiPolicy", fixedWindowOptions =>
                {
                    fixedWindowOptions.PermitLimit = 10;
                    fixedWindowOptions.Window = TimeSpan.FromMinutes(1);
                    fixedWindowOptions.QueueLimit = 0;
                });
            });

            return services;
        }
    }
}
