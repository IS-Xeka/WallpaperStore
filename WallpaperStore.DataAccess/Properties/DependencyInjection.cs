using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using WallpaperStore.DataAccess.Repositories;

namespace WallpaperStore.DataAccess.Properties;


public static class DependencyInjection
{
    public static IServiceCollection AddPostgreSqlDependecies(
        this IServiceCollection services,
        string connectionsString)
    {
        services.AddScoped<IWallpapersRepository, WallpapersRepository>();
        services.AddScoped<IUsersRepository, UsersRepository>();
        services.AddScoped<IUserWallpapersRepository, UserWallpapersRepository>();

        services.AddDbContext<WallpaperStoreDbContext>(
        options =>
        {
            options.UseNpgsql(connectionsString);
        });

        return services;
    }
}
