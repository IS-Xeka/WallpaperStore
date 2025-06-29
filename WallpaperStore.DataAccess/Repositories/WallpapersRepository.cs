using Microsoft.EntityFrameworkCore;
using WallpaperStore.Core.Models;
using WallpaperStore.DataAccess.Entities;

namespace WallpaperStore.DataAccess.Repositories;

public class WallpapersRepository
{
    private readonly WallpaperStoreDbCOntext _context;
    public WallpapersRepository(WallpaperStoreDbCOntext context)
    {
        _context = context;
    }

    public async Task<List<Wallpaper>> GetWallpapers()
    {
        var wallpaperEntites = await _context.Wallpapers
            .AsNoTracking()
            .ToListAsync();

        var wallpapers = wallpaperEntites
            .Select(w => Wallpaper.Create(w.Id, w.Title, w.Description, w.Url, w.Price).wallpaper).ToList();

        return wallpapers;
    }
}
