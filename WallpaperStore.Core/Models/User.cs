using Microsoft.EntityFrameworkCore.Storage.ValueConversion.Internal;

namespace WallpaperStore.Core.Models;

public class User
{
    public const uint MAX_NAME_LENGTH = 50;
    public Guid Id { get; private set; }
    public string Name { get; private set; } = string.Empty;
    public Email Email { get; private set; }
    public string PasswordHash { get; private set; } = string.Empty;
    public DateTime? RegisterDate { get; private set; }
    public DateTime? LastTimeOnline { get; private set; }
    public bool IsOnline { get; private set; } = false;
    public bool IsPublicProfile { get; private set; } = true;

    private readonly List<Wallpaper> _addedWallpapers = [];
    private readonly List<Wallpaper> _savedWallpapers = [];
    public IReadOnlyCollection<Wallpaper> AddedWallpapers => _addedWallpapers.AsReadOnly();
    public IReadOnlyCollection<Wallpaper> SavedWallpapers => _savedWallpapers.AsReadOnly();

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

    public void AddWallpaper(Wallpaper wallpaper)
    {
        IsValidWallpaper(wallpaper);
        if (_addedWallpapers.Any(w => w.Id == wallpaper.Id))
        {
            throw new ArgumentException($"Wallpaper has been added. {nameof(wallpaper)}");
        }
        _addedWallpapers.Add(wallpaper);
    }
    public void SaveWallpaper(Wallpaper wallpaper)
    {
        IsValidWallpaper(wallpaper);
        if (_addedWallpapers.Any(w => w.Id == wallpaper.Id))
        {
            throw new ArgumentException($"Wallpaper has been saved. {nameof(wallpaper)}");
        }
        _savedWallpapers.Add(wallpaper);
    }
    public void RemoveAddedWallpaper(Wallpaper wallpaper)
    {
        IsValidWallpaper(wallpaper);
        var removeWallpaper = _addedWallpapers.FirstOrDefault(w => w.Id == wallpaper.Id)
            ?? throw new ArgumentException($"Wallpaper can not be found. {nameof(wallpaper)}");
        _addedWallpapers.Remove(removeWallpaper);
    }
    public void RemoveSavedWallpaper(Wallpaper wallpaper)
    {
        IsValidWallpaper(wallpaper);
        var removeWallpaper = _savedWallpapers.FirstOrDefault(w => w.Id == wallpaper.Id)
            ?? throw new ArgumentException($"Wallpaper can not be found. {nameof(wallpaper)}");
        _savedWallpapers.Remove(removeWallpaper);
    }

    public void Login()
    {
        IsOnline = true;
    }

    public void LogOut()
    {
        IsOnline = false;
        LastTimeOnline  = DateTime.Now;
    }

    private void IsValidWallpaper(Wallpaper wallpaper)
    {
        if(wallpaper == null)
        {
            throw new ArgumentNullException($"Wallpaper can not be null. {wallpaper}");
        }
    }
}