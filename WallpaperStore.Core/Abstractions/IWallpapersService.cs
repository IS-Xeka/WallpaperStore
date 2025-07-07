using WallpaperStore.Core.Models;

namespace WallpaperStore.Application.Services
{
    public interface IWallpapersService
    {
        Task<Guid> CreateWallpaper(Wallpaper wallpaper);
        Task<List<Wallpaper>> GetWallpapers();
    }
}