namespace JAValmet_Backend.Config;

public static partial class ServiceCollectionExtension
{
    public static IServiceCollection ConfigEntitiesServices (this IServiceCollection services) 
    {
        // services.AddScoped<IImageService, ImageService>();
        return services;
    }
}