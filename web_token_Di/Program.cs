using Serilog;
using web_token_Di.Extensions;

try
{
    var builder = WebApplication.CreateBuilder(args);

    // Configure Serilog early
    builder.AddApplicationSerilog();

    Log.Information("Starting web application...");

    // Add services to the container.
    builder.Services.AddDatabase(builder.Configuration);
    builder.Services.AddApplicationIdentity();
    builder.Services.AddApplicationCors();
    builder.Services.AddJwtAuthentication(builder.Configuration);
    builder.Services.AddApplicationRateLimiter();
    builder.Services.AddApplicationDependencies();
    builder.Services.AddApplicationSwagger();
    builder.Services.AddApplicationExceptionHandling();
    builder.Services.AddControllers();

    var app = builder.Build();

    if (app.Environment.IsDevelopment())
    {
        app.UseSwagger();  
        app.UseSwaggerUI();
        app.MapOpenApi();
    }

    app.UseSerilogRequestLogging();
    app.UseExceptionHandler();

    app.UseHttpsRedirection();
    app.UseCors(CorsExtensions.CorsPolicyName); 
    app.UseRateLimiter();
    app.UseAuthentication();
    app.UseAuthorization();

    app.MapControllers();

    app.Run();
}
catch (Exception ex)
{
    Log.Fatal(ex, "Application startup failed");
}
finally
{
    Log.CloseAndFlush();
}

