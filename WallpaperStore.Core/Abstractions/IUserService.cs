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
    }
}