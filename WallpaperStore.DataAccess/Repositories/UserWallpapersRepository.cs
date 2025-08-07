using CSharpFunctionalExtensions;
using Microsoft.EntityFrameworkCore;
using WallpaperStore.Application.Extensions;
using WallpaperStore.Core.Models;
using WallpaperStore.DataAccess.Entities;
using WallpaperStore.DataAccess.Extensions;

namespace WallpaperStore.DataAccess.Repositories;

public class UserWallpapersRepository : IUserWallpapersRepository
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

    public async Task<Result<List<UserSavedWallpaper>>> GetAllSavedWallpapersAsync(
        Guid? userId = null,
        Guid? wallpaperId = null,
        bool includeWallpapers = true,
        CancellationToken ct = default)
    {
        try
        {
            var query = _context.UserSavedWallpapers.AsNoTracking();

            if (userId.HasValue)
                query = query.Where(uw => uw.UserId == userId);
            if (wallpaperId.HasValue)
                query = query.Where(uw => uw.WallpaperId == wallpaperId);
            if (includeWallpapers)
                query = query.Include(uw => uw.WallpaperEntity);

            var savedWallpapers = await query
                .Select(uw => includeWallpapers
                    ? uw.ToDomainWithWallpaper()
                    : uw.ToDomain())
                .ToListAsync(ct);
            return Result.Success(savedWallpapers);
        }
        catch (Exception ex)
        {
            return Result.Failure<List<UserSavedWallpaper>>("Server error. Failed to get UserSavedWallpaper");
        }
    }

    public async Task<Result<UserSavedWallpaper>> GetSavedWallpaperAsync(
        Guid userId,
        Guid wallpaperId,
        bool includeWallpaper = false,
        CancellationToken ct = default)
    {
        try
        {
            var query = _context.UserSavedWallpapers.AsNoTracking();
            var savedWallpaper = await query.FirstOrDefaultAsync(uw => uw.WallpaperId == wallpaperId && uw.UserId == userId, ct);
            if (includeWallpaper)
                query = query.Include(uw => uw.WallpaperEntity);

            if (savedWallpaper == null)
                return Result.Failure<UserSavedWallpaper>("Saved wallpaper not found");

            if (includeWallpaper)
                return Result.Success(savedWallpaper.ToDomainWithWallpaper());
            return Result.Success(savedWallpaper.ToDomain());
        }
        catch (Exception ex)
        {
            return Result.Failure<UserSavedWallpaper>("Server error. Failed to get UserSavedWallpaper");
        }
    }
}
