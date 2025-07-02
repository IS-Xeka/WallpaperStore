using Microsoft.EntityFrameworkCore;
using WallpaperStore.Core.Models;

namespace WallpaperStore.DataAccess.Repositories;

public class WallpapersRepository : IWallpapersRepository
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

    public async Task<Guid> DeleteWallpaper(Guid id)
    {
        var wallpaper = await _context.Wallpapers.FirstOrDefaultAsync(w => w.Id == id);
        if (wallpaper != null)
        {
            _context.Wallpapers.Remove(wallpaper);
            await _context.SaveChangesAsync();
        }
        return wallpaper.Id;
    }

    public async Task<Wallpaper?> GetById(Guid id)
    {
        var wallpEntity = await _context.Wallpapers
            .AsNoTracking()
            .FirstOrDefaultAsync(w => w.Id == id);

        return wallpEntity != null
            ? Wallpaper.Create(
                wallpEntity.Id,
                wallpEntity.Title,
                wallpEntity.Description,
                wallpEntity.Url,
                wallpEntity.Price
                ).wallpaper
            : null;
    }

    //public async Task Update(Guid id, string title, string description)
    //{
    //    var wallpaper = await _context.Wallpapers.FirstOrDefaultAsync(w => w.Id == id);

    //    if(wallpaper != null)
    //    {
    //        wallpaper.Title = title;
    //        wallpaper.Description = description;

    //        await _context.SaveChangesAsync();
    //    }
    //}

    public async Task Update(Guid id, string title, string description)
    {
        await _context.Wallpapers
            .Where(w => w.Id == id)
            .ExecuteUpdateAsync(wall => wall
                .SetProperty(w => w.Title, title)
                .SetProperty(w => w.Description, description));
    }
}
