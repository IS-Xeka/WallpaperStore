namespace WallpaperStore.API.Contracts
{
    public record SaveWallpaperRequest(
        Guid WallpaperId,
        bool IsFavorite = false
        );
}
