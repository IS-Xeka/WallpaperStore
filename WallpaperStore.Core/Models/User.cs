using Microsoft.EntityFrameworkCore.Storage.ValueConversion.Internal;

namespace WallpaperStore.Core.Models;

public class User
{
    public const uint MAX_STRING_LENGTH = 50;
    public Guid Id { get; private set; }
    public string Name { get; private set; } = string.Empty;
    public string Email { get; private set; } = string.Empty;
    public string PasswordHash { get; private set; } = string.Empty;
    public DateTime? RegisterDate { get; private set; }
    public DateTime? LastTimeOnline { get; private set; }
    public bool IsOnline { get; private set; } = false;
    public bool IsPublicProfile { get; private set; } = true;
    public List<Wallpaper> SavedWallpapers { get; private set; } = [];
    public List<Wallpaper> AddedWallpapers { get; private set; } = [];

    private User(Guid id, string name, string email, string passwordHash, DateTime registerDate, bool isPublicProfile = true)
    {
        Id = id;
        Name = name;
        Email = email;
        PasswordHash = passwordHash;
        RegisterDate = registerDate;
        IsPublicProfile = isPublicProfile;
    }

    public static User Create(Guid id, string name, string email, string passwordHash, DateTime registerDate, bool isPublicProfile = true)
    {
        var errorString = string.Empty;
        if(string.IsNullOrEmpty(name) || name.Length > MAX_STRING_LENGTH)
        {
            throw new ArgumentException("Name can not be empty or longer than 50 symbols");
        }
        if (string.IsNullOrEmpty(email) || email.Length > MAX_STRING_LENGTH)
        {
            throw new ArgumentException("Email can not be empty or longer than 50 symbols");
        }
        if (string.IsNullOrEmpty(passwordHash))
        {
            throw new ArgumentNullException("Password can not be emty");
        }
        return new User(id, name, email, passwordHash, registerDate, isPublicProfile);
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
}