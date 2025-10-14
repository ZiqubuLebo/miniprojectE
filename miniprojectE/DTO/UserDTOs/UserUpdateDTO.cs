using System.ComponentModel.DataAnnotations;

namespace miniprojectE.DTO.UserDTOs
{
    public class UserUpdateDTO
    {
        public required string FirstName { get; set; }

        public required string LastName { get; set; }

        public string? Phone { get; set; }
    }
}
