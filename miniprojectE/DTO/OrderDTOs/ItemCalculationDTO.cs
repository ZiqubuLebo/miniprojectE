using miniprojectE.DTO.ComponentDTOs;

namespace miniprojectE.DTO.OrderDTOs
{
    public class ItemCalculationDTO
    {
        public string ItemName { get; set; }
        public int Quantity { get; set; }
        public decimal ItemUnitPrice { get; set; }
        public decimal ItemTotalPrice { get; set; }
        public List<ComponentCalculationDTO> ComponentCalculations { get; set; } = new List<ComponentCalculationDTO>();
    }
}