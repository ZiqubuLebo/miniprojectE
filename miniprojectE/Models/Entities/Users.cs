using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;

namespace miniprojectE.Models.Entities
{
    public enum UserType
    {
        Customer,
        Clerk,
        Manager,
        Admin
    }
    public class Users
    {
        [Key]
        public Guid UserID { get; set; }
        public required string Name { get; set; }
        public required string LastName { get; set; }
        public required string UserEmail { get; set; }
        public required string Password { get; set; }
        public string? Phone { get; set; }
        public string? Address { get; set; }
        public DateTime RegistrationDate { get; set; } = DateTime.UtcNow;
        public DateTime UpdatedAt { get; set; } = DateTime.UtcNow;
        public required UserType Role { get; set; }
        public required bool IsActive { get; set; }

        //public ICollection<Order> Orders { get; set; }
        //public ICollection<OrderProgress> OrdersProgress { get; set; }
        public virtual ICollection<Address> Addresses { get; set; } = new List<Address>();
        public virtual ICollection<Orders> CustomerOrders { get; set; } = new List<Orders>();
        public virtual ICollection<Orders> AssignedOrders { get; set; } = new List<Orders>();
        public virtual ICollection<Stock> StockMovements { get; set; } = new List<Stock>();
        public virtual ICollection<PurchaseOrder> PurchaseOrders { get; set; } = new List<PurchaseOrder>();
    }
}
