﻿using System.Text;
using BLL.Services.DataCoder;
using BLL.Services.TokenService;
using GymCardSystemBackend.Consts;
using GymCardSystemBackend.Singleton;

namespace GymCardSystemBackend.DependencyInjection;

public static class DependencyInjectionServices
{
    private const string ByteCoderKey = "ByteCoderKey";
    
    public static IServiceCollection AddSubServices(this IServiceCollection services,
        ConfigurationManager configuration)
    {
        services.AddTokenService(configuration);
        services.AddGuidCoderService(configuration);
        
        return services;
    }

    private static void AddTokenService(this IServiceCollection services, ConfigurationManager configuration)
    {
        var key = DependencyInjectionJwt.GetSymmetricSecurityKey(configuration);
        services.AddTransient<ITokenService, StandartTokenService>((s) => new StandartTokenService(key, TimeSpan.FromMinutes(200), Claims.UserClaim));
    }
    
    private static void AddGuidCoderService(this IServiceCollection services, ConfigurationManager configuration)
    {
        var coderKey = Encoding.UTF8.GetBytes(configuration[ByteCoderKey]);
        var byteCoder = new ByteDataCoder(coderKey);
        var coder = new GuidStringCoder(
            new ByteStringCoder(),
            byteCoder);
        
        services.AddSingleton<IDataCoder<Guid, string>>((s) => coder);
        
        GuidCoderSingleton.Init(coder);
    }
}