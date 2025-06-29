namespace WallpaperStore.Core.Models;

public class Wallpaper
{
    private const uint MAX_TITLE_LENGTH = 100;
    public Guid Id { get; private set; }
    public string Title { get; private set; } = string.Empty;
    public string Description { get; private set; } = string.Empty;
    public string Url { get; private set; } = string.Empty;
    public decimal Price { get; private set; }

    private Wallpaper(Guid id, string title, string description, string url, decimal price)
    {
        Id = id; 
        Title = title; 
        Description = description; 
        Url = url; 
        Price = price;
    }

    public static (Wallpaper wallpaper, string error) Create(Guid id, string title, string description, string url, decimal price)
    {
        string errorString = string.Empty;

        if(string.IsNullOrWhiteSpace(title) || title.Length > MAX_TITLE_LENGTH)
        {
            errorString = $"Имя {title} не может быть пустым или длиннее 100 символов";
        }

        var wallpaper = new Wallpaper(id, title, description, url, price);

        return (wallpaper, errorString);
    }
}
