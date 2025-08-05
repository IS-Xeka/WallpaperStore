using CSharpFunctionalExtensions;
using Microsoft.EntityFrameworkCore;
using WallpaperStore.Application.Extensions;
using WallpaperStore.Core.Models;
using WallpaperStore.DataAccess.Entities;

namespace WallpaperStore.DataAccess.Repositories
{
    public class UsersRepository : IUsersRepository
    {
        private readonly WallpaperStoreDbContext _context;

        public UsersRepository(WallpaperStoreDbContext context)
        {
            _context = context;
        }
        public async Task<Result<List<User>>> GetAsync()
        {
            try
            {
                var userEntites = await _context.Users
                        .AsNoTracking()
                        .ToListAsync();

                if (userEntites == null)
                    return Result.Failure<List<User>>("Users not found");
                var users = userEntites.Select(u => u.ToDomain()).ToList();
                return Result.Success(users);
            }
            catch (Exception ex)
            {
                return Result.Failure<List<User>>($"Server error. Can not get users. {ex.Message}");
            }
        }
        public async Task<Result<User>> GetByIdAsync(Guid id)
        {
            try
            {
                var userEntity = await _context.Users
                        .AsNoTracking()
                        .FirstOrDefaultAsync(u => u.Id == id);

                if (userEntity == null)
                    return Result.Failure<User>("User not found");

                return Result.Success(userEntity.ToDomain());
            }
            catch (Exception ex)
            {
                return Result.Failure<User>($"Server error. Can not get user. {ex.Message}");
            }
        }
        public async Task<Result<Guid>> CreateAsync(User user, CancellationToken ct = default)
        {
            try
            {
                if (user == null)
                    return Result.Failure<Guid>("User can not be null");
                var userEntity = new UserEntity
                {
                    Id = user.Id,
                    Name = user.Name,
                    Email = user.Email,
                    PasswordHash = user.PasswordHash,
                    RegisterDate = user.RegisterDate,
                    IsPublicProfile = user.IsPublicProfile
                };

                await _context.AddAsync(userEntity, ct);
                await _context.SaveChangesAsync(ct);
                return Result.Success(userEntity.Id);
            }
            catch (Exception ex)
            {
                return Result.Failure<Guid>($"Server error. Can not create users. {ex.Message}");
            }
        }
        public async Task<Result<Guid>> UpdateAsync(Guid id, string name, CancellationToken ct = default)
        {
            try
            {
                if (!await _context.Users.AnyAsync(u => u.Id == id))
                    return Result.Failure<Guid>("User not found");
                await _context.Users
                    .Where(u => u.Id == id)
                    .ExecuteUpdateAsync(user => user
                        .SetProperty(u => u.Name, name));
                await _context.SaveChangesAsync(ct);
                return Result.Success(id);
            }
            catch (Exception ex)
            {
                return Result.Failure<Guid>($"Server error. Can not update users. {ex.Message}");
            }
        }

        public async Task<Result> DeleteAsync(Guid id, CancellationToken ct = default)
        {
            try
            {
                if (!await _context.Users.AnyAsync(u => u.Id == id, ct))
                    return Result.Failure("User not found");

                var userEntity = new UserEntity { Id = id };
                _context.Attach(userEntity);
                _context.Users.Remove(userEntity);
                await _context.SaveChangesAsync(ct);
                return Result.Success();
            }
            catch (Exception ex)
            {

                return Result.Failure<Guid>($"Server error. Can not delete users. {ex.Message}");
            }
        }
    }
}
