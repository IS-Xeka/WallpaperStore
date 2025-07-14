using CSharpFunctionalExtensions;
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

    public async Task<Result<Guid>> CreateUser(User user)
    {
        try
        {
            var userCreateResult = await _usersRepository.Create(user);
            if (userCreateResult.IsFailure)
                return Result.Failure<Guid>(userCreateResult.Error);
            return Result.Success(userCreateResult.Value);
        }
        catch
        {
            throw new InvalidOperationException("Can't create user");
        }
    }

    public async Task<Result<User>> GetUserById(Guid id)
    {
        try
        {
            var userGetResult = await _usersRepository.GetById(id);
            if (userGetResult.IsFailure)
                return Result.Failure<User>(userGetResult.Error);
            return Result.Success(userGetResult.Value);
        }
        catch
        {
            throw new InvalidOperationException("Can't get user");
        }
    }

    public async Task<Result<User>> GetUserByIdWithWallpapers(Guid id)
    {
        try
        {
            var userGetResult = await _usersRepository.GetByIdWithWallpapers(id);
            if (userGetResult.IsFailure)
                return Result.Failure<User>(userGetResult.Error);
            return Result.Success(userGetResult.Value);
        }
        catch
        {
            throw new InvalidOperationException("Can't get user");
        }
    }

    public async Task<Result<Guid>> UpdateUser(Guid id, string name)
    {
        try
        {
            var userUpdateResult = await _usersRepository.Update(id, name);
            if (userUpdateResult.IsFailure)
                return Result.Failure<Guid>(userUpdateResult.Error);
            return Result.Success(userUpdateResult.Value);
        }
        catch
        {
            throw new InvalidOperationException("Can't update user");
        }
    }
}
