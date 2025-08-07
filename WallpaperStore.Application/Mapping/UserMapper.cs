using WallpaperStore.API.Contracts;
using WallpaperStore.Core.Models;

namespace WallpaperStore.Application.Mapping;

public class UserMapper : IUserMapper
{
    public UserDto MapToUserDto(User user, bool withWallpapers = false)
    {
        var addedWallpapers = !withWallpapers || user.AddedWallpapers is null
            ? Array.Empty<WallpaperDto>()
            : user.AddedWallpapers.Select(w => new WallpaperDto(
                w.Id,
                w.Title,
                w.Description,
                w.Url,
                w.Price,
                w.OwnerId)).ToArray();

        var savedWallpapers = !withWallpapers || user.SavedWallpapers is null
            ? Array.Empty<WallpaperDto>()
            : user.SavedWallpapers.Select(w => new WallpaperDto(
                w.Wallpaper.Id,
                w.Wallpaper.Title,
                w.Wallpaper.Description,
                w.Wallpaper.Url,
                w.Wallpaper.Price,
                w.Wallpaper.OwnerId)).ToArray();

        return new UserDto(
            user.Id,
            user.Name,
            user.Email,
            user.RegisterDate,
            user.LastTimeOnline,
            user.IsPublicProfile,
            addedWallpapers,
            savedWallpapers);
    }
}
