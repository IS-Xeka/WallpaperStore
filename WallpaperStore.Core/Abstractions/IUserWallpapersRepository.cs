using CSharpFunctionalExtensions;
using WallpaperStore.Core.Models;

namespace WallpaperStore.DataAccess.Repositories
{
    public interface IUserWallpapersRepository
    {
        Task<Result<List<UserSavedWallpaper>>> GetAllSavedWallpapersAsync(Guid? userId = null, Guid? wallpaperId = null, bool includeWallpapers = false, CancellationToken ct = default);
        Task<Result<UserSavedWallpaper>> GetSavedWallpaperAsync(Guid userId, Guid wallpaperId, bool includeWallpaper = false, CancellationToken ct = default);
        Task<Result> SaveWallpaperAsync(Guid userId, Guid wallpaperId, bool isFavorite = false, CancellationToken ct = default);
        Task<Result> UnsaveWallpaperAsync(Guid userId, Guid wallpaperId, bool isFavorite = false, CancellationToken ct = default);
    }
}