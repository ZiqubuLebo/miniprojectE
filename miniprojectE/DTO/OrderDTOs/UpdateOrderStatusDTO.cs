using miniprojectE.Models.Entities;
using System.ComponentModel.DataAnnotations;

namespace miniprojectE.DTO.OrderDTOs
{
    public class UpdateOrderStatusDTO
    {
        [Required]
        public OrderStatus NewStatus { get; set; }

        public string Notes { get; set; }
    }
}
