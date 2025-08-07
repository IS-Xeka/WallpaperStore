using WallpaperStore.API.Contracts;
using WallpaperStore.Core.Models;

namespace WallpaperStore.Application.Mapping
{
    public interface IUserMapper
    {
        UserDto MapToUserDto(User user, bool withWallpapers = false);
    }
}