using Microsoft.EntityFrameworkCore;

namespace JAValmet_Backend.Config;

public static partial class ServiceCollectionExtension
{
    public static IServiceCollection ConfigDatabase(this IServiceCollection services, ConfigurationManager configuration)
    {
        var connectionString = configuration.GetConnectionString("SqlServer");
        services.AddDbContext<JAValmetContext>(
            options => options.UseNpgsql(connectionString)
        );

        return services;
    }
}