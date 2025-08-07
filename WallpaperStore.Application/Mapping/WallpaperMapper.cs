using WallpaperStore.API.Contracts;
using WallpaperStore.Core.Models;

namespace WallpaperStore.Application.Mapping
{
    public class WallpaperMapper
    {
        public WallpaperDto MapToWallpaperDto(Wallpaper wallpaper)
        {
            return new WallpaperDto(
                wallpaper.Id,
                wallpaper.Title,
                wallpaper.Description,
                wallpaper.Url,
                wallpaper.Price,
                wallpaper.OwnerId);
        }
    }
}
