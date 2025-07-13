using CSharpFunctionalExtensions;
using Microsoft.EntityFrameworkCore;
using WallpaperStore.Application.Extensions;
using WallpaperStore.Core.Models;
using WallpaperStore.DataAccess.Entities;

namespace WallpaperStore.DataAccess.Repositories;

public class WallpapersRepository : IWallpapersRepository
{
    private readonly WallpaperStoreDbContext _context;
    public WallpapersRepository(WallpaperStoreDbContext context)
    {
        _context = context;
    }

    public async Task<List<Wallpaper>> GetWallpapers()
    {
        var wallpaperEntites = await _context.Wallpapers
            .AsNoTracking()
            .Include(w => w.Owner)
            .ToListAsync();
        var wallpapers = wallpaperEntites
        .Select(w => w.ToDomain()).ToList();
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
            .Include(w => w.Owner)
            .FirstOrDefaultAsync(w => w.Id == id);

        return wallpEntity != null
            ? wallpEntity.ToDomain()
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
        await _context.SaveChangesAsync();
    }

    public async Task<Guid> Create(Wallpaper wallpaper)
    {
        var wallpaperEntity = new WallpaperEntity
        {
            Id = wallpaper.Id,
            Title = wallpaper.Title,
            Description = wallpaper.Description,
            Url = wallpaper.Url,
            Price = wallpaper.Price,
            Owner = new UserEntity
            {
                Id = wallpaper.Owner.Id,
                Name = wallpaper.Owner.Name,
                Email = wallpaper.Owner.Email,
                PasswordHash = wallpaper.Owner.PasswordHash,
                RegisterDate = wallpaper.Owner.RegisterDate,
                IsPublicProfile = wallpaper.Owner.IsPublicProfile
            }
        };

        await _context.Wallpapers.AddAsync(wallpaperEntity);
        await _context.SaveChangesAsync();
        return wallpaperEntity.Id;
    }
}
