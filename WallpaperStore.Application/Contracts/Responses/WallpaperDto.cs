namespace WallpaperStore.API.Contracts
{
    public record WallpaperDto(
        Guid Id,
        string Title,
        string Description,
        string Url,
        decimal Price,
        Guid OwenerId
        );
}
