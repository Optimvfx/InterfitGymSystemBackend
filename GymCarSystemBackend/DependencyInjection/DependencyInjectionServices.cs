using System.Buffers.Text;
using System.Net;
using System.Text;
using BLL.Services.DataCoder;
using BLL.Services.TokenService;
using CLL.Consts;
using GymCarSystemBackend.Controllers.AuthorizationHandlers;
using GymCarSystemBackend.Extensions;
using GymCarSystemBackend.Middlewares.ErrorHandler;
using GymCarSystemBackend.Middlewares.ErrorHandler.PossibleErrorHandler;
using Microsoft.AspNetCore.Authorization;

namespace GymCarSystemBackend.DependencyInjection;

public static class DependencyInjectionServices
{
    private const string ByteCoderKey = "ByteCoderKey";
    
    public static IServiceCollection AddSubServices(this IServiceCollection services,
        ConfigurationManager configuration)
    {
        var key = DependencyInjectionJwt.GetSymmetricSecurityKey(configuration);
        services.AddTransient<ITokenService, StandartTokenService>((s) => new StandartTokenService(key, TimeSpan.FromMinutes(200), Claims.UserClaim));

        var byteCoderKey = Encoding.UTF8.GetBytes(configuration[ByteCoderKey]);
        services.AddTransient<IDataCoder<Guid, string>, GuidStringCoder>((s) => 
            new GuidStringCoder(
                new ByteStringCoder(), 
                new ByteDataCoder(byteCoderKey)));
        
        return services;
    }
}