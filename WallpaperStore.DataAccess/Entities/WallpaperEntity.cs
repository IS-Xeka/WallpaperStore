using WallpaperStore.Core.Models;

namespace WallpaperStore.DataAccess.Entities;

public class WallpaperEntity
{
    public Guid Id { get; set; }
    public string Title { get; set; }
    public string Description { get; set; }
    public string Url { get; set; }
    public decimal Price { get; set; }
    public Guid OwnerId { get;  set; }
    public UserEntity? Owner { get;  set; }
}
