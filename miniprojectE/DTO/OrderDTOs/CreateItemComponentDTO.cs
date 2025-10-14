using System.ComponentModel.DataAnnotations;

namespace miniprojectE.DTO.OrderDTOs
{
    public class CreateItemComponentDTO
    {
        [Required]
        public int ComponentId { get; set; }

        [Required]
        [Range(1, int.MaxValue)]
        public int QuantityUsed { get; set; }
    }
}
