namespace WallpaperStore.Core.Models;

public class UserSavedWallpaper
{
    public Guid UserId { get; }
    public Guid WallpaperId { get; }
    public DateTime? SavedDate { get; } = DateTime.UtcNow;
    public bool IsFavorite { get; private set; } = false;

    public User User { get; } = null!;
    public Wallpaper Wallpaper { get; } = null!;

    private UserSavedWallpaper(
        User user, 
        Wallpaper wallpaper,
        bool isFavorite)
    {
        User = user;
        Wallpaper = wallpaper;
        IsFavorite = isFavorite;
        UserId = user.Id;
        WallpaperId = wallpaper.Id;
    }

    public static UserSavedWallpaper Create(
        User user,
        Wallpaper wallpaper,
        bool isFavorite)
    {
        if (user == null)
            throw new ArgumentNullException($"User can not be null {nameof(user)}");
        if (wallpaper == null)
            throw new ArgumentNullException($"Wallpaper can not be null {nameof(wallpaper)}");

        return new UserSavedWallpaper(user, wallpaper, isFavorite);
    }

    public void ToggleFavorite()
    {
        IsFavorite = !IsFavorite;
    }
}
