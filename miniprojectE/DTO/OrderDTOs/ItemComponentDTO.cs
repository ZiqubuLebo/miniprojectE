using miniprojectE.Models.Entities;

namespace miniprojectE.DTO.OrderDTOs
{
    public class ItemComponentDTO
    {
        public int ComponentId { get; set; }
        public string ComponentName { get; set; }
        public ComponentType ComponentType { get; set; }
        public int QuantityUsed { get; set; }
        public decimal UnitPrice { get; set; }
        public decimal LineTotal { get; set; }
    }
}
