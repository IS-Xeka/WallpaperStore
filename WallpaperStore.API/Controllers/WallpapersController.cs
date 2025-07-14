using Microsoft.AspNetCore.Mvc;
using WallpaperStore.API.Contracts;
using WallpaperStore.Application.Services;
using WallpaperStore.Core.Models;

namespace WallpaperStore.API.Controllers;

[ApiController]
[Route("[controller]")]
public class WallpapersController : Controller
{
    private readonly IWallpapersService _wallpapersService;
    private readonly IUserService _userService;
    public WallpapersController(IWallpapersService wallpapersService, IUserService userService)
    {
        _wallpapersService = wallpapersService;
        _userService = userService;
    }

    [HttpPost]
    [Route("UpdateWallpaper")]
    public async Task<ActionResult<Guid>> UpdateWallpaper([FromBody] WallpaperUpdateResponse response)
    {
        var updateResult = await _wallpapersService.UpdateWallpaper(response.Id, response.Title, response.Description);
        return Ok(updateResult.Value);
    }
    

    [HttpDelete]
    [Route("DeleteWallpaper")]

    public async Task<ActionResult<Guid>> DeleteWallpaper([FromQuery] Guid id)
    {
        var deleteResult = await _wallpapersService.DeleteWallpaper(id);
        return Ok(deleteResult.Value);
    }

    [HttpGet]
    [Route("GetWallpapers")]
    public async Task<ActionResult<List<WallpaperResponse>>> GetWallpapers()
    {
        var wallpapers =  await _wallpapersService.GetWallpapers();
        var wallpapersResponse = wallpapers.Value.Select(w => new WallpaperResponse
        (
            w.Id,
            w.Title,
            w.Description,
            w.Url,
            w.Price
        ));
        return Ok(wallpapersResponse);
    }

    [HttpPost]
    [Route("CreateWallpaper")]
    public async Task<ActionResult<Guid>> CreateWallpaper([FromBody] WallpaperRequest request)
    {
        var userResult = await _userService.GetUserById(request.OwenerId);
        var wallpaper = Wallpaper.Create(
            Guid.NewGuid(),
            request.Title,
            request.Description,
            request.Url,
            request.Price,
            userResult.Value
            );

        var wallpaperId = await _wallpapersService.CreateWallpaper(wallpaper);
        return Ok(wallpaperId.Value);
    }
}
