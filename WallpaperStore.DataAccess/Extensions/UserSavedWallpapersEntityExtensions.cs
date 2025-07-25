using WallpaperStore.Application.Extensions;
using WallpaperStore.Core.Models;
using WallpaperStore.DataAccess.Entities;

namespace WallpaperStore.DataAccess.Extensions;

public static class UserSavedWallpapersEntityExtensions
{
    public static UserSavedWallpaper ToDomainWithWallpaper(this UserSavedWallpapersEntity entity)
    {
        var userSavedWallpaper = UserSavedWallpaper.Create(
            entity.UserId,
            entity.WallpaperId,
            entity.SavedDate,
            entity.IsFavorite).Value;
        userSavedWallpaper.AttachWallpaper(entity.WallpaperEntity.ToDomain());
        return userSavedWallpaper;
    }    
    public static UserSavedWallpaper ToDomain(this UserSavedWallpapersEntity entity)
    {
        return UserSavedWallpaper.Create(
            entity.UserId,
            entity.WallpaperId,
            entity.SavedDate,
            entity.IsFavorite).Value;
    }
}
