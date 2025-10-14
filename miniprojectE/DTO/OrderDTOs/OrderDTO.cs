//using FurnitureStore.DTOs;
using miniprojectE.DTO.AddressDTOs;
using miniprojectE.Models.Entities;

namespace miniprojectE.DTO.OrderDTOs
{
    public class OrderDTO
    {
        public int OrderId { get; set; }
        public string OrderNumber { get; set; }
        public Guid CustomerId { get; set; }
        public string CustomerName { get; set; }
        public string CustomerEmail { get; set; }
        public Guid? AssignedClerkId { get; set; }
        public string AssignedClerkName { get; set; }
        public OrderStatus OrderStatus { get; set; }
        public decimal Subtotal { get; set; }
        public decimal ShippingCost { get; set; }
        public decimal TotalAmount { get; set; }
        public AddressDTO ShippingAddress { get; set; }
        public string SpecialInstructions { get; set; }
        public DateTime OrderDate { get; set; }
        public DateTime? ExpectedCompletionDate { get; set; }
        public DateTime? CompletedDate { get; set; }
        public List<CustomFurnitureItemDTO> Items { get; set; } = new List<CustomFurnitureItemDTO>();
    }
}
