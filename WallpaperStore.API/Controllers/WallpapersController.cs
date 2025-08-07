using Microsoft.AspNetCore.Mvc;
using WallpaperStore.API.Contracts;
using WallpaperStore.Application.Mapping;
using WallpaperStore.Application.Services;
using WallpaperStore.Core.Models;

namespace WallpaperStore.API.Controllers;

[ApiController]
[Route("api/wallpapers")]
public class WallpapersController : ControllerBase
{
    private readonly IWallpapersService _wallpapersService;
    public WallpapersController(IWallpapersService wallpapersService)
    {
        _wallpapersService = wallpapersService;
    }

    [HttpGet]
    public async Task<ActionResult<List<Wallpaper>>> Get()
    {
        var getResult = await _wallpapersService.GetAsync();
        if (getResult.IsFailure)
            return BadRequest(getResult.Error);
        return Ok(getResult.Value.Select(w => w.ToDto()).ToList());
    }

    [HttpGet("{userId}")]
    public async Task<ActionResult<List<Wallpaper>>> GetUserWallpapers(Guid userId)
    {
        var getResult = await _wallpapersService.GetUserWallpapersAsync(userId);
        if (getResult.IsFailure)
            return BadRequest(getResult.Error);
        return Ok(getResult.Value.Select(w => w.ToDto()).ToList());
    }

    [HttpGet("{userId}/savedWallpapers")]
    public async Task<ActionResult<List<Wallpaper>>> GetUserSavedWallpapers(Guid? userId = null, Guid? wallpaperId = null, bool includeWallpapers = true)
    {
        var getResult = await _wallpapersService.GetAllSavedWallpapersAsync(userId, wallpaperId, includeWallpapers);
        if (getResult.IsFailure)
            return BadRequest(getResult.Error);
        return Ok(getResult.Value.Select(w => w.Wallpaper.ToDto()).ToList());
    }

    [HttpPost("{userId}/saved-wallpapers/save")]
    public async Task<ActionResult> SaveWallpaper(Guid userId, Guid wallpaperId, bool isFavorite)
    {
        var saveResult = await _wallpapersService.SaveWallpaperAsync(userId, wallpaperId, isFavorite);
        if (saveResult.IsFailure)
            return BadRequest(saveResult.Error);
        return Ok();
    }    
    
    [HttpPost("{userId}/saved-wallpapers/unsave")]
    public async Task<ActionResult> UnsaveWallpaper(Guid userId, Guid wallpaperId, bool isFavorite)
    {
        var saveResult = await _wallpapersService.UnsaveWallpaperAsync(userId, wallpaperId, isFavorite);
        if (saveResult.IsFailure)
            return BadRequest(saveResult.Error);
        return Ok();
    }
    [HttpPost("{userId}/added-wallpapers/add")]
    public async Task<ActionResult<Guid>> AddWallpaper(Guid userId, [FromBody] WallpaperRequest request)
    {
        var wallpaperResult = Wallpaper.Create(
            Guid.NewGuid(),
            request.Title,
            request.Description,
            request.Url,
            request.Price,
            userId);
        if(wallpaperResult.IsFailure)
            return BadRequest(wallpaperResult.Error);
        var addResult = await _wallpapersService.AddWallpaperAsync(userId, wallpaperResult.Value);
        if (addResult.IsFailure)
            return BadRequest(addResult.Error);
        return Ok(addResult.Value);
    }



    /*    [HttpGet("{userId}/addedWallpapers")]
        public async Task<ActionResult<List<Wallpaper>>> GetUserAddedWallpapers(Guid userId)
        {
            var getResult = await _wallpapersService.GetAllSavedWallpapersAsync(userId);
            if (getResult.IsFailure)
                return BadRequest(getResult.Error);
            return Ok(getResult.Value.Select(w => w.Wallpaper.ToDto()).ToList());
        }*/
}
