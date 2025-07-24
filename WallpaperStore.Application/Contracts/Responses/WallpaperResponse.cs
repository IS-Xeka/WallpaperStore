namespace WallpaperStore.API.Contracts
{
    public record WallpaperResponse(
        Guid Id,
        string Title,
        string Description,
        string Url,
        decimal Price,
        Guid OwenerId
        );
}
