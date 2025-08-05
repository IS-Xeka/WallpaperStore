using CSharpFunctionalExtensions;

namespace WallpaperStore.Core.Models;

public class User
{
    public const uint MAX_NAME_LENGTH = 50;
    public Guid Id { get; }
    public string Name { get; } = string.Empty;
    public Email Email { get; }
    public string PasswordHash { get; } = string.Empty;
    public DateTime RegisterDate { get; }
    public DateTime? LastTimeOnline { get; private set; }
    public bool IsOnline { get; private set; } = false;
    public bool IsPublicProfile { get; private set; } = true;

    private readonly List<Wallpaper> _addedWallpapers = new ();
    private readonly List<UserSavedWallpaper> _savedWallpapers = new ();
    public IReadOnlyCollection<Wallpaper> AddedWallpapers => _addedWallpapers.AsReadOnly();
    public IReadOnlyCollection<UserSavedWallpaper> SavedWallpapers => _savedWallpapers.AsReadOnly();

    private User(Guid id, string name, Email email, string passwordHash, DateTime registerDate, bool isPublicProfile = true)
    {
        Id = id;
        Name = name;
        Email = email;
        PasswordHash = passwordHash;
        RegisterDate = registerDate;
        IsPublicProfile = isPublicProfile;
    }

    public static Result<User> Create(Guid id, string name, Email email, string passwordHash, DateTime registerDate, bool isPublicProfile = true)
    {
        var errors = new List<string>();
        if (id == Guid.Empty)
            errors.Add("User ID cannot be empty");
        if (email == null)
            errors.Add("Email is null");
        if(string.IsNullOrEmpty(name))
            errors.Add("Name is empty");
        if (name.Length > MAX_NAME_LENGTH)
            errors.Add("Name longer than 50 symbols");
        if (string.IsNullOrEmpty(passwordHash))
            errors.Add("Password is empty");
        if (errors.Any())
            return Result.Failure<User>(string.Join("; ", errors));
        return Result.Success(new User(id, name, email, passwordHash, registerDate, isPublicProfile));
    }

    public Result SaveWallpaper(Wallpaper wallpaper, bool isFavorite = false)
    {
        if (wallpaper == null)
            return Result.Failure($"Wallpaper is null.");
        if (_addedWallpapers.Any(w => w.Id == wallpaper.Id))
            return Result.Failure($"Cannot save own wallpaper, {wallpaper.Id}");
        if (_savedWallpapers.Any(w => w.WallpaperId == wallpaper.Id))
            return Result.Failure($"Wallpaper has been saved, {wallpaper.Id}");

        var userSavedWallpaperResult = UserSavedWallpaper.Create(this, wallpaper, DateTime.UtcNow, isFavorite);
        if (userSavedWallpaperResult.IsFailure)
            return Result.Failure(userSavedWallpaperResult.Error);

        _savedWallpapers.Add(userSavedWallpaperResult.Value);
        return Result.Success();
    }
    public Result RemoveSavedWallpaper(Wallpaper wallpaper)
    {
        var wallpaperToRemove = _savedWallpapers.FirstOrDefault(sw => sw.WallpaperId == wallpaper.Id);
        if(wallpaperToRemove == null)
            return Result.Failure($"Wallpaper not found in saved collection. {nameof(wallpaper)}");

        _savedWallpapers.Remove(wallpaperToRemove);
        return Result.Success();
    }
    public Result AddWallpaper(Wallpaper wallpaper)
    {
        if (wallpaper == null)
            return Result.Failure($"Wallpaper is null.");
        if (_addedWallpapers.Any(w => w.Id == wallpaper.Id))
            return Result.Failure($"Wallpaper has been added. {wallpaper.Id}");
        _addedWallpapers.Add(wallpaper);
        return Result.Success();
    }
    public Result RemoveAddedWallpaper(Wallpaper wallpaper)
    {
        var wallpaperToRemove = _addedWallpapers.FirstOrDefault(w => w.Id == wallpaper.Id);
        if (wallpaperToRemove == null)
            Result.Failure($"Wallpaper not found in saved collection. {nameof(wallpaper)}");

        _addedWallpapers.Remove(wallpaperToRemove);
        return Result.Success();
    }

    public void ToggleProfileVisibility()
    {
        IsPublicProfile = !IsPublicProfile;
    }

    public void SetOnline()
    {
        IsOnline = true;
    }

    public void SetOffline()
    {
        IsOnline = false;
        LastTimeOnline = DateTime.UtcNow;
    }
}