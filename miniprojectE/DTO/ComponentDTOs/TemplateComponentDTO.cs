using miniprojectE.Models.Entities;

namespace miniprojectE.DTO.ComponentDTOs
{
    public class TemplateComponentDTO
    {
        public int TemplateID { get; set; }
        public int ComponentID { get; set; }
        public string Name { get; set; }
        public ComponentType Type { get; set; }
        public bool isRequired { get; set; }
        public int minLevel { get; set; }
        public int maxLevel { get; set; }
        public ComponentRole ComponentRole { get; set; }
        public decimal UnitPrice { get; set; }
    }
}
