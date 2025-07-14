using WallpaperStore.Core.Models;

namespace WallpaperStore.API.Contracts
{
    public record UserRequest(
        string name,
        string email,
        string passwordHash
        );
}
