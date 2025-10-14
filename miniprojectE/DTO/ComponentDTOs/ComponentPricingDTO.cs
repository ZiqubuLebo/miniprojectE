namespace miniprojectE.DTO.ComponentDTOs
{
    public class ComponentPricingDTO
    {
        public int ComponentId { get; set; }
        public string ComponentName { get; set; }
        public int Quantity { get; set; }
        public decimal UnitPrice { get; set; }
        public decimal LineTotal { get; set; }
    }
}
