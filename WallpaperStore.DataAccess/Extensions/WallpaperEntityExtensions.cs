using WallpaperStore.Core.Models;
using WallpaperStore.DataAccess.Entities;

namespace WallpaperStore.Application.Extensions;

public static class WallpaperEntityExtensions
{
    public static Wallpaper ToDomain(this WallpaperEntity entity)
    {
        return Wallpaper.Create(
            entity.Id,
            entity.Title,
            entity.Description,
            entity.Url,
            entity.Price,
            entity.OwnerId);
    }
}