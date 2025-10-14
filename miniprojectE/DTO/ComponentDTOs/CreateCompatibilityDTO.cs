using System.ComponentModel.DataAnnotations;

namespace miniprojectE.DTO.ComponentDTOs
{
    public class CreateCompatibilityDTO
    {
        public required int ComponentAId { get; set; }

        public required int ComponentBId { get; set; }

        public required bool IsCompatible { get; set; }

        public string CompatibilityNotes { get; set; }
    }
}
