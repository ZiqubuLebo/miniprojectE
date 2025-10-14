using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace miniprojectE.Models.Entities
{
    public class OrderHistoryLog
    {
        [Key]
        public int LogID { get; set; }

        public required int OrderID { get; set; }

        public required Guid UserID { get; set; }

        public required OrderStatus StatusFrom { get; set; }

        public required OrderStatus StatusTo { get; set; }

        [StringLength(500)]
        public string Notes { get; set; }

        public DateTime ChangedAt { get; set; } = DateTime.UtcNow;

        [ForeignKey("OrderID")]
        public virtual Orders Order { get; set; }

        [ForeignKey("UserID")]
        public virtual Users User { get; set; }
    }
}
