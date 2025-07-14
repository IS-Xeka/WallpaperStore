using CSharpFunctionalExtensions;
using System.Collections.Generic;
using WallpaperStore.Core.Models;
using WallpaperStore.DataAccess.Repositories;

namespace WallpaperStore.Application.Services;

public class UserService : IUserService
{
    private readonly IUsersRepository _usersRepository;
    public UserService(IUsersRepository usersRepository)
    {
        _usersRepository = usersRepository;
    }

    public async Task<Result<Guid>> AddWallpaper(Guid userId, Wallpaper wallpaper)
    {
        try
        {
            var addResult = await _usersRepository.AddWallpaper(userId, wallpaper);
            if (addResult.IsFailure)
                return Result.Failure<Guid>(addResult.Error);
            return Result.Success(addResult.Value);
        }
        catch (Exception ex)
        {
            throw new InvalidOperationException(ex.Message);
        }
    }

    public async Task<Result<Guid>> CreateUser(User user)
    {
        try
        {
            var userCreateResult = await _usersRepository.Create(user);
            if (userCreateResult.IsFailure)
                return Result.Failure<Guid>(userCreateResult.Error);
            return Result.Success(userCreateResult.Value);
        }
        catch(Exception ex)
        {
            throw new InvalidOperationException(ex.Message);
        }
    }

    public async Task<Result<List<User>>> GetAll()
    {
        try
        {
            var getResult = await _usersRepository.Get();
            if (getResult.IsFailure)
                return Result.Failure<List<User>>(getResult.Error);
            return Result.Success(getResult.Value);
        }
        catch(Exception ex)
        {
            throw new InvalidOperationException(ex.Message);
        }
    }

    public async Task<Result<List<User>>> GetAllWithWallpapers()
    {
        try
        {
            var getAllResult = await _usersRepository.GetWithWallpapers();
            if (getAllResult.IsFailure)
                return Result.Failure<List<User>>(getAllResult.Error);
            return Result.Success(getAllResult.Value);
        }
        catch (Exception ex)
        {
            throw new InvalidOperationException(ex.Message);
        }
    }

    public async Task<Result<User>> GetUserById(Guid id)
    {
        try
        {
            var getByIdResult = await _usersRepository.GetById(id);
            if (getByIdResult.IsFailure)
                return Result.Failure<User>(getByIdResult.Error);
            return Result.Success(getByIdResult.Value);
        }
        catch(Exception ex)
        {
            throw new InvalidOperationException(ex.Message);
        }
    }

    public async Task<Result<User>> GetUserByIdWithWallpapers(Guid id)
    {
        try
        {
            var getByIdWithWallpapersResult = await _usersRepository.GetByIdWithWallpapers(id);
            if (getByIdWithWallpapersResult.IsFailure)
                return Result.Failure<User>(getByIdWithWallpapersResult.Error);
            return Result.Success(getByIdWithWallpapersResult.Value);
        }
        catch(Exception ex)
        {
            throw new InvalidOperationException(ex.Message);
        }
    }

    public async Task<Result<Guid>> SaveWallpaper(Guid userId, Guid wallaperId, bool isFavorite)
    {
        try
        {
            var saveWallpaperResult = await _usersRepository.SaveWallpaper(userId, wallaperId, isFavorite);
            if (saveWallpaperResult.IsFailure)
                return Result.Failure<Guid>(saveWallpaperResult.Error);
            return Result.Success(saveWallpaperResult.Value);
        }
        catch (Exception ex)
        {
            throw new InvalidOperationException(ex.Message);
        }
    }

    public async Task<Result<Guid>> UpdateUser(Guid id, string name)
    {
        try
        {
            var updateResult = await _usersRepository.Update(id, name);
            if (updateResult.IsFailure)
                return Result.Failure<Guid>(updateResult.Error);
            return Result.Success(updateResult.Value);
        }
        catch(Exception ex)
        {
            throw new InvalidOperationException(ex.Message);
        }
    }
}
