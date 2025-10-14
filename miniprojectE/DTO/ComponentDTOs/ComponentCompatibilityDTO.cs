using miniprojectE.Models.Entities;

namespace miniprojectE.DTO.ComponentDTOs
{
    public class ComponentCompatibilityDTO
    {
        public int CompatibilityId { get; set; }
        public int ComponentAId { get; set; }
        public int ComponentBId { get; set; }
        public string ComponentAName { get; set; }
        public string ComponentBName { get; set; }
        public bool IsCompatible { get; set; }
        public string CompatibilityNotes { get; set; }
        public ComponentType ComponentAType { get; set; }
        public ComponentType ComponentBType { get; set; }
        public string CompatibilityStatus => IsCompatible ? "Compatible" : "Not Compatible";
    }
}
