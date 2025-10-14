using miniprojectE.Models.Entities;

namespace miniprojectE.DTO.ComponentDTOs
{
    public class ComponentSearchDTO
    {
        public string SearchTerm { get; set; }
        public ComponentType? ComponentType { get; set; }
        public decimal? MinPrice { get; set; }
        public decimal? MaxPrice { get; set; }
        public bool? InStock { get; set; }
        public bool? IsActive { get; set; }
    }
}
