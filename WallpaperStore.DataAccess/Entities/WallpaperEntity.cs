namespace WallpaperStore.DataAccess.Entities;

public class WallpaperEntity
{
    public Guid Id { get; set; }
    public string Title { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public string Url { get; set; } = string.Empty;
    public decimal Price { get; set; }
}
