using Microsoft.AspNetCore.Mvc;
using WallpaperStore.API.Contracts;
using WallpaperStore.Application.Mapping;
using WallpaperStore.Application.Services;
using WallpaperStore.Core.Models;

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
        public async Task<ActionResult<List<WallpaperDto>>> GetById(Guid userId)
        {
            var usersResult = await _userService.GetByIdAsync(userId);
            return usersResult.IsSuccess
                ? Ok(_userMapper.MapToUserDto(usersResult.Value))
                : BadRequest(usersResult.Error);
        }

        [HttpGet]
        public async Task<ActionResult<List<WallpaperDto>>> GetUsers([FromQuery] bool includeWallpapers = false)
        {
            if (includeWallpapers)
            {
                var usersResult = await _userService.GetAsync();
                return usersResult.IsSuccess
                    ? Ok(usersResult.Value.Select(u => _userMapper.MapToUserDto(u, includeWallpapers)))
                    : BadRequest(usersResult.Error);
            }
            else
            {
                var usersResult = await _userService.GetAsync();
                return usersResult.IsSuccess
                    ? Ok(usersResult.Value.Select(u => _userMapper.MapToUserDto(u, includeWallpapers)))
                    : BadRequest(usersResult.Error);
            }
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

            var createUserResult = await _userService.CreateAsync(userResult.Value);
            return Ok(createUserResult.Value);
        }

        [HttpPost("{userId}")]
        public async Task<ActionResult<Guid>> UpdateUsername(Guid userId, string newName)
        {
            var updateResult = await _userService.UpdateAsync(userId, newName);
            if(updateResult.IsFailure)
                return BadRequest(updateResult.Error);
            return Ok(updateResult.Value);
        }

        [HttpDelete("{userId}")]
        public async Task<ActionResult<Guid>> DeleteUser(Guid userId)
        {
            var updateResult = await _userService.DeleteAsync(userId);
            if (updateResult.IsFailure)
                return BadRequest(updateResult.Error);
            return Ok();
        }
    }
}
