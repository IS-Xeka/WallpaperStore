using WallpaperStore.Core.Models;

namespace WallpaperStore.DataAccess.Repositories
{
    public interface IWallpapersRepository
    {
        Task<Guid> Create(Wallpaper wallpaper);
        Task<Guid> DeleteWallpaper(Guid id);
        Task<Wallpaper?> GetById(Guid id);
        Task<List<Wallpaper>> GetWallpapers();
        Task Update(Guid id, string title, string description);
    }
}