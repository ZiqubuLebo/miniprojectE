using miniprojectE.Models.Entities;

namespace miniprojectE.DTO.ComponentDTOs
{
    public class ComponentDTO
    {
        public int ComponentId { get; set; }
        public string ComponentName { get; set; }
        public string Description { get; set; }
        public decimal UnitPrice { get; set; }
        public int StockQuantity { get; set; }
        public int MinimumStockLevel { get; set; }
        public ComponentType ComponentType { get; set; }
        public string ImageUrl { get; set; }
        public bool IsActive { get; set; }
        public bool IsLowStock { get; set; }
    }
}
