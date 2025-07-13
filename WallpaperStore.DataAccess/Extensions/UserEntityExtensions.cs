using WallpaperStore.Core.Models;
using WallpaperStore.DataAccess.Entities;

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
}
