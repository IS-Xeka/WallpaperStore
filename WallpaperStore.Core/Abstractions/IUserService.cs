using CSharpFunctionalExtensions;
using WallpaperStore.Core.Models;

namespace WallpaperStore.Application.Services
{
    public interface IUserService
    {
        Task<Result<Guid>> CreateUser(User user);
        Task<Result<User>> GetUserById(Guid id);
        Task<Result<User>> GetUserByIdWithWallpapers(Guid id);
        Task<Result<Guid>> UpdateUser(Guid id, string name);
        Task<Result<List<User>>> GetAll();
        Task<Result<List<User>>> GetAllWithWallpapers();
        Task<Result<Guid>> SaveWallpaper(Guid userId, Guid wallaperId, bool isFavorite);
        Task<Result<Guid>> AddWallpaper(Guid userId, Wallpaper wallpaper);
    }
}