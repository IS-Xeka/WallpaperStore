namespace WallpaperStore.API.Contracts
{
    public record WallpaperUpdateResponse(
        Guid Id,
        string Title,
        string Description
        );

}
