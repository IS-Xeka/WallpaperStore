using CSharpFunctionalExtensions;
using WallpaperStore.Core.Models;

namespace WallpaperStore.Application.Services
{
    public interface IWallpapersService
    {
        Task<Result<Guid>> AddWallpaperAsync(Guid userId, Wallpaper wallpaper, CancellationToken ct = default);
        Task<Result<List<Wallpaper>>> GetAsync();
        Task<Result<List<Wallpaper>>> GetUserWallpapersAsync(Guid userId);
        Task<Result<List<UserSavedWallpaper>>> GetAllSavedWallpapersAsync(Guid? userId = null, Guid? wallpaperId = null, bool includeWallpapers = false, CancellationToken ct = default);
        Task<Result<UserSavedWallpaper>> GetSavedWallpaperAsync(Guid userId, Guid wallpaperId, bool includeWallpaper = false, CancellationToken ct = default);
        Task<Result> SaveWallpaperAsync(Guid userId, Guid wallpaperId, bool isFavorite = false, CancellationToken ct = default);
        Task<Result> UnsaveWallpaperAsync(Guid userId, Guid wallpaperId, bool isFavorite = false, CancellationToken ct = default);
    }
}