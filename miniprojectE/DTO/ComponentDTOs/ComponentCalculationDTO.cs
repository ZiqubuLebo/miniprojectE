namespace miniprojectE.DTO.ComponentDTOs
{
    public class ComponentCalculationDTO
    {
        public int ComponentId { get; set; }
        public string ComponentName { get; set; }
        public int QuantityUsed { get; set; }
        public decimal UnitPrice { get; set; }
        public decimal LineTotal { get; set; }
    }
}
