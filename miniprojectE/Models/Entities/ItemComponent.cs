using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace miniprojectE.Models.Entities
{
    public class ItemComponent
    {
        [Key]
        public int ItemComponentID { get; set; }

        public required int ItemID { get; set; }

        public required int ComponentID { get; set; }

        public required int QuantityUsed { get; set; }

        [Column(TypeName = "decimal(10,2)")]
        public required decimal UnitPriceAtOrder { get; set; }

        [Column(TypeName = "decimal(12,2)")]
        public required decimal LineTotal { get; set; }

        // Navigation Properties
        [ForeignKey("ItemID")]
        public virtual OrderItem Item { get; set; }

        [ForeignKey("ComponentID")]
        public virtual Component Component { get; set; }
    }
}
