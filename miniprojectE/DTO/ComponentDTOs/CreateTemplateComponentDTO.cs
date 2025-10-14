using miniprojectE.Models.Entities;
using System.ComponentModel.DataAnnotations;

namespace miniprojectE.DTO.ComponentDTOs
{
    public class CreateTemplateComponentDTO
    {
        public required int ComponentID { get; set; }

        public required bool isRequired { get; set; }

        [Range(1, int.MaxValue)]
        public required int MinQuantity { get; set; }

        [Range(1, int.MaxValue)]
        public required int MaxQuantity { get; set; }

        public required ComponentRole ComponentRole { get; set; }
    }
}
