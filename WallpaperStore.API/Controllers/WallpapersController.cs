﻿using Microsoft.AspNetCore.Mvc;
using WallpaperStore.API.Contracts;
using WallpaperStore.Application.Services;
using WallpaperStore.Core.Models;

namespace WallpaperStore.API.Controllers;

[ApiController]
[Route("[controller]")]
public class WallpapersController : Controller
{
    private readonly IWallpapersService _wallpapersService;
    public WallpapersController(IWallpapersService wallpapersService)
    {
        _wallpapersService = wallpapersService;
    }

    [HttpPost]
    [Route("UpdateWallpaper")]
    public async Task<ActionResult> UpdateWallpaper([FromBody] WallpaperUpdateResponse response)
    {
        await _wallpapersService.UpdateWallpaper(response.Id, response.Title, response.Description);
        return Ok();
    }
    

    [HttpDelete]
    [Route("DeleteWallpaper")]

    public async Task<ActionResult<Guid>> DeleteWallpaper([FromQuery] Guid id)
    {
        await _wallpapersService.DeleteWallpaper(id);
        return Ok(id);
    }

    [HttpGet]
    [Route("GetWallpapers")]
    public async Task<ActionResult<List<WallpaperResponse>>> GetWallpapers()
    {
        var wallpapers =  await _wallpapersService.GetWallpapers();
        var wallpapersResponse = wallpapers.Select(w => new WallpaperResponse
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
        var (wallpaper, error) = Wallpaper.Create(
            Guid.NewGuid(),
            request.Title,
            request.Description,
            request.Url,
            request.Price);
        if (!string.IsNullOrWhiteSpace(error))
        {
            return BadRequest(error);
        }

        var wallpaperId = await _wallpapersService.CreateWallpaper(wallpaper);
        return Ok(wallpaperId);
    }
}
