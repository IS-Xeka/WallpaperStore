using CSharpFunctionalExtensions;
using Microsoft.EntityFrameworkCore;
using WallpaperStore.Application.Extensions;
using WallpaperStore.Core.Models;
using WallpaperStore.DataAccess.Entities;
using WallpaperStore.DataAccess.Extensions;

namespace WallpaperStore.DataAccess.Repositories;

public class UserWallpapersRepository
{
    private readonly WallpaperStoreDbContext _context;
    public UserWallpapersRepository(WallpaperStoreDbContext context) 
    {
        _context = context;
    }

    public async Task<Result> SaveWallpaperAsync(Guid userId, Guid wallpaperId, bool isFavorite = false, CancellationToken ct = default)
    {
        try
        {
            var exists = await _context.UserSavedWallpapers
                .AsNoTracking()
                .AnyAsync(uw => uw.UserId == userId && uw.WallpaperId == wallpaperId, ct);

            if (exists)
                return Result.Failure("Wallpaper was saved");
            
            var userSavedWallpaperEntity = new UserSavedWallpapersEntity
            {
                UserId = userId,
                WallpaperId = wallpaperId,
                IsFavorite = isFavorite,
                SavedDate = DateTime.UtcNow
            };
            await _context.UserSavedWallpapers.AddAsync(userSavedWallpaperEntity, ct);
            await _context.SaveChangesAsync(ct);
            return Result.Success();
        }
        catch (Exception ex)
        {
            return Result.Failure("Internal server error. Failed to save wallpaper");
        }
    }

    public async Task<Result> UnsaveWallpaperAsync(Guid userId, Guid wallpaperId, bool isFavorite = false, CancellationToken ct = default)
    {
        try
        {
            var userSavedWallpaperEntity = await _context.UserSavedWallpapers
                .Where(uw => uw.WallpaperId == wallpaperId && uw.UserId == userId)
                .FirstOrDefaultAsync(ct);

            if (userSavedWallpaperEntity == null)
                return Result.Failure("Wallpaper not found in saved collection");

            _context.UserSavedWallpapers.Remove(userSavedWallpaperEntity);
            await _context.SaveChangesAsync(ct);
            return Result.Success();
        }
        catch (Exception ex)
        {
            return Result.Failure("Server error. Failed to unsave wallpaper");
        }
    }

    public async Task<Result<List<UserSavedWallpaper>>> GetByUserIdAsync(Guid userId, CancellationToken ct = default)
    {
        try
        {
            var savedWallpapers = await _context.UserSavedWallpapers
                    .AsNoTracking()
                    .Where(uw => uw.UserId == userId)
                    .Include(uw => uw.WallpaperEntity)
                    .Select(uw => uw.ToDomain())
                    .ToListAsync(ct);

            return Result.Success(savedWallpapers);
        }
        catch (Exception ex)
        {
            return Result.Failure<List<UserSavedWallpaper>>("Server error. Failed to get UserSavedWallpaper");
        }
    }
}
