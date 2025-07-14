using CSharpFunctionalExtensions;
using WallpaperStore.Core.Models;

namespace WallpaperStore.Application.Services
{
    public interface IWallpapersService
    {   
        Task<Result<Guid>> CreateWallpaper(Wallpaper wallpaper);
        Task<Result<Guid>> DeleteWallpaper(Guid id);
        Task<Result<Wallpaper>> GetById(Guid id);
        Task<Result<Wallpaper>> GetByIdWithOwner(Guid id);
        Task<Result<List<Wallpaper>>> GetWallpapers();
        Task<Result<List<Wallpaper>>> GetWallpapersWithOwners();
        Task<Result<Guid>> UpdateWallpaper(Guid id, string title, string description);
    }
}