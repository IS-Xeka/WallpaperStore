using WallpaperStore.Core.Models;

namespace WallpaperStore.Application.Services
{
    public interface IWallpapersService
    {
        Task UpdateWallpaper(Guid id, string title, string description);
        Task<Guid> DeleteWallpaper(Guid id);
        Task<Guid> CreateWallpaper(Wallpaper wallpaper);
        Task<List<Wallpaper>> GetWallpapers();
    }
}