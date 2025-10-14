//using FurnitureStore.DTOs;
using miniprojectE.DTO.AddressDTOs;
using miniprojectE.Models.Entities;

namespace miniprojectE.DTO.UserDTOs
{
    public class UserProfileDTO
    {
        public Guid UserId { get; set; }
        public string UserEmail { get; set; }
        public string Name { get; set; }
        public string LastName { get; set; }
        public string? Phone { get; set; }
        public UserType UserType { get; set; }
        public DateTime CreatedAt { get; set; }
        public List<AddressDTO> Addresses { get; set; } = new List<AddressDTO>();
    }
}
