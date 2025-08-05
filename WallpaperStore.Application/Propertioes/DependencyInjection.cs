using Microsoft.Extensions.DependencyInjection;
using WallpaperStore.Application.Mapping;
using WallpaperStore.Application.Services;

namespace WallpaperStore.Application.Propertioes;

public static class DependencyInjection
{
    public static IServiceCollection AddApplication(
        this IServiceCollection services)
    {
        services.AddScoped<IWallpapersService, WallpapersService>();
        services.AddScoped<IUserService, UserService>();
        services.AddScoped<IUserMapper, UserMapper>();

        return services;
    }
}
