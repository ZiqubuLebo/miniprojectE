using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace miniprojectE.Models.Entities
{
    public enum MovementType
    {
        Purchase,
        Sale,
        Adjustment,
        AssemblyUse
    }
   
    public class Stock
    {
        [Key]
        public int StockID { get; set; }
        public required int ComponentID { get; set; }
        public required Guid UserID { get; set; }
        public required string Type { get; set; }

        public required MovementType MovementType { get; set; }

        public required int QuantityChange { get; set; }

        public required int QuantityBefore { get; set; }

        public required int QuantityAfter { get; set; }

        [StringLength(50)]
        public string ReferenceId { get; set; }

        [StringLength(500)]
        public string Notes { get; set; }

        public DateTime MovementDate { get; set; } = DateTime.UtcNow;

        [ForeignKey("ComponentID")]
        public virtual Component Component { get; set; }

        [ForeignKey("UserID")]
        public virtual Users User { get; set; }
    }
}
