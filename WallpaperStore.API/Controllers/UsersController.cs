using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Xml.Linq;
using WallpaperStore.API.Contracts;
using WallpaperStore.Application.Mapping;
using WallpaperStore.Application.Services;
using WallpaperStore.Core.Models;
using WallpaperStore.DataAccess.Entities;

namespace WallpaperStore.API.Controllers
{
    [ApiController]
    [Route("api/users")]
    public class UsersController : ControllerBase
    {
        private readonly IUserService _userService;
        private readonly IUserMapper _userMapper;
        public UsersController(IUserService userService, IUserMapper userMapper)
        {
            _userService = userService;
            _userMapper = userMapper;
        }
          
        [HttpGet("{userId}")]
        public async Task<ActionResult<List<WallpaperResponse>>> GetById(Guid userId)
        {
            var usersResult = await _userService.GetUserById(userId);
            return usersResult.IsSuccess
                ? Ok(_userMapper.MapToUserResponse(usersResult.Value))
                : BadRequest(usersResult.Error);
        }

        [HttpGet("{userId}/wallpapers")]
        public async Task<ActionResult<List<WallpaperResponse>>> GetByIdWithWallpapers(Guid userId)
        {
            var usersResult = await _userService.GetUserByIdWithWallpapers(userId);
            return usersResult.IsSuccess
                ? Ok(_userMapper.MapToUserResponse(usersResult.Value, true))
                : BadRequest(usersResult.Error);
        }
        
        [HttpPost]
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
            if (userResult.IsFailure)
                return BadRequest(userResult.Error);

            var createUserResult = await _userService.CreateUser(userResult.Value);
            return Ok(createUserResult.Value);
        }

        [HttpPost("{userId}/saved-wallpapers")]
        public async Task<ActionResult<Guid>> SaveWallpaper(Guid userId, [FromBody] SaveWallpaperRequest request)
        {
            var saveWallpaperResult = await _userService.SaveWallpaper(
                userId,
                request.WallpaperId,
                request.IsFavorite);
            return Ok(saveWallpaperResult.Value);
        }

        [HttpPost("{userId}/added-wallpaper")]
        public async Task<ActionResult<Guid>> AddWallpaper(Guid userId, [FromBody] AddWallpaperRequest request)
        {
            var wallpaperResult = Wallpaper.Create(
                Guid.NewGuid(),
                request.Title,
                request.Description,
                request.Url,
                request.Price,
                userId);
            if (wallpaperResult.IsFailure)
                return BadRequest(wallpaperResult.Error);

            var addWallpaperResult = await _userService.AddWallpaper(
                userId,
                wallpaperResult.Value);
            return Ok(addWallpaperResult.Value);
        }

        [HttpGet]
        public async Task<ActionResult<List<WallpaperResponse>>> GetUsers([FromQuery] bool includeWallpapers = false)
        { 
            if (includeWallpapers)
            {
                var usersResult = await _userService.GetAllWithWallpapers();
                return usersResult.IsSuccess
                    ? Ok(usersResult.Value.Select(u => _userMapper.MapToUserResponse(u, includeWallpapers)))
                    : BadRequest(usersResult.Error);
            }
            else
            {
                var usersResult = await _userService.GetAll();
                return usersResult.IsSuccess 
                    ? Ok(usersResult.Value.Select(u => _userMapper.MapToUserResponse(u, includeWallpapers))) 
                    : BadRequest(usersResult.Error);
            }
        }
    }
}
