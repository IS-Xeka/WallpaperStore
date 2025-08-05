using WallpaperStore.Application.Propertioes;
using WallpaperStore.DataAccess;
using WallpaperStore.DataAccess.Properties;

namespace WallpaperStore.API;

public static class DependencyInjection
{
    public static IServiceCollection AddProgramDependencies(
        this IServiceCollection services, IConfiguration configuration)
    {
        string connectionString = configuration.GetConnectionString("WallpaperStoreDbContext")
                    ?? throw new InvalidOperationException("Connection string 'WallpaperStoreDbContext' not found in configuration");
        services.AddWebDependencies();
        services.AddApplication();
        Console.WriteLine(configuration.GetConnectionString(nameof(WallpaperStoreDbContext)));
        services.AddPostgreSqlDependecies(connectionString);
        return services;
    }

    public static IServiceCollection AddWebDependencies(
        this IServiceCollection services)
    {
        services.AddEndpointsApiExplorer();
        services.AddSwaggerGen();
        services.AddControllers();

        return services;
    }
}
