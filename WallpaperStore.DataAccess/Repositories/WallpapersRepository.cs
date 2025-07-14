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

    public async Task<Result<List<Wallpaper>>> GetWallpapers()
    {
        var wallpaperEntites = await _context.Wallpapers
            .AsNoTracking()
            .ToListAsync();
        if (!wallpaperEntites.Any())
            return Result.Failure<List<Wallpaper>>("Wallpapers not found");
        var wallpapers = wallpaperEntites
        .Select(w => w.ToDomain()).ToList();
        return Result.Success(wallpapers);
    }

    public async Task<Result<List<Wallpaper>>> GetWallpapersWithOwners()
    {
        var wallpaperEntites = await _context.Wallpapers
            .AsNoTracking()
            .Include(w => w.Owner)
            .ToListAsync();
        if (!wallpaperEntites.Any())
            return Result.Failure<List<Wallpaper>>("Wallpapers not found");
        var wallpapers = wallpaperEntites
        .Select(w => w.ToDomain()).ToList();
        return Result.Success(wallpapers);
    }

    public async Task<Result<Guid>> DeleteWallpaper(Guid id)
    {
        var wallpaper = await _context.Wallpapers.FirstOrDefaultAsync(w => w.Id == id);
        if (wallpaper == null)
            return Result.Failure<Guid>("Wallpaper not found");

        _context.Wallpapers.Remove(wallpaper);
        await _context.SaveChangesAsync();
        return Result.Success(wallpaper.Id);
    }

    public async Task<Result<Wallpaper>> GetById(Guid id)
    {
        var wallpaperEntity = await _context.Wallpapers
            .AsNoTracking()
            .FirstOrDefaultAsync(w => w.Id == id);

        if (wallpaperEntity == null)
            return Result.Failure<Wallpaper>("Wallpaper not found");

        return Result.Success(wallpaperEntity.ToDomain());
    }

    public async Task<Result<Wallpaper>> GetByIdWithOwner(Guid id)
    {
        var wallpaperEntity = await _context.Wallpapers
            .AsNoTracking()
            .Include(w => w.Owner)
            .FirstOrDefaultAsync(w => w.Id == id);

        if (wallpaperEntity == null)
            return Result.Failure<Wallpaper>("Wallpaper not found");

        return Result.Success(wallpaperEntity.ToDomain());
    }

    public async Task<Result> Update(Guid id, string title, string description)
    {
        if (!await _context.Wallpapers.AnyAsync(w => w.Id == id))
            return Result.Failure("Not found wallpaper");
        await _context.Wallpapers
            .Where(w => w.Id == id)
            .ExecuteUpdateAsync(wall => wall
                .SetProperty(w => w.Title, title)
                .SetProperty(w => w.Description, description));
        await _context.SaveChangesAsync();
        return Result.Success();
    }

    public async Task<Result<Guid>> Create(Wallpaper wallpaper)
    {
        if (wallpaper == null)
            return Result.Failure<Guid>("Wallpaper can not be null");

        var wallpaperEntity = new WallpaperEntity
        {
            Id = wallpaper.Id,
            Title = wallpaper.Title,
            Description = wallpaper.Description,
            Url = wallpaper.Url,
            Price = wallpaper.Price,
            OwnerId = wallpaper.OwnerId
        };

        await _context.Wallpapers.AddAsync(wallpaperEntity);
        await _context.SaveChangesAsync();
        return Result.Success(wallpaperEntity.Id);
    }
}
