using CSharpFunctionalExtensions;
using WallpaperStore.Core.Models;

namespace WallpaperStore.DataAccess.Repositories
{
    public interface IWallpapersRepository
    {
        Task<Result<Guid>> Create(Wallpaper wallpaper);
        Task<Result<Guid>> DeleteWallpaper(Guid id);
        Task<Result<Wallpaper>> GetById(Guid id);
        Task<Result<Wallpaper>> GetByIdWithOwner(Guid id);
        Task<Result<List<Wallpaper>>> GetWallpapers();
        Task<Result<List<Wallpaper>>> GetWallpapersWithOwners();
        Task<Result> Update(Guid id, string title, string description);
    }
}