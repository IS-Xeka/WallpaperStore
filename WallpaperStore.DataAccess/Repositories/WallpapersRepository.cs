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

    public async Task<Result<List<Wallpaper>>> GetUserWallpapers(Guid userId)
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
        catch(Exception ex)
        {
            return Result.Failure<List<Wallpaper>>(ex.Message);
        }
    }

    public async Task<Result<Guid>> AddWallpaper(Guid userId, Wallpaper wallpaper)
    {
        if(userId == Guid.Empty)
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
            await _context.Wallpapers.AddAsync(wallpaperEntity);
            await _context.SaveChangesAsync();
            return Result.Success(wallpaperEntity.Id);
        }
        catch (Exception ex)
        {
            return Result.Failure<Guid>("Internal server error");
        }
    }
}
