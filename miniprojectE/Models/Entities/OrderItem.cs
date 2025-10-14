using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace miniprojectE.Models.Entities
{
    public enum AssemblyStatus
    {
        Pending,
        InProgress,
        Completed
    }
    public class OrderItem
    {
        [Key]
        public int OrderItemID { get; set; }
        public required int OrderID { get; set; }
        public required int TemplateID { get; set; }
        public required string ItemName { get; set; }
        public string? ItemDescription { get; set; }
        public int? Quantity { get; set; }
        [Column(TypeName = "decimal(12,2)")]
        public required decimal ItemTotalPrice { get; set; }
        public string? Details { get; set; }
        [Required]
        public AssemblyStatus AssemblyStatus { get; set; } = AssemblyStatus.Pending;

        public DateTime? AssemblyStartedAt { get; set; }
        public DateTime? AssemblyCompletedAt { get; set; }

        [ForeignKey("OrderID")]
        public virtual Orders Order { get; set; }

        [ForeignKey("TemplateID")]
        public virtual Furniture Template { get; set; }

        public virtual ICollection<ItemComponent> ItemComponents { get; set; } = new List<ItemComponent>();
    }
}
