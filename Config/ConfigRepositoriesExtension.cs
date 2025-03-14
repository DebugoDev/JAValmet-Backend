namespace JAValmet_Backend.Config;

public static partial class ServiceCollectionExtension
{
    public static IServiceCollection ConfigEntitiesRepositories(this IServiceCollection services)
    {
        // services.AddScoped<BaseRepository<Class>, ClassRepository>();
        // services.AddScoped<IClassRepository, ClassRepository>();

        return services;
    }
}