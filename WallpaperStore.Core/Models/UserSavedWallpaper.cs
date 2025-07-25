using CSharpFunctionalExtensions;

namespace WallpaperStore.Core.Models;

public class UserSavedWallpaper
{
    public Guid UserId { get; }
    public Guid WallpaperId { get; }
    public DateTime SavedDate { get; }
    public bool IsFavorite { get; private set; } = false;
    public User? User { get; private set; }
    public Wallpaper? Wallpaper { get; private set; }

    private UserSavedWallpaper(
        Guid userId,
        Guid wallpaperId,
        bool isFavorite,
        DateTime savedDate,
        User? user = null,
        Wallpaper? wallpaper = null)
    {
        UserId = userId;
        WallpaperId = wallpaperId;
        IsFavorite = isFavorite;
        SavedDate = savedDate;
        User = user;
        Wallpaper = wallpaper;
    }

    public static Result<UserSavedWallpaper> Create(
        User user,
        Wallpaper wallpaper,
        DateTime savedDate,
        bool isFavorite = false)
    {
        if (user == null)
            return Result.Failure<UserSavedWallpaper>($"User can not be null {nameof(user)}");
        if (wallpaper == null)
            return Result.Failure<UserSavedWallpaper>($"Wallpaper can not be null {nameof(wallpaper)}");

        return Result.Success(new UserSavedWallpaper(user.Id, wallpaper.Id, isFavorite, savedDate, user, wallpaper));
    }    
    public static Result<UserSavedWallpaper> Create(
        Guid userId,
        Guid wallpaperId,
        DateTime savedDate,
        bool isFavorite = false)
    {
        if (userId == Guid.Empty)
            return Result.Failure<UserSavedWallpaper>($"UserId can not be null {nameof(userId)}");
        if (wallpaperId == Guid.Empty)
            return Result.Failure<UserSavedWallpaper>($"WallpaperId can not be null {nameof(wallpaperId)}");

        return Result.Success(new UserSavedWallpaper(userId, wallpaperId, isFavorite, savedDate));
    }

    public void ToggleFavorite()
    {
        IsFavorite = !IsFavorite;
    }
    public void AttachUser(User user)
    {
        if (user.Id != UserId)
            throw new ArgumentException("User ID mismatch");

        User = user;
    }
    public void AttachWallpaper(Wallpaper wallpaper)
    {
        if (wallpaper.Id != WallpaperId)
            throw new ArgumentException("Wallpaper ID mismatch");

        Wallpaper = wallpaper;
    }
}
