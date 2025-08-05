using CSharpFunctionalExtensions;
using WallpaperStore.Core.Models;
using WallpaperStore.DataAccess.Repositories;

namespace WallpaperStore.Application.Services;

public class WallpapersService: IWallpapersService
{
    private readonly IWallpapersRepository _wallpaperRepository;
    public WallpapersService(IWallpapersRepository wallpapersRepository)
    {
        _wallpaperRepository = wallpapersRepository;
    }

    public Task<Result<Guid>> AddWallpaperAsync(Guid userId, Wallpaper wallpaper, CancellationToken ct = default)
    {
        throw new NotImplementedException();
    }

    public Task<Result<List<Wallpaper>>> GetAsync()
    {
        throw new NotImplementedException();
    }

    public Task<Result<List<Wallpaper>>> GetUserWallpapersAsync(Guid userId)
    {
        throw new NotImplementedException();
    }
}
