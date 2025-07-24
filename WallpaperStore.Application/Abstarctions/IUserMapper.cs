using WallpaperStore.API.Contracts;
using WallpaperStore.Core.Models;

namespace WallpaperStore.Application.Mapping
{
    public interface IUserMapper
    {
        UserResponse MapToUserResponse(User user, bool withWallpapers = false);
    }
}