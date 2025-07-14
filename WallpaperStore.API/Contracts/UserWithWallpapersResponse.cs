using WallpaperStore.Core.Models;
using WallpaperStore.DataAccess.Entities;

namespace WallpaperStore.API.Contracts
{
    public record UserWithWallpapersResponse(
        Guid Id,
        string Name,
        Email Email,
        DateTime RegisterDate,
        DateTime? LastTimeOnline,
        bool IsPublicProfile,
        IReadOnlyCollection<WallpaperResponse> AddedWallpapers,
        IReadOnlyCollection<WallpaperResponse> SavedWallpapers
        );
}
