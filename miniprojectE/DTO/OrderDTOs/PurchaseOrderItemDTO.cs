namespace miniprojectE.DTO.OrderDTOs
{
    public class PurchaseOrderItemDTO
    {
        public int PurchaseItemId { get; set; }
        public int ComponentId { get; set; }
        public string ComponentName { get; set; }
        public string ComponentSku { get; set; }
        public int QuantityOrdered { get; set; }
        public int QuantityReceived { get; set; }
        public decimal UnitCost { get; set; }
        public decimal LineTotal { get; set; }
    }
}
