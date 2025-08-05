using CSharpFunctionalExtensions;
using WallpaperStore.Core.Models;

namespace WallpaperStore.Application.Services
{
    public interface IUserService
    {
        Task<Result<Guid>> CreateAsync(User user, CancellationToken ct = default);
        Task<Result> DeleteAsync(Guid id, CancellationToken ct = default);
        Task<Result<List<User>>> GetAsync();
        Task<Result<User>> GetByIdAsync(Guid id);
        Task<Result<Guid>> UpdateAsync(Guid id, string name, CancellationToken ct = default);
        Task<Result<List<UserSavedWallpaper>>> GetAllSavedWallpapersAsync(Guid? userId = null, Guid? wallpaperId = null, bool includeWallpapers = false, CancellationToken ct = default);
        Task<Result<UserSavedWallpaper>> GetSavedWallpaperAsync(Guid userId, Guid wallpaperId, bool includeWallpaper = false, CancellationToken ct = default);
        Task<Result> SaveWallpaperAsync(Guid userId, Guid wallpaperId, bool isFavorite = false, CancellationToken ct = default);
        Task<Result> UnsaveWallpaperAsync(Guid userId, Guid wallpaperId, bool isFavorite = false, CancellationToken ct = default);
    }
}