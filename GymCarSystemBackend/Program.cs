using BLL;
using CCL;
using DAL;
using GymCarSystemBackend;
using Microsoft.AspNetCore.Authentication.Cookies;

var builder = WebApplication.CreateBuilder();

var connection = builder.Configuration.GetConnectionString("DefaultConnectionString");

builder.Services.AddJwtAuth(builder.Configuration);

builder.Services.AddDatabase(connection);

builder.Services.AddAbstractServices();
builder.Services.AddServices();
builder.Services.AddControllersLogic();

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddCustomSwaggerGen();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    using (var scope = app.Services.CreateScope())
    {
        var dbContext = scope.ServiceProvider.GetService<ApplicationDbContext>();

        if (!dbContext.IsInitialized())
            ApplicationDbInitializer.Initialize(dbContext);
    }
    
    app.UseSwagger();
    app.UseSwaggerUI(c =>
    {
        c.SwaggerEndpoint("/swagger/v1/swagger.json", "Тренажерна Зала Interfit API V1");
        c.OAuthClientId("swaggerui");
        c.OAuthAppName("Swagger UI");
    });
}

app.UseHttpsRedirection();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();