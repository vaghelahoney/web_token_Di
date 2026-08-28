using Microsoft.AspNetCore.Builder;
using Serilog;

namespace web_token_Di.Extensions
{
    public static class SerilogExtensions
    {
        public static WebApplicationBuilder AddApplicationSerilog(this WebApplicationBuilder builder)
        {
            Log.Logger = new LoggerConfiguration()
                .ReadFrom.Configuration(builder.Configuration)
                .CreateLogger();

            builder.Host.UseSerilog();

            return builder;
        }
    }
}
