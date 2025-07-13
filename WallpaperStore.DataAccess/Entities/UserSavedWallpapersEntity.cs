namespace WallpaperStore.DataAccess.Entities;

public class UserSavedWallpapersEntity
{
    public Guid UserId { get; set; }
    public Guid WallpaperId { get; set;}
    public DateTime SavedDate { get; set; }
    public bool IsFavorite { get; set; } = false;

    public UserEntity UserEntity { get; set; }
    public WallpaperEntity WallpaperEntity { get; set; }
}
