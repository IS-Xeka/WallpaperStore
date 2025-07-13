using WallpaperStore.Core.Models;

namespace WallpaperStore.DataAccess.Entities;

public class UserEntity
{
    public Guid Id { get; set; }
    public string Name { get; set; }
    public Email Email { get; set; }
    public string PasswordHash { get; set; }
    public DateTime RegisterDate { get; set; }
    public DateTime? LastTimeOnline { get; set; }
    public bool IsPublicProfile { get; set; } = true;
    public ICollection<WallpaperEntity> AddedWallpapers { get; set; } = new List<WallpaperEntity>();
    public ICollection<UserSavedWallpapersEntity> SavedWallpapers { get; set; } = new List<UserSavedWallpapersEntity>();
}

