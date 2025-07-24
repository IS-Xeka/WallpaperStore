using Microsoft.AspNetCore.Mvc;
using WallpaperStore.API.Contracts;
using WallpaperStore.Application.Services;
using WallpaperStore.Core.Models;

namespace WallpaperStore.API.Controllers;

[ApiController]
[Route("api/users/{userId}/wallpapers")]
public class WallpapersController : ControllerBase
{
    private readonly IWallpapersService _wallpapersService;
    public WallpapersController(IWallpapersService wallpapersService)
    {
        _wallpapersService = wallpapersService;
    }
}
