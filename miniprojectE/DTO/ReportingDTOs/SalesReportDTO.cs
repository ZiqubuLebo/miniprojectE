using miniprojectE.DTO.OrderDTOs;

namespace miniprojectE.DTO.ReportingDTOs
{
    public class SalesReportDTO
    {
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public int TotalOrders { get; set; }
        public decimal TotalRevenue { get; set; }
        public decimal AverageOrderValue { get; set; }
        public List<OrderSummaryDTO> RecentOrders { get; set; } = new List<OrderSummaryDTO>();
        public List<ComponentPopularityDTO> TopComponents { get; set; } = new List<ComponentPopularityDTO>();
    }
}
