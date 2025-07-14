using WallpaperStore.Core.Models;

namespace WallpaperStore.API.Contracts
{
    public record WallpaperRequest(
        string Title,
        string Description,
        string Url,
        decimal Price,
        Guid OwenerId
        );
}
