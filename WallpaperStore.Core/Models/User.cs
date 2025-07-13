using Microsoft.EntityFrameworkCore.Storage.ValueConversion.Internal;

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

    public static User Create(Guid id, string name, Email email, string passwordHash, DateTime registerDate, bool isPublicProfile = true)
    {
        var errorString = string.Empty;
        if(string.IsNullOrEmpty(name) || name.Length > MAX_NAME_LENGTH)
        {
            throw new ArgumentException("Name can not be empty or longer than 50 symbols");
        }
        if (string.IsNullOrEmpty(passwordHash))
        {
            throw new ArgumentNullException("Password can not be emty");
        }
        return new User(id, name, email, passwordHash, registerDate, isPublicProfile);
    }

    public void SaveWallpaper(Wallpaper wallpaper, bool isFavorite = false)
    {
        IsValidWallpaper(wallpaper);
        if (_addedWallpapers.Any(w => w.Id == wallpaper.Id))
            throw new InvalidOperationException($"Cannot save own wallpaper, {wallpaper.Id}");
        if (_savedWallpapers.Any(w => w.WallpaperId == wallpaper.Id))
            throw new InvalidOperationException($"Wallpaper has been saved, {wallpaper.Id}");
        _savedWallpapers.Add(UserSavedWallpaper.Create(this, wallpaper, isFavorite));
    }
    public void RemoveSavedWallpaper(Wallpaper wallpaper)
    {
        IsValidWallpaper(wallpaper);
        var removeWallpaper = _savedWallpapers.FirstOrDefault(sw => sw.WallpaperId == wallpaper.Id)
            ?? throw new ArgumentException(nameof(wallpaper), $"Wallpaper with ID {wallpaper.Id} not found in saved collection");
        _savedWallpapers.Remove(removeWallpaper);
    }
    public void AddWallpaper(Wallpaper wallpaper)
    {
        IsValidWallpaper(wallpaper);
        if (_addedWallpapers.Any(w => w.Id == wallpaper.Id))
            throw new InvalidOperationException($"Wallpaper has been added. {wallpaper.Id}");
        _addedWallpapers.Add(wallpaper);
    }
    public void RemoveAddedWallpaper(Wallpaper wallpaper)
    {
        IsValidWallpaper(wallpaper);
        var removeWallpaper = _addedWallpapers.FirstOrDefault(w => w.Id == wallpaper.Id)
            ?? throw new ArgumentException(nameof(wallpaper), $"Wallpaper with ID {wallpaper.Id} not found in saved collection");
        _addedWallpapers.Remove(removeWallpaper);
    }

    public void Login()
    {
        IsOnline = true;
    }

    public void LogOut()
    {
        IsOnline = false;
        LastTimeOnline  = DateTime.UtcNow;
    }

    private static void IsValidWallpaper(Wallpaper wallpaper)
    {
        if(wallpaper == null)
        {
            throw new ArgumentNullException(nameof(wallpaper), $"Wallpaper can not be null.");
        }
    }
}