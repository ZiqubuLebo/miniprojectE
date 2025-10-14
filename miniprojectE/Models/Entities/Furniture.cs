using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace miniprojectE.Models.Entities
{
    public enum FurnitureType
    {
        Table,
        Chair,
        Desk,
        Cabinet,
        Shelf,
        Bed,
        Dresser,
        Nightstand
    }
    public class Furniture
    {
        [Key]
        public int FurnitureID { get; set; }
        public required string Name { get; set; }
        public required string Description { get; set; }
        public required FurnitureType FurnitureType { get; set; }

        [Column(TypeName = "decimal(10,2)")]
        public decimal BasePrice { get; set; } = 0;

        public bool IsActive { get; set; } = true;
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

        // Navigation Properties
        public virtual ICollection<TemplateComponent> TemplateComponents { get; set; } = new List<TemplateComponent>();
        public virtual ICollection<OrderItem> CustomFurnitureItems { get; set; } = new List<OrderItem>();
    }
}
