using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace miniprojectE.Models.Entities
{
    public class PurchaseOrderItem
    {
        [Key]
        public int PurchaseItemID { get; set; }

        public required int PurchaseOrderID { get; set; }

        public required int ComponentID { get; set; }

        public required int QuantityOrdered { get; set; }

        public int? QuantityReceived { get; set; } 

        [Column(TypeName = "decimal(10,2)")]
        public required decimal UnitCost { get; set; }

        [Column(TypeName = "decimal(12,2)")]
        public required decimal LineTotal { get; set; }

        [ForeignKey("PurchaseOrderID")]
        public virtual PurchaseOrder PurchaseOrder { get; set; }

        [ForeignKey("ComponentID")]
        public virtual Component Component { get; set; }
    }
}
