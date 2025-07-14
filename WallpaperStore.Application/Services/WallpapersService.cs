using CSharpFunctionalExtensions;
using WallpaperStore.Core.Models;
using WallpaperStore.DataAccess.Repositories;

namespace WallpaperStore.Application.Services;

public class WallpapersService : IWallpapersService
{
    private readonly IWallpapersRepository _wallpaperRepository;
    public WallpapersService(IWallpapersRepository wallpapersRepository)
    {
        _wallpaperRepository = wallpapersRepository;
    }

    public async Task<Result<Guid>> UpdateWallpaper(Guid id, string title, string description)
    {
        try
        {
            var updateResult = await _wallpaperRepository.Update(id, title, description);
            if (updateResult.IsFailure)
                return Result.Failure<Guid>(updateResult.Error);

            return Result.Success(updateResult.Value);
        }
        catch (Exception ex)
        {
            throw new ArgumentNullException(ex.Message);
        }
    }

    public async Task<Result<Guid>> DeleteWallpaper(Guid id)
    {
        try
        {
            var deleteResult = await _wallpaperRepository.DeleteWallpaper(id);
            if(deleteResult.IsFailure)
                return Result.Failure<Guid>(deleteResult.Error);
            return Result.Success(deleteResult.Value);
        }
        catch (Exception ex)
        {
            throw new InvalidOperationException(ex.Message);
        }
    }
    
    public async Task<Result<Guid>> CreateWallpaper(Wallpaper wallpaper)
    {
        try
        {
            var createResult = await _wallpaperRepository.Create(wallpaper);
            if (createResult.IsFailure)
                return Result.Failure<Guid>(createResult.Error);
            return Result.Success(createResult.Value);
        }
        catch (Exception ex)
        {
            throw new InvalidOperationException(ex.Message);
        }
    }
    public async Task<Result<List<Wallpaper>>> GetWallpapers()
    {
        try
        {
            var getResult = await _wallpaperRepository.GetWallpapers();
            if (getResult.IsFailure)
                return Result.Failure<List<Wallpaper>>(getResult.Error);
            return Result.Success(getResult.Value);
        }
        catch (Exception ex)
        {
            throw new InvalidOperationException(ex.Message);
        }
    }

    public async Task<Result<Wallpaper>> GetById(Guid id)
    {
        try
        {
            var getByIdResult = await _wallpaperRepository.GetById(id);
            if (getByIdResult.IsFailure)
                return Result.Failure<Wallpaper>(getByIdResult.Error);
            return Result.Success(getByIdResult.Value);
        }
        catch (Exception ex)
        {
            throw new InvalidOperationException(ex.Message);
        }
    }

    public async Task<Result<Wallpaper>> GetByIdWithOwner(Guid id)
    {
        try
        {
            var getByIdWithOwnerResult = await _wallpaperRepository.GetByIdWithOwner(id);
            if (getByIdWithOwnerResult.IsFailure)
                return Result.Failure<Wallpaper>(getByIdWithOwnerResult.Error);
            return Result.Success(getByIdWithOwnerResult.Value);
        }
        catch (Exception ex)
        {
            throw new InvalidOperationException(ex.Message);
        }
    }

    public async Task<Result<List<Wallpaper>>> GetWallpapersWithOwners()
    {
        try
        {
            var getWithOwnersResult = await _wallpaperRepository.GetWallpapersWithOwners();
            if (getWithOwnersResult.IsFailure)
                return Result.Failure<List<Wallpaper>>(getWithOwnersResult.Error);
            return Result.Success(getWithOwnersResult.Value);
        }
        catch (Exception ex)
        {
            throw new InvalidOperationException(ex.Message);
        }
    }
}
