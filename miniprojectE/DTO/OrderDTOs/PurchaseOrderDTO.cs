//using FurnitureStore.DTOs;
using miniprojectE.Models.Entities;

namespace miniprojectE.DTO.OrderDTOs
{
    public class PurchaseOrderDTO
    {
        public int PurchaseOrderId { get; set; }
        public string SupplierName { get; set; }
        public PurchaseOrderStatus Status { get; set; }
        public decimal TotalCost { get; set; }
        public DateTime OrderDate { get; set; }
        public DateTime? ExpectedDeliveryDate { get; set; }
        public DateTime? ReceivedDate { get; set; }
        public string ManagerName { get; set; }
        public List<PurchaseOrderItemDTO> Items { get; set; } = new List<PurchaseOrderItemDTO>();
    }
}
