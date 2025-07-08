using WallpaperStore.Core.Models;
using WallpaperStore.DataAccess.Repositories;

namespace WallpaperStore.Application.Services;

public class WallpapersService : IWallpapersService
{
    private readonly IWallpapersRepository _wallpaperRepository;
    public WallpapersService(IWallpapersRepository wallpapersRepository)
    {
        _wallpaperRepository = wallpapersRepository;
    }

    public async Task UpdateWallpaper(Guid id, string title, string description)
    {
        try
        {
            await _wallpaperRepository.Update(id, title, description);
        }
        catch (Exception ex)
        {

            throw new ArgumentNullException(ex.Message);
        }
    }

    public async Task<Guid> DeleteWallpaper(Guid id)
    {
        try
        {
            await _wallpaperRepository.DeleteWallpaper(id);
            return id;
        }
        catch (Exception ex)
        {

            throw new Exception(ex.Message);
        }
    }
    
    public async Task<Guid> CreateWallpaper(Wallpaper wallpaper)
    {
        try
        {
            var wallpaperId = await _wallpaperRepository.Create(wallpaper);
            return wallpaperId;
        }
        catch (Exception ex)
        {
            Console.WriteLine("!!!!!!!!!!!!!!!!!!!!!!!    " + ex);
            throw new Exception(ex.Message);
        }
    }
    public async Task<List<Wallpaper>> GetWallpapers()
    {
        try
        {
            return await _wallpaperRepository.GetWallpapers();
        }
        catch (Exception ex)
        {
            throw new Exception(ex.Message);
        }
    }
}
