using miniprojectE.Models.Entities;
using System.ComponentModel.DataAnnotations;

namespace miniprojectE.DTO.ComponentDTOs
{
    public class UpdateTemplateComponentDTO
    {
        [Required]
        public bool IsRequired { get; set; }

        [Required]
        [Range(1, int.MaxValue)]
        public int MinQuantity { get; set; }

        [Required]
        [Range(1, int.MaxValue)]
        public int MaxQuantity { get; set; }

        [Required]
        public ComponentRole ComponentRole { get; set; }
    }
}
