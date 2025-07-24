using WallpaperStore.API.Contracts;
using WallpaperStore.Core.Models;

namespace WallpaperStore.Application.Mapping;

public class UserMapper : IUserMapper
{
    public UserResponse MapToUserResponse(User user, bool withWallpapers = false)
    {
        var addedWallpapers = !withWallpapers || user.AddedWallpapers is null
            ? Array.Empty<WallpaperResponse>()
            : user.AddedWallpapers.Select(w => new WallpaperResponse(
                w.Id,
                w.Title,
                w.Description,
                w.Url,
                w.Price,
                w.OwnerId)).ToArray();

        var savedWallpapers = !withWallpapers || user.SavedWallpapers is null
            ? Array.Empty<WallpaperResponse>()
            : user.SavedWallpapers.Select(w => new WallpaperResponse(
                w.Wallpaper.Id,
                w.Wallpaper.Title,
                w.Wallpaper.Description,
                w.Wallpaper.Url,
                w.Wallpaper.Price,
                w.Wallpaper.OwnerId)).ToArray();

        return new UserResponse(
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
