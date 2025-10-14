using System.ComponentModel.DataAnnotations;

namespace miniprojectE.DTO.OrderDTOs
{
    public class AssignClerkDTO
    {
        [Required]
        public Guid ClerkId { get; set; }
    }
}
