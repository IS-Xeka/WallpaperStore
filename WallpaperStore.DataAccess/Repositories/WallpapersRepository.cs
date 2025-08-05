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

    public async Task<Result<List<Wallpaper>>> GetAsync()
    {
        try
        {
            var wallpapers = await _context.Wallpapers
                    .AsNoTracking()
                    .ToListAsync();
            return Result.Success(wallpapers.Select(w => w.ToDomain()).ToList());
        }
        catch (Exception ex)
        {
            return Result.Failure<List<Wallpaper>>($"Server error. Can not get wallpapers. {ex.Message}");
        }
    }

    public async Task<Result<List<Wallpaper>>> GetUserWallpapersAsync(Guid userId)
    {
        try
        {
            var wallpapers = await _context.Wallpapers
                    .AsNoTracking()
                    .Where(w => w.OwnerId == userId)
                    .Select(w => w.ToDomain())
                    .ToListAsync();
            return Result.Success(wallpapers);
        }
        catch (Exception ex)
        {
            return Result.Failure<List<Wallpaper>>($"Server error. Can not get wallpapers. {ex.Message}");
        }
    }

    public async Task<Result<Guid>> AddWallpaperAsync(
        Guid userId,
        Wallpaper wallpaper,
        CancellationToken ct = default)
    {
        if (userId == Guid.Empty)
            return Result.Failure<Guid>("UserId can not be empty");
        if (wallpaper == null)
            return Result.Failure<Guid>("Wallpaper can not be empty");

        try
        {
            if (!await _context.Users.AnyAsync(u => u.Id == userId))
                return Result.Failure<Guid>("User not found");
            if (await _context.Wallpapers.AnyAsync(w => w.Url == wallpaper.Url))
                return Result.Failure<Guid>("Wallpaper was added");
            var wallpaperEntity = new WallpaperEntity
            {
                Id = wallpaper.Id,
                Title = wallpaper.Title,
                Description = wallpaper.Description,
                Url = wallpaper.Url,
                Price = wallpaper.Price,
                OwnerId = userId
            };
            await _context.Wallpapers.AddAsync(wallpaperEntity, ct);
            await _context.SaveChangesAsync(ct);
            return Result.Success(wallpaperEntity.Id);
        }
        catch (Exception ex)
        {
            return Result.Failure<Guid>("Internal server error");
        }
    }
}
