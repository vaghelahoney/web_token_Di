using web_token_Di.Extensions;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddDatabase(builder.Configuration);
builder.Services.AddApplicationIdentity();
builder.Services.AddApplicationCors();
builder.Services.AddJwtAuthentication(builder.Configuration);
builder.Services.AddApplicationRateLimiter();
builder.Services.AddApplicationDependencies();
builder.Services.AddApplicationSwagger();
builder.Services.AddControllers();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();  
    app.UseSwaggerUI();
    app.MapOpenApi();
}

app.UseHttpsRedirection();
app.UseCors(CorsExtensions.CorsPolicyName); 
app.UseRateLimiter();
app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();
