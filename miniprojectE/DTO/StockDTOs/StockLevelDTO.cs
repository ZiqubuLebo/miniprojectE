using miniprojectE.Models.Entities;
using System.ComponentModel.DataAnnotations;

namespace miniprojectE.DTO.StockDTOs
{
    public class StockLevelDTO
    {
        public int ComponentId { get; set; }

        public string ComponentName { get; set; }

        public ComponentType ComponentType { get; set; }

        public int CurrentStock { get; set; }

        public int MinimumStockLevel { get; set; }

        public int? MaximumStockLevel { get; set; }

        public int ReorderPoint { get; set; }

        public int ReorderQuantity { get; set; }

        public bool IsLowStock => CurrentStock <= MinimumStockLevel;

        public bool IsOutOfStock => CurrentStock <= 0;

        public bool IsAtReorderPoint => CurrentStock <= ReorderPoint;

        [DisplayFormat(DataFormatString = "{0:C}", ApplyFormatInEditMode = false)]
        public decimal UnitPrice { get; set; }

        [DisplayFormat(DataFormatString = "{0:C}", ApplyFormatInEditMode = false)]
        public decimal TotalInventoryValue => CurrentStock * UnitPrice;

        public decimal AverageMonthlyUsage { get; set; }

        public int? EstimatedDaysUntilStockOut
        {
            get
            {
                if (AverageMonthlyUsage <= 0 || CurrentStock <= 0) return null;
                var dailyUsage = AverageMonthlyUsage / 30;
                return (int)(CurrentStock / dailyUsage);
            }
        }

        public bool IsActive { get; set; }

        public DateTime? LastMovementDate { get; set; }

        public string PrimarySupplier { get; set; }

        public int LeadTimeDays { get; set; }

        public string StockStatus
        {
            get
            {
                if (IsOutOfStock) return "Out of Stock";
                if (IsLowStock) return "Low Stock";
                if (IsAtReorderPoint) return "Reorder Point";
                return "In Stock";
            }
        }

        public string StatusColor
        {
            get
            {
                if (IsOutOfStock) return "red";
                if (IsLowStock) return "orange";
                if (IsAtReorderPoint) return "yellow";
                return "green";
            }
        }
    }
}
