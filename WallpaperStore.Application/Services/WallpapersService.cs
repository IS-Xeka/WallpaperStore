using CSharpFunctionalExtensions;
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

    public async Result<Task> UpdateWallpaper(Guid id, string title, string description)
    {
        try
        {
            var updateResult = await _wallpaperRepository.Update(id, title, description);
            return Result.Success(updateResult);
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

    public Task<Result<Guid>> Create(Wallpaper wallpaper)
    {
        throw new NotImplementedException();
    }

    Task<Result<Guid>> IWallpapersService.DeleteWallpaper(Guid id)
    {
        throw new NotImplementedException();
    }

    public Task<Result<Wallpaper>> GetById(Guid id)
    {
        throw new NotImplementedException();
    }

    public Task<Result<Wallpaper>> GetByIdWithOwner(Guid id)
    {
        throw new NotImplementedException();
    }

    Task<Result<List<Wallpaper>>> IWallpapersService.GetWallpapers()
    {
        throw new NotImplementedException();
    }

    public Task<Result<List<Wallpaper>>> GetWallpapersWithOwners()
    {
        throw new NotImplementedException();
    }

    public Task<Result> Update(Guid id, string title, string description)
    {
        throw new NotImplementedException();
    }
}
