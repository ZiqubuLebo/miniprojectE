using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace miniprojectE.Models.Entities
{
    public class Address
    {
        [Key]
        public int AddressID { get; set; }
        public Guid UserID { get; set; }
        public required string Street { get; set; }
        public required string City { get; set; }
        public required string Province { get; set; }
        public required string Code { get; set; }
        public required string Country { get; set; }
        [ForeignKey("UserID")]
        public virtual Users User { get; set; }
        public virtual ICollection<Orders> ShippingOrders { get; set; } = new List<Orders>();
        //public virtual ICollection<Order> BillingOrders { get; set; } = new List<Order>();
    }
}
