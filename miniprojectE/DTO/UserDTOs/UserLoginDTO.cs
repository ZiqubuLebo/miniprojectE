using System.ComponentModel.DataAnnotations;

namespace miniprojectE.DTO.UserDTOs
{
    public class UserLoginDTO
    {
        [EmailAddress]
        public required string UserEmail { get; set; }

        public required string Password { get; set; }
    }
}
