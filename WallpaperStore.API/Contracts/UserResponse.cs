using WallpaperStore.Core.Models;

namespace WallpaperStore.API.Contracts
{
    public record UserResponse(
        Guid Id,
        string Name,
        Email Email,
        DateTime RegisterDate,
        DateTime? LastTimeOnline,
        bool IsPublicProfile
        );

}
