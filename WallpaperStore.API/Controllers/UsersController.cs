using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Xml.Linq;
using WallpaperStore.API.Contracts;
using WallpaperStore.Application.Services;
using WallpaperStore.Core.Models;
using WallpaperStore.DataAccess.Entities;

namespace WallpaperStore.API.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class UsersController : Controller
    {
        private readonly IUserService _userService;
        public UsersController(IUserService userService)
        {
            _userService = userService;
        }


        [HttpGet]
        [Route("GetById")]
        public async Task<ActionResult<List<WallpaperResponse>>> GetById([FromHeader] Guid UserId)
        {
            var usersResult = await _userService.GetUserById(UserId);
            if (usersResult.IsFailure)
                throw new InvalidOperationException(usersResult.Error);
            var userEntity = usersResult.Value;
            var usersResponse = new UserResponse
            (
                userEntity.Id,
                userEntity.Name,
                userEntity.Email,
                userEntity.RegisterDate,
                userEntity.LastTimeOnline,
                userEntity.IsPublicProfile
            );
            return Ok(usersResponse);
        }
        [HttpGet]
        [Route("GetUsers")]
        public async Task<ActionResult<List<WallpaperResponse>>> GetUsers()
        {
            var usersResult = await _userService.GetAll();
            if (usersResult.IsFailure)
                throw new InvalidOperationException(usersResult.Error);
            var usersResponse = usersResult.Value.Select(u => new UserResponse
            (
                u.Id,
                u.Name,
                u.Email,
                u.RegisterDate,
                u.LastTimeOnline,
                u.IsPublicProfile
            ));
            return Ok(usersResponse);
        }

        [HttpGet]
        [Route("GetWithWallpapers")]
        public async Task<ActionResult<List<UserWithWallpapersResponse>>> GetWithWallpapers()
        {
            var usersWithWallpapersResult = await _userService.GetAllWithWallpapers();
            if (usersWithWallpapersResult.IsFailure)
                return BadRequest(usersWithWallpapersResult.Error);
            var usersWithWallpapersResponse = usersWithWallpapersResult.Value.Select(u => new UserWithWallpapersResponse
            (
                u.Id,
                u.Name,
                u.Email,
                u.RegisterDate,
                u.LastTimeOnline,
                u.IsPublicProfile,
                u.AddedWallpapers.Select(w => new WallpaperResponse(
                                w.Id,
                                w.Title,
                                w.Description,
                                w.Url,
                                w.Price,
                                w.OwnerId
                    )).ToList(),
                u.SavedWallpapers.Select(w => new WallpaperResponse(
                                w.Wallpaper.Id,
                                w.Wallpaper.Title,
                                w.Wallpaper.Description,
                                w.Wallpaper.Url,
                                w.Wallpaper.Price,
                                w.Wallpaper.OwnerId
                    )).ToList()
            ));
            return Ok(usersWithWallpapersResponse);
        }

        [HttpPost]
        [Route("CreateUser")]
        public async Task<ActionResult<Guid>> CreateUser([FromBody] UserRequest request)
        {
            var emailResult = Email.Create(request.email);
            if (emailResult.IsFailure)
                return BadRequest(emailResult.Error);
            var userResult = Core.Models.User.Create(
                    Guid.NewGuid(),
                    request.name,
                    emailResult.Value,
                    request.passwordHash,
                    DateTime.UtcNow
                    );
            if(userResult.IsFailure)
                return BadRequest(userResult.Error);

            var createUserResult = await _userService.CreateUser(userResult.Value);
            return Ok(createUserResult.Value);
        }

        [HttpPost]
        [Route("SaveWallpaper")]
        public async Task<ActionResult<Guid>> SaveWallpaper([FromBody] SaveWallpaperRequest request)
        {
            var saveWallpaperResult = await _userService.SaveWallpaper(
                request.UserId,
                request.WallpaperId,
                request.IsFavorite);
            return Ok(saveWallpaperResult.Value);
        }

        [HttpPost]
        [Route("AddWallpaper")]
        public async Task<ActionResult<Guid>> AddWallpaper([FromBody] AddWallpaperRequest request)
        {
            var wallpaperResult = Wallpaper.Create(
                Guid.NewGuid(),
                request.Title,
                request.Description,
                request.Url,
                request.Price,
                request.UserId);
            if(wallpaperResult.IsFailure)
                return BadRequest(wallpaperResult.Error);

            var addWallpaperResult = await _userService.AddWallpaper(
                request.UserId,
                wallpaperResult.Value);
            return Ok(addWallpaperResult.Value);
        }
    }
}
