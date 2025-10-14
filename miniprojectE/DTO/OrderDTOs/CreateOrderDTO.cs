using miniprojectE.DTO.OrderDTOs;
using System.ComponentModel.DataAnnotations;

namespace miniprojectE.DTO.OrderDTOs
{
    public class CreateOrderDTO
    {
        [Required]
        public int ShippingAddressId { get; set; }

        [Required]
        public int BillingAddressId { get; set; }

        public string SpecialInstructions { get; set; }

        [Required]
        public List<CreateCustomFurnitureItemDTO> Items { get; set; } = new List<CreateCustomFurnitureItemDTO>();
    }
}
