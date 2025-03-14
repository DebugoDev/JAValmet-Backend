namespace JAValmet_Backend.Config;

using System.IdentityModel.Tokens.Jwt;
using JAValmet_Backend.Core.Services.JWT;

public static partial class ServiceCollectionExtension
{
    public static IServiceCollection ConfigJwt(this IServiceCollection services, ConfigurationManager configuration)
    {
        var jwtSettings = new JwtSettings()
        {
            SecretKey = configuration.GetSection("JwtSettings")
                    .GetValue<string>("SecretKey")!
        };
        services.AddSingleton(jwtSettings);
        services.AddSingleton<JwtSecurityTokenHandler>();
        services.AddScoped<JwtService>();

        return services;

    }
}