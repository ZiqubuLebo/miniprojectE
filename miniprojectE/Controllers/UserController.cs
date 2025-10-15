using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using miniprojectE.DTO.ResponseDTOs;
using miniprojectE.DTO.UserDTOs;
using miniprojectE.Models.Entities;
using miniprojectE.Services;

namespace miniprojectE.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IUserService _userService;

        public UserController(IUserService userService)
        {
            _userService = userService;
        }

        [HttpPost("Register")]
        public async Task<ActionResult<ApiResponseDTO<UserProfileDTO>>> Register([FromBody] UserRegistrationDTO dto)
        {
            try
            {
                var result = await _userService.RegisterUserAsync(dto);
                return Ok(new ApiResponseDTO<UserProfileDTO> { Success = true, Data = result });
            }
            catch (Exception ex)
            {
                return BadRequest(new ApiResponseDTO<UserProfileDTO> { Success = false, Message = ex.Message });
            }
        }

        [HttpPost("Login")]
        public async Task<ActionResult<ApiResponseDTO<string>>> Login([FromBody] UserLoginDTO dto)
        {
            //calls user service login and brings back authentication token
            try
            {
                var (token, user) = await _userService.LoginAsync(dto);
                return Ok(new {token, user});
            }
            catch (Exception ex)
            {
                return Unauthorized(new ApiResponseDTO<string> { Success = false, Message = ex.Message });
            }
        }

        [HttpGet("GetProfile/{id}")]
        [Authorize]
        public async Task<ActionResult<ApiResponseDTO<UserProfileDTO>>> GetProfile(Guid id)
        {
            try
            {
                var profile = await _userService.GetUserProfileAsync(id);
                return Ok(new ApiResponseDTO<UserProfileDTO> { Success = true, Data = profile });
            }
            catch (Exception ex)
            {
                return NotFound(new ApiResponseDTO<UserProfileDTO> { Success = false, Message = ex.Message });
            }
        }

        [HttpPut("UpdateProfile/{id}")]
        [Authorize]
        public async Task<ActionResult<ApiResponseDTO<UserProfileDTO>>> UpdateProfile(Guid id, [FromBody] UserUpdateDTO dto)
        {
            try
            {
                var result = await _userService.UpdateUserAsync(id, dto);
                return Ok(new ApiResponseDTO<UserProfileDTO> { Success = true, Data = result });
            }
            catch (Exception ex)
            {
                return BadRequest(new ApiResponseDTO<UserProfileDTO> { Success = false, Message = ex.Message });
            }
        }

        //get a list of customers
        //get a list of workers
        [HttpGet("GetProfileByType/{type}")]
        [Authorize]
        public async Task<ActionResult<ApiResponseDTO<List<UserProfileDTO>>>> GetUsersByRoleAsync(UserType type)
        {
            try
            {
                var profile = await _userService.GetUsersByRoleAsync(type);
                return Ok(new ApiResponseDTO<List<UserProfileDTO>> { Success = true, Data = profile });
            }
            catch (Exception ex)
            {
                return NotFound(new ApiResponseDTO<UserProfileDTO> { Success = false, Message = ex.Message });
            }
        }
    }
}
