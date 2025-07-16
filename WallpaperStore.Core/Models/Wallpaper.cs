namespace WallpaperStore.Core.Models;

public class Wallpaper
{
    private const uint MAX_TITLE_LENGTH = 50;
    private const uint MAX_DESCRIPTION_LENGTH = 300;
    public Guid Id { get; private set; }
    public Guid OwnerId { get; private set; }
    public User? Owner { get; private set; }
    public string Title { get; private set; } = string.Empty;
    public string Description { get; private set; } = string.Empty;
    public string Url { get; private set; } = string.Empty;
    public decimal Price { get; private set; }
    private Wallpaper(Guid id, string title, string description, string url, decimal price, Guid ownerId)
    {
        Id = id; 
        Title = title; 
        Description = description; 
        Url = url; 
        Price = price;
        OwnerId = ownerId;
    }

    public static Wallpaper Create(Guid id, string title, string description, string url, decimal price, Guid ownerId)
    {
        if(string.IsNullOrEmpty(title) || title.Length > MAX_TITLE_LENGTH)
        {
            throw new ArgumentException($"Title can not be empty and must be less than {MAX_TITLE_LENGTH} characters");
        }
        if (string.IsNullOrEmpty(description) || description.Length > MAX_DESCRIPTION_LENGTH)
        {
            throw new ArgumentException($"Description can not be empty and must be less than {MAX_TITLE_LENGTH} characters");
        }
        if (string.IsNullOrEmpty(url))
        {
            throw new ArgumentException($"Url can not be empty");
        }
        if (price < 0)
        {
            throw new ArgumentException($"Price can not be below than 0");
        }

        var wallpaper = new Wallpaper(id, title, description, url, price, ownerId);

        return wallpaper;
    }
}
