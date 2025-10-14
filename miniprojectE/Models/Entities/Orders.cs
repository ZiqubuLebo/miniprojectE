using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace miniprojectE.Models.Entities
{
    public enum OrderStatus
    {
        Pending,
        Confirmed,
        Assembling,
        Assembled,
        Shipped,
        Delivered,
        Completed,
        Cancelled
    }
    public partial class Orders
    {
        [Key]
        public int OrderID { get; set; }

        public required Guid CustomerID { get; set; }

        public Guid? ClerkID { get; set; }

        public required string OrderNumber { get; set; }

        public required OrderStatus OrderStatus { get; set; } = OrderStatus.Pending;

        [Column(TypeName = "decimal(12,2)")]
        public required decimal Subtotal { get; set; }


        [Column(TypeName = "decimal(12,2)")]
        public decimal ShippingCost { get; set; } = 0;

        [Column(TypeName = "decimal(12,2)")]
        public required decimal TotalAmount { get; set; }
        public required int AddressID { get; set; }


        [StringLength(1000)]
        public string? SpecialInstructions { get; set; }

        public DateTime OrderDate { get; set; } = DateTime.UtcNow;
        public DateTime? ExpectedCompletionDate { get; set; }
        public DateTime? CompletedDate { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
        public DateTime UpdatedAt { get; set; } = DateTime.UtcNow;

        [ForeignKey("CustomerID")]
        public virtual Users Customer { get; set; }

        [ForeignKey("ClerkID")]
        public virtual Users Clerk { get; set; }

        [ForeignKey("AddressID")]
        public virtual Address ShippingAddress { get; set; }

        public virtual ICollection<OrderItem> CustomFurnitureItems { get; set; } = new List<OrderItem>();
        public virtual ICollection<OrderHistoryLog> OrderHistoryLogs { get; set; } = new List<OrderHistoryLog>();
    }
}