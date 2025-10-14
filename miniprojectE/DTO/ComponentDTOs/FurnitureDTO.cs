//using FurnitureStore.DTOs;
using miniprojectE.Models.Entities;

namespace miniprojectE.DTO.ComponentDTOs
{
    public class FurnitureDTO
    {
        public int TemplateID { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public FurnitureType FurnitureType { get; set; }
        public decimal Price { get; set; }
        //public bool IsActive { get; set; }
        public List<TemplateComponentDTO> TemplateComponents { get; set; } = new List<TemplateComponentDTO>();
    }
}
