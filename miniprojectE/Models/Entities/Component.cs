using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace miniprojectE.Models.Entities
{
    public enum ComponentType
    {
        Tabletop,
        Leg,
        Drawer,
        Handle,
        Finish,
        Hardware,
        Upholstery,
        Glass,
        Wood,
        Metal
    }
    public class Component
    {
        [Key]
        public int ComponentID { get; set; }
        public required string Name { get; set; }
        public string? Description { get; set; }
        public required ComponentType Type { get; set; }

        [Column(TypeName = "decimal(10,2)")]
        public required decimal UnitPrice { get; set; }
        public required int Level { get; set; }
        public int MinimumLevel { get; set; } = 0;
        public string? Image {  get; set; }
        public string? LastIpdate { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
        public DateTime UpdatedAt { get; set; } = DateTime.UtcNow;

        public virtual ICollection<ComponentCompatibility> CompatibilitiesAsA { get; set; } = new List<ComponentCompatibility>();
        public virtual ICollection<ComponentCompatibility> CompatibilitiesAsB { get; set; } = new List<ComponentCompatibility>();
        public virtual ICollection<TemplateComponent> TemplateComponents { get; set; } = new List<TemplateComponent>();
        public virtual ICollection<ItemComponent> ItemComponents { get; set; } = new List<ItemComponent>();
        public virtual ICollection<Stock> StockMovements { get; set; } = new List<Stock>();
        public virtual ICollection<PurchaseOrderItem> PurchaseOrderItems { get; set; } = new List<PurchaseOrderItem>();
        public virtual ICollection<ComponentPopularity> PopularityRecords { get; set; } = new List<ComponentPopularity>();
        public bool IsActive { get; set; }
    }
}
