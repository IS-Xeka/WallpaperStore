namespace WallpaperStore.API.Contracts
{
    public record AddWallpaperRequest(
        string Title,
        string Description,
        string Url,
        decimal Price
        );
}
