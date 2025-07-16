namespace WallpaperStore.API.Contracts
{
    public record AddWallpaperRequest(
        Guid UserId,
        string Title,
        string Description,
        string Url,
        decimal Price
        );
}
