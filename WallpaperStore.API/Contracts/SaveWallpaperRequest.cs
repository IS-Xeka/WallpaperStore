namespace WallpaperStore.API.Contracts
{
    public record SaveWallpaperRequest(
        Guid UserId,
        Guid WallpaperId,
        bool IsFavorite = false
        );
}
