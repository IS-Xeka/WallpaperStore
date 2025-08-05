using CSharpFunctionalExtensions;
using System.Threading;
using WallpaperStore.Core.Models;
using WallpaperStore.DataAccess.Repositories;

namespace WallpaperStore.Application.Services;

public class UserService : IUserService
{
    private readonly IUsersRepository _usersRepository;
    private readonly IUserWallpapersRepository _userWallpapersRepository;
    public UserService(
        IUsersRepository usersRepository,
        IUserWallpapersRepository userWallpapersRepository)
    {
        _usersRepository = usersRepository;
        _userWallpapersRepository = userWallpapersRepository;
    }

    public async Task<Result<Guid>> CreateAsync(User user, CancellationToken ct = default)
    {
        try
        {
            var result = await _usersRepository.CreateAsync(user, ct);
            if (result.IsFailure)
                return Result.Failure<Guid>(result.Error);
            return Result.Success(result.Value);
        }
        catch(OperationCanceledException)
        {
            return Result.Failure<Guid>($"Operation canceled");
        }
        catch (Exception ex)
        {
            return Result.Failure<Guid>($"Internal service error. {ex.Message}");
        }
    }

    public async Task<Result> DeleteAsync(Guid id, CancellationToken ct = default)
    {
        try
        {
            var result = await _usersRepository.DeleteAsync(id, ct);
            if (result.IsFailure)
                return Result.Failure<Guid>(result.Error);
            return Result.Success();
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
    public async Task<Result<List<User>>> GetAsync()
    {
        try
        {
            var result = await _usersRepository.GetAsync();
            if (result.IsFailure)
                return Result.Failure<List<User>>(result.Error);
            return Result.Success(result.Value);
        }
        catch (Exception ex)
        {
            return Result.Failure<List<User>>($"Internal service error. {ex}");
        }
    }

    public async Task<Result<User>> GetByIdAsync(Guid id)
    {
        try
        {
            var result = await _usersRepository.GetByIdAsync(id);
            if (result.IsFailure)
                return Result.Failure<User>(result.Error);
            return Result.Success(result.Value);
        }
        catch (Exception ex)
        {
            return Result.Failure<User>($"Internal service error. {ex}");
        }
    }
    public async Task<Result<Guid>> UpdateAsync(Guid id, string name, CancellationToken ct = default)
    {
        try
        {
            var result = await _usersRepository.UpdateAsync(id, name, ct);
            if (result.IsFailure)
                return Result.Failure<Guid>(result.Error);
            return Result.Success(result.Value);
        }
        catch (OperationCanceledException)
        {
            return Result.Failure<Guid>($"Operation canceled");
        }
        catch (Exception ex)
        {
            return Result.Failure<Guid>($"Internal service error. {ex}");
        }
    }

    public async Task<Result<List<UserSavedWallpaper>>> GetAllSavedWallpapersAsync(Guid? userId = null, Guid? wallpaperId = null, bool includeWallpapers = false, CancellationToken ct = default)
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
