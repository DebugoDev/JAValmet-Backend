namespace JAValmet_Backend.Config;

using JAValmet_Backend.Core.Middlewares.ErrorHandler;

public static partial class ServiceCollectionExtension
{
    public static IServiceCollection ConfigMiddlewares(this IServiceCollection services)
    {
        services.AddTransient<ErrorHandlingMiddleware>();
        // services.AddTransient<AuthenticationMiddleware>();
        return services;
    }
}