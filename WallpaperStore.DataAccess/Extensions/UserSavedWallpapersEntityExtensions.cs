using WallpaperStore.Application.Extensions;
using WallpaperStore.Core.Models;
using WallpaperStore.DataAccess.Entities;

namespace WallpaperStore.DataAccess.Extensions;

public static class UserSavedWallpapersEntityExtensions
{
    public static UserSavedWallpaper ToDomain(this UserSavedWallpapersEntity entity)
    {
        return UserSavedWallpaper.Create(
            entity.UserEntity.ToDomain(),
            entity.WallpaperEntity.ToDomain(),
            entity.IsFavorite).Value;
    }
}
