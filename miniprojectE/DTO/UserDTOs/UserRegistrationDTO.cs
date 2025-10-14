using System.ComponentModel.DataAnnotations;

namespace miniprojectE.DTO.UserDTOs
{
    public class UserRegistrationDTO
    {
        [EmailAddress]
        public required string UserEmail { get; set; }

        [MinLength(6)]
        public required string Password { get; set; }

        public required string Name { get; set; }

        public required string LastName { get; set; }

        public string? Phone { get; set; }
    }
}
