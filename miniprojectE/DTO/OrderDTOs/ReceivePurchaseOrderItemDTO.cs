using System.ComponentModel.DataAnnotations;

namespace miniprojectE.DTO.OrderDTOs
{
    public class ReceivePurchaseOrderItemDTO
    {
        [Required]
        public int PurchaseItemId { get; set; }

        [Required]
        [Range(0, int.MaxValue)]
        public int QuantityReceived { get; set; }
    }
}
