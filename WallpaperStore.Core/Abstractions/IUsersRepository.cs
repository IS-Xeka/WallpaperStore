using CSharpFunctionalExtensions;
using WallpaperStore.Core.Models;

namespace WallpaperStore.DataAccess.Repositories
{
    public interface IUsersRepository
    {
        Task<Result<Guid>> Create(User user);
        Task<Result<User>> GetById(Guid id);
        Task<Result<User>> GetByIdWithWallpapers(Guid id);
        Task<Result<Guid>> Update(Guid id, string name);
        Task<Result<List<User>>> GetUsers();
        Task<Result<List<User>>> GetUsersWithWallpapers();
        Task<Result<Guid>> SaveWallpaper(Guid userId, Guid wallaperId, bool isFavorite);
        Task<Result<Guid>> AddWallpaper(Guid userId, Wallpaper wallpaper);
    }
}