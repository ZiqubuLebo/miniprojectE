using System.ComponentModel.DataAnnotations;

namespace miniprojectE.DTO.ComponentDTOs
{
    public class UpdateComponentDTO
    {
        public required string Name { get; set; }

        public string? Description { get; set; }

        [Range(0.01, double.MaxValue)]
        public required decimal UnitPrice { get; set; }

        [Range(0, int.MaxValue)]
        public int MinimumStockLevel { get; set; }

        public string? Image { get; set; }

        public bool IsActive { get; set; }
    }
}
