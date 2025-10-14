namespace miniprojectE.DTO.OrderDTOs
{
    public class OrderCalculationDTO
    {
        public decimal Subtotal { get; set; }
        public decimal ShippingCost { get; set; }
        public decimal TotalAmount { get; set; }
        public List<ItemCalculationDTO> ItemCalculations { get; set; } = new List<ItemCalculationDTO>();
    }
}
