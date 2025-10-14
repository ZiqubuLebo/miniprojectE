using miniprojectE.Models.Entities;
using System.ComponentModel.DataAnnotations;

namespace miniprojectE.DTO.ComponentDTOs
{
    public class CreateTemplateDTO
    {
        public required string Name { get; set; }

        public string? Description { get; set; }

        [Required]
        public FurnitureType FurnitureType { get; set; }

        [Range(0, double.MaxValue)]
        public decimal BasePrice { get; set; }
    }
}
