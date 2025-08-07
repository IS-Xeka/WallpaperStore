using WallpaperStore.API.Contracts;
using WallpaperStore.Core.Models;

namespace WallpaperStore.Application.Mapping;

public static class WallpaperExtensions
{
    public static WallpaperDto ToDto(this Wallpaper wallpaper)
    {
        return new WallpaperDto(
            wallpaper.Id,
            wallpaper.Title,
            wallpaper.Description,
            wallpaper.Url,
            wallpaper.Price,
            wallpaper.OwnerId);
    }

    /*    Guid Id,
            string Title,
            string Description,
            string Url,
            decimal Price,
            Guid OwenerId*/
}
