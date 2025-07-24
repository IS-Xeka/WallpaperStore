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

        public async Task<Result<Guid>> Update(Guid id, string name)
        {
            if (!await _context.Users.AnyAsync(u => u.Id == id))
                return Result.Failure<Guid>("User not found");
            await _context.Users
                .Where(u => u.Id == id)
                .ExecuteUpdateAsync(user => user
                    .SetProperty(u => u.Name, name));
            await _context.SaveChangesAsync();
            return Result.Success(id);
        }

        public async Task<Result<User>> GetById(Guid id)
        {
            var userEntity = await _context.Users
                .AsNoTracking()
                .FirstOrDefaultAsync(u => u.Id == id);

            if (userEntity == null)
                return Result.Failure<User>("User not found");

            return Result.Success(userEntity.ToDomain());
        }
/*        public async Task<Result<User>> GetByIdWithWallpapers(Guid id)
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
        }*/

        public async Task<Result<Guid>> Create(User user)
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

            await _context.AddAsync(userEntity);
            await _context.SaveChangesAsync();
            return Result.Success(userEntity.Id);
        }

        public async Task<Result<List<User>>> Get()
        {
            var userEntites = await _context.Users
                .AsNoTracking()
                .ToListAsync();

            if (userEntites == null)
                return Result.Failure<List<User>>("Users not found");
            var users = userEntites.Select(u => u.ToDomain()).ToList();
            return Result.Success(users);
        }

/*        public async Task<Result<List<User>>> GetWithWallpapers()
        {
            var userEntites = await _context.Users
                    .AsNoTracking()
                    .Include(u => u.AddedWallpapers)
                    .Include(u => u.SavedWallpapers)
                        .ThenInclude(us => us.WallpaperEntity)
                    .ToListAsync();

            if (!userEntites.Any())
                return Result.Failure<List<User>>("Users not found");
            var users = userEntites.Select(u => u.ToDomainWithWallpapers()).ToList();
            return Result.Success(users);
        }*/

/*        public async Task<Result<Guid>> SaveWallpaper(Guid userId, Guid wallpaperId, bool isFavorite)
        {
            var userEntity = await _context.Users
                .Include(u => u.SavedWallpapers)
                    .ThenInclude(w => w.WallpaperEntity)
                .FirstOrDefaultAsync(u => u.Id == userId);

            if (userEntity == null)
                return Result.Failure<Guid>("User not found");
            if (userEntity.SavedWallpapers.Any(w => w.WallpaperId == wallpaperId))
                return Result.Failure<Guid>("Wallpaper was saved");

            userEntity.SavedWallpapers.Add(new UserSavedWallpapersEntity
            {
                UserId = userId,
                WallpaperId = wallpaperId,
                IsFavorite = isFavorite,
                SavedDate = DateTime.UtcNow
            });

            await _context.SaveChangesAsync();
            return Result.Success(wallpaperId);

        }*/

/*        public async Task<Result<Guid>> AddWallpaper(Guid userId, Wallpaper wallpaper)
        {
            if (wallpaper == null)
                return Result.Failure<Guid>("Wallpaper can't be null");

            var wallpaperEntity = new WallpaperEntity
            {
                Id = wallpaper.Id,
                Title = wallpaper.Title,
                Description = wallpaper.Description,
                Url = wallpaper.Url,
                Price = wallpaper.Price,
                OwnerId = userId
            };

            var userEntity = await _context.Users
                .Include(u => u.AddedWallpapers)
                .FirstOrDefaultAsync(u => u.Id == userId);

            if (userEntity == null)
                return Result.Failure<Guid>("User not found");
            if (userEntity.AddedWallpapers.Any(w => w.Id == wallpaper.Id))
                return Result.Failure<Guid>("Wallpaper was added");
            if (userEntity.AddedWallpapers.Any(w => w.Url == wallpaper.Url))
                return Result.Failure<Guid>("Wallpaper already exist");

            await _context.Wallpapers.AddAsync(wallpaperEntity);
            userEntity.AddedWallpapers.Add(wallpaperEntity);
            await _context.SaveChangesAsync();
            return Result.Success(wallpaper.Id);
        }*/
    }
}
