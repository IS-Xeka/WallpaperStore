using CSharpFunctionalExtensions;
using WallpaperStore.Core.Models;

namespace WallpaperStore.Application.Services
{
    public interface IWallpapersService
    {
        Task<Result<Guid>> AddWallpaperAsync(Guid userId, Wallpaper wallpaper, CancellationToken ct = default);
        Task<Result<List<Wallpaper>>> GetAsync();
        Task<Result<List<Wallpaper>>> GetUserWallpapersAsync(Guid userId);
    }
}