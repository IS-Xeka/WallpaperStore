using WallpaperStore.Core.Models;
using WallpaperStore.DataAccess.Entities;
using WallpaperStore.DataAccess.Extensions;

namespace WallpaperStore.Application.Extensions;

public static class UserEntityExtensions
{
    public static User ToDomain(this UserEntity entity)
    {
        return User.Create(
            entity.Id,
            entity.Name,
            entity.Email,
            entity.PasswordHash,
            entity.RegisterDate,
            entity.IsPublicProfile);
    }

    public static User ToDomainWithWallpapers(this UserEntity entity)
    {
        var user =  User.Create(
            entity.Id,
            entity.Name,
            entity.Email,
            entity.PasswordHash,
            entity.RegisterDate,
            entity.IsPublicProfile);
        
        foreach(var wallpaperEntity in entity.AddedWallpapers)
        {
            user.AddWallpaper(wallpaperEntity.ToDomain());
        }

        foreach (var wallpaperEntity in entity.SavedWallpapers)
        {
            user.SaveWallpaper(wallpaperEntity.WallpaperEntity.ToDomain());
        }

        return user;
    }
}
