using CSharpFunctionalExtensions;
using WallpaperStore.Core.Models;
using WallpaperStore.DataAccess.Repositories;

namespace WallpaperStore.Application.Services;

public class WallpapersService: IWallpapersService
{
    private readonly IWallpapersRepository _wallpaperRepository;
    private readonly IUserWallpapersRepository _userWallpapersRepository;
    public WallpapersService(
        IWallpapersRepository wallpapersRepository,
        IUserWallpapersRepository userWallpapersRepository)
    {
        _wallpaperRepository = wallpapersRepository;
        _userWallpapersRepository = userWallpapersRepository;
    }

    public async Task<Result<Guid>> AddWallpaperAsync(Guid userId, Wallpaper wallpaper, CancellationToken ct = default)
    {
        try
        {
            var result = await _wallpaperRepository.AddWallpaperAsync(userId, wallpaper, ct);
            if (result.IsFailure)
                return Result.Failure<Guid>(result.Error);
            return Result.Success(result.Value);
        }
        catch (OperationCanceledException)
        {
            return Result.Failure<Guid>("Operation canceled");
        }
        catch (Exception ex)
        {
            return Result.Failure<Guid>($"Internal service error. {ex.Message}");
        }
    }

    public async Task<Result<List<Wallpaper>>> GetAsync()
    {
        try
        {
            var result = await _wallpaperRepository.GetAsync();
            if (result.IsFailure)
                return Result.Failure<List<Wallpaper>>(result.Error);
            return Result.Success(result.Value); 
        }
        catch (Exception ex)
        {
            return Result.Failure<List<Wallpaper>>($"Internal service error. {ex.Message}");
        }
    }

    public async Task<Result<List<Wallpaper>>> GetUserWallpapersAsync(Guid userId)
    {
        try
        {
            var result = await _wallpaperRepository.GetUserWallpapersAsync(userId);
            if (result.IsFailure)
                return Result.Failure<List<Wallpaper>>(result.Error);
            return Result.Success(result.Value); 
        }
        catch (Exception ex)
        {
            return Result.Failure<List<Wallpaper>>($"Internal service error. {ex.Message}");
        }
    }
    public async Task<Result<List<UserSavedWallpaper>>> GetAllSavedWallpapersAsync(Guid? userId = null, Guid? wallpaperId = null, bool includeWallpapers = true, CancellationToken ct = default)
    {
        try
        {
            var result = await _userWallpapersRepository.GetAllSavedWallpapersAsync(userId, wallpaperId, includeWallpapers, ct);
            if (result.IsFailure)
                return Result.Failure<List<UserSavedWallpaper>>(result.Error);
            return Result.Success(result.Value);
        }
        catch (OperationCanceledException)
        {
            return Result.Failure<List<UserSavedWallpaper>>($"Operation canceled");
        }
        catch (Exception ex)
        {
            return Result.Failure<List<UserSavedWallpaper>>($"Internal service error. {ex}");
        }
    }

    public async Task<Result<UserSavedWallpaper>> GetSavedWallpaperAsync(Guid userId, Guid wallpaperId, bool includeWallpaper = false, CancellationToken ct = default)
    {
        try
        {
            var result = await _userWallpapersRepository.GetSavedWallpaperAsync(userId, wallpaperId, includeWallpaper, ct);
            if (result.IsFailure)
                return Result.Failure<UserSavedWallpaper>(result.Error);
            return Result.Success(result.Value);
        }
        catch (OperationCanceledException)
        {
            return Result.Failure<UserSavedWallpaper>($"Operation canceled");
        }
        catch (Exception ex)
        {
            return Result.Failure<UserSavedWallpaper>($"Internal service error. {ex}");
        }
    }

    public async Task<Result> SaveWallpaperAsync(Guid userId, Guid wallpaperId, bool isFavorite = false, CancellationToken ct = default)
    {
        try
        {
            var result = await _userWallpapersRepository.SaveWallpaperAsync(userId, wallpaperId, isFavorite, ct);
            if (result.IsFailure)
                return Result.Failure<UserSavedWallpaper>(result.Error);
            return Result.Success(result);
        }
        catch (OperationCanceledException)
        {
            return Result.Failure($"Operation canceled");
        }
        catch (Exception ex)
        {
            return Result.Failure($"Internal service error. {ex}");
        }
    }

    public async Task<Result> UnsaveWallpaperAsync(Guid userId, Guid wallpaperId, bool isFavorite = false, CancellationToken ct = default)
    {
        try
        {
            var result = await _userWallpapersRepository.UnsaveWallpaperAsync(userId, wallpaperId, isFavorite, ct);
            if (result.IsFailure)
                return Result.Failure<UserSavedWallpaper>(result.Error);
            return Result.Success(result);
        }
        catch (OperationCanceledException)
        {
            return Result.Failure($"Operation canceled");
        }
        catch (Exception ex)
        {
            return Result.Failure($"Internal service error. {ex}");
        }
    }
}
