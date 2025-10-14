using miniprojectE.Models.Entities;
using System.ComponentModel.DataAnnotations;

namespace miniprojectE.DTO.OrderDTOs
{
    public class OrderSummaryDTO
    {
        public int OrderId { get; set; }
        public string OrderNumber { get; set; }
        public Guid CustomerId { get; set; }
        public string CustomerName { get; set; }
        public string CustomerEmail { get; set; }
        public string CustomerPhone { get; set; }
        public OrderStatus Status { get; set; }
        public string StatusDisplay { get; set; }
        [DisplayFormat(DataFormatString = "{0:C}", ApplyFormatInEditMode = false)]
        public decimal TotalAmount { get; set; }
        [DisplayFormat(DataFormatString = "{0:C}", ApplyFormatInEditMode = false)]
        public decimal Subtotal { get; set; }
        [DisplayFormat(DataFormatString = "{0:C}", ApplyFormatInEditMode = false)]
        public decimal TaxAmount { get; set; }
        public DateTime OrderDate { get; set; }
        public DateTime? ExpectedCompletionDate { get; set; }
        public DateTime? CompletedDate { get; set; }
        public int ItemCount { get; set; }
        public int TotalComponentCount { get; set; }
        public int? AssignedClerkId { get; set; }
        public string AssignedClerkName { get; set; }
        public int DaysSinceOrdered => (DateTime.Now - OrderDate).Days;
        public bool IsOverdue => ExpectedCompletionDate.HasValue &&
                                 DateTime.Now > ExpectedCompletionDate.Value &&
                                 Status != OrderStatus.Completed &&
                                 Status != OrderStatus.Cancelled;
        public string Priority
        {
            get
            {
                if (IsOverdue) return "High";
                if (DaysSinceOrdered > 7 && Status == OrderStatus.Pending) return "Medium";
                return "Normal";
            }
        }
        public string ShippingAddressSummary { get; set; }
    }
}
