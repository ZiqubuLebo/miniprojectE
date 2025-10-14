using miniprojectE.DTO.StockDTOs;
namespace miniprojectE.DTO.ReportingDTOs
{
    public class StockReportDTO
    {
        public List<StockLevelDTO> LowStockItems { get; set; } = new List<StockLevelDTO>();
        public List<StockLevelDTO> AllComponents { get; set; } = new List<StockLevelDTO>();
        public int TotalComponents { get; set; }
        public int LowStockCount { get; set; }
        public DateTime GeneratedAt { get; set; }
    }
}
