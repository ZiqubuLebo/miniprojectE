using miniprojectE.DTO.ReportingDTOs;
using miniprojectE.DTO.StockDTOs;

namespace miniprojectE.Services
{
    public interface IReportService
    {
        Task<StockReportDTO> GetStockReportAsync();
        Task<PopularityReportDTO> GetPopularityReportAsync(int year, int month);
        Task<SalesReportDTO> GetSalesReportAsync(DateTime startDate, DateTime endDate);
        Task<List<StockLevelDTO>> GetLowStockComponentsAsync();
        Task<List<ComponentPopularityDTO>> GetComponentUsageHistoryAsync(int componentId, int months);
        Task GenerateMonthlyPopularityDataAsync();
    }
}
