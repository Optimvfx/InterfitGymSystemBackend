using BLL;
using CLL;
using DAL;
using GymCardSystemBackend.DependencyInjection;

var builder = WebApplication.CreateBuilder();

var connection = builder.Configuration.GetConnectionString("DefaultConnectionString");

builder.Services.AddMemoryCache();

builder.Services.AddDatabase(connection, LogLevel.Debug);

builder.Services.AddJwtAuth(builder.Configuration);
builder.Services.AddSubServices(builder.Configuration);
builder.Services.AddServices();
builder.Services.AddControllersLogic();

builder.Services.AddRateLimitPolicys();
builder.Services.AddAuthorizationPolicys();
builder.Services.AddControllers()
    .AddCustomJsonConvetors();

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddCustomSwaggerGen();

builder.Logging.AddCustomLogging(LogLevel.Warning);

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(c =>
    {
        c.SwaggerEndpoint("/swagger/v1/swagger.json", "Тренажерна Зала Interfit API V1");
        c.OAuthClientId("swaggerui");
        c.OAuthAppName("Swagger UI");
    });
}

app.AddMiddlewares(LogLevel.Warning);

app.UseHttpsRedirection();
app.UseRateLimiter();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();