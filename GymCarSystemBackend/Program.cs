using System.Net;
using AspNetCoreRateLimit;
using BLL;
using CLL;
using Common.Extensions;
using DAL;
using DAL.Entities.Access.AccessType;
using DAL.Extensions;
using GymCarSystemBackend;
using GymCarSystemBackend.DependencyInjection;
using GymCarSystemBackend.Extensions;
using GymCarSystemBackend.Middlewares;
using GymCarSystemBackend.Middlewares.ErrorHandler;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.EntityFrameworkCore;

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
builder.Services.AddControllers();

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddCustomSwaggerGen();

builder.Logging.AddCustomLogging(LogLevel.Warning);

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    using (var scope = app.Services.CreateScope())
    {
        var dbContext = scope.ServiceProvider.GetService<ApplicationDbContext>();

        if (dbContext.ApiAdministrators.Nothing())
        {
            dbContext.AddAdminApiKey(app.Configuration["MainApiKey"]);
        }
    }
    
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