//using FurnitureStore.DTOs;
using System.ComponentModel.DataAnnotations;

namespace miniprojectE.DTO.OrderDTOs
{
    public class CreatePurchaseOrderDTO
    {
        [Required]
        public string SupplierName { get; set; }

        public DateTime? ExpectedDeliveryDate { get; set; }

        [Required]
        public List<CreatePurchaseOrderItemDTO> Items { get; set; } = new List<CreatePurchaseOrderItemDTO>();
    }
}
