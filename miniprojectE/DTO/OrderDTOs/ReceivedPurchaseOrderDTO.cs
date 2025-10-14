//using FurnitureStore.DTOs;
using System.ComponentModel.DataAnnotations;

namespace miniprojectE.DTO.OrderDTOs
{
    public class ReceivedPurchaseOrderDTO
    {
        [Required]
        public List<ReceivePurchaseOrderItemDTO> Items { get; set; } = new List<ReceivePurchaseOrderItemDTO>();
    }
}
