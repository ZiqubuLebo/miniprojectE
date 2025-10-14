using miniprojectE.Models.Entities;
using System.ComponentModel.DataAnnotations;

namespace miniprojectE.DTO.ReportingDTOs
{
    public class ComponentPopularityDTO
    {
        public int ComponentId { get; set; }
        public string ComponentName { get; set; }
        public ComponentType ComponentType { get; set; }
        public int TimesOrdered { get; set; }
        public int TotalQuantitySold { get; set; }
        [DisplayFormat(DataFormatString = "{0:C}", ApplyFormatInEditMode = false)]
        public decimal TotalRevenue { get; set; }
        public int PeriodYear { get; set; }
        [Range(1, 12)]
        public int PeriodMonth { get; set; }
        public decimal AverageQuantityPerOrder => TimesOrdered > 0 ? (decimal)TotalQuantitySold / TimesOrdered : 0;
        [DisplayFormat(DataFormatString = "{0:C}", ApplyFormatInEditMode = false)]
        public decimal AverageRevenuePerOrder => TimesOrdered > 0 ? TotalRevenue / TimesOrdered : 0;
        [DisplayFormat(DataFormatString = "{0:C}", ApplyFormatInEditMode = false)]
        public decimal CurrentUnitPrice { get; set; }
        [DisplayFormat(DataFormatString = "{0:C}", ApplyFormatInEditMode = false)]
        public decimal AverageSellingPrice { get; set; }
        public int PopularityRank { get; set; }
        public decimal? PercentageChange { get; set; }
        public decimal MarketSharePercentage { get; set; }
        public bool IsActive { get; set; }
        public string PeriodDisplay => $"{GetMonthName(PeriodMonth)} {PeriodYear}";
        public string TrendIndicator
        {
            get
            {
                if (!PercentageChange.HasValue) return "No Data";
                if (PercentageChange > 10) return "Rising Fast";
                if (PercentageChange > 0) return "Rising";
                if (PercentageChange < -10) return "Declining Fast";
                if (PercentageChange < 0) return "Declining";
                return "Stable";
            }
        }

        private string GetMonthName(int month) => month switch
        {
            1 => "January",
            2 => "February",
            3 => "March",
            4 => "April",
            5 => "May",
            6 => "June",
            7 => "July",
            8 => "August",
            9 => "September",
            10 => "October",
            11 => "November",
            12 => "December",
            _ => "Unknown"
        };
    }
}
