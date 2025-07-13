using CSharpFunctionalExtensions;
using Microsoft.EntityFrameworkCore;
using WallpaperStore.Application.Extensions;
using WallpaperStore.Core.Models;
using WallpaperStore.DataAccess.Entities;

namespace WallpaperStore.DataAccess.Repositories
{
    public class UsersRepository
    {
        private readonly WallpaperStoreDbContext _context;

        public UsersRepository(WallpaperStoreDbContext context)
        {
            _context = context;
        }

        public async Task<Result<User>> GetByIdWithWallpapers(Guid id)
        {
            var userEntity = await _context.Users
                .AsNoTracking()
                .Include(u => u.AddedWallpapers)
                .Include(u => u.SavedWallpapers)
                    .ThenInclude(sw => sw.WallpaperEntity)
                .FirstOrDefaultAsync(u => u.Id == id);

            if (userEntity == null)
                return Result.Failure<User>("User not found");

            return userEntity.ToDomainWithWallpapers();
        }


        public async Task<Result<Guid>> Create(User user)
        {
            if (user == null)
                return Result.Failure<Guid>("User can not be null");
            var userEntity = new UserEntity{
                Id = user.Id,
                Name = user.Name,
                Email = user.Email,
                PasswordHash = user.PasswordHash,
                RegisterDate = user.RegisterDate,
                IsPublicProfile = user.IsPublicProfile
            };

            await _context.AddAsync(userEntity);
            await _context.SaveChangesAsync();
            return Result.Success(userEntity.Id);
        }
    }
}
