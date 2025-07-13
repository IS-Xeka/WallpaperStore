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

        public async Task<Guid> Create(User user)
        {
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
            return userEntity.Id;
        }
    }
}
