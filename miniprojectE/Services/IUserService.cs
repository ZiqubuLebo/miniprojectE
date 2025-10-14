using miniprojectE.DTO.UserDTOs;
using miniprojectE.Models.Entities;

namespace miniprojectE.Services
{
    public interface IUserService
    {
        Task<UserProfileDTO> RegisterUserAsync(UserRegistrationDTO dto);
        Task<(string Token, UserProfileDTO user)> LoginAsync(UserLoginDTO dto);
        Task<UserProfileDTO> GetUserProfileAsync(Guid userId);
        Task<UserProfileDTO> UpdateUserAsync(Guid userId, UserUpdateDTO dto);
        Task<List<UserProfileDTO>> GetUsersByRoleAsync(UserType userType);
    }
}
