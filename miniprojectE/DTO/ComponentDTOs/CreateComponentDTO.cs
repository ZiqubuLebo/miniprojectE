using miniprojectE.Models.Entities;
using System.ComponentModel.DataAnnotations;

namespace miniprojectE.DTO.ComponentDTOs
{
    public class CreateComponentDTO
    {
        public required string ComponentName { get; set; }

        public string Description { get; set; }

        [Range(0.01, double.MaxValue)]
        public required decimal UnitPrice { get; set; }

        [Range(0, int.MaxValue)]
        public required int StockQuantity { get; set; }

        [Range(0, int.MaxValue)]
        public int MinimumStockLevel { get; set; }

        [Required]
        public ComponentType ComponentType { get; set; }

        public string ImageUrl { get; set; }
    }
}
