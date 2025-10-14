using System.ComponentModel.DataAnnotations;

namespace miniprojectE.DTO.OrderDTOs
{
    public class CreatePurchaseOrderItemDTO
    {
        [Required]
        public int ComponentId { get; set; }

        [Required]
        [Range(1, int.MaxValue)]
        public int QuantityOrdered { get; set; }

        [Required]
        [Range(0.01, double.MaxValue)]
        public decimal UnitCost { get; set; }
    }
}
