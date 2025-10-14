using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace miniprojectE.Models.Entities
{
    public enum PurchaseOrderStatus
    {
        Pending,
        Ordered,
        Received,
        Cancelled
    }
    public class PurchaseOrder
    {
        [Key]
        public int PurchaseOrderID { get; set; }

        public required Guid ManagerID { get; set; }

        [StringLength(200)]
        public required string SupplierName { get; set; }

        public required PurchaseOrderStatus Status { get; set; } = PurchaseOrderStatus.Pending;

        [Column(TypeName = "decimal(12,2)")]
        public required decimal TotalCost { get; set; }

        public DateTime OrderDate { get; set; } = DateTime.UtcNow;
        public DateTime? ExpectedDeliveryDate { get; set; }
        public DateTime? ReceivedDate { get; set; }

        [ForeignKey("ManagerID")]
        public virtual Users Manager { get; set; }

        public virtual ICollection<PurchaseOrderItem> PurchaseOrderItems { get; set; } = new List<PurchaseOrderItem>();
    }
}
