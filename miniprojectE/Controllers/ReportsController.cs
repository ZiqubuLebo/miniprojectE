using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using miniprojectE.DTO.ReportingDTOs;
using miniprojectE.DTO.ResponseDTOs;
using miniprojectE.DTO.StockDTOs;
using miniprojectE.Services;

namespace miniprojectE.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize(Roles = "Manager,Admin")]
    public class ReportsController : ControllerBase
    {
         private readonly IReportService _reportService;

        public ReportsController(IReportService reportService)
        {
            _reportService = reportService;
        }

        [HttpGet("GetStockReport")]
        public async Task<ActionResult<ApiResponseDTO<StockReportDTO>>> GetStockReport()
        {
            try
            {
                var report = await _reportService.GetStockReportAsync();
                return Ok(new ApiResponseDTO<StockReportDTO> { Success = true, Data = report });
            }
            catch (Exception ex)
            {
                return BadRequest(new ApiResponseDTO<StockReportDTO> { Success = false, Message = ex.Message });
            }
        }

        [HttpGet("GetPopularityReport")]
        public async Task<ActionResult<ApiResponseDTO<PopularityReportDTO>>> GetPopularityReport(
            [FromQuery] int year = 0,
            [FromQuery] int month = 0)
        {
            try
            {
                if (year == 0) year = DateTime.Now.Year;
                if (month == 0) month = DateTime.Now.Month;

                var report = await _reportService.GetPopularityReportAsync(year, month);
                return Ok(new ApiResponseDTO<PopularityReportDTO> { Success = true, Data = report });
            }
            catch (Exception ex)
            {
                return BadRequest(new ApiResponseDTO<PopularityReportDTO> { Success = false, Message = ex.Message });
            }
        }

        [HttpGet("GetSalesReport")]
        public async Task<ActionResult<ApiResponseDTO<SalesReportDTO>>> GetSalesReport(
            [FromQuery] DateTime? startDate = null,
            [FromQuery] DateTime? endDate = null)
        {
            try
            {
                startDate ??= DateTime.Now.AddDays(-30);
                endDate ??= DateTime.Now;

                var report = await _reportService.GetSalesReportAsync(startDate.Value, endDate.Value);
                return Ok(new ApiResponseDTO<SalesReportDTO> { Success = true, Data = report });
            }
            catch (Exception ex)
            {
                return BadRequest(new ApiResponseDTO<SalesReportDTO> { Success = false, Message = ex.Message });
            }
        }

        [HttpGet("GetLowStockReport")]
        public async Task<ActionResult<ApiResponseDTO<List<StockLevelDTO>>>> GetLowStockReport()
        {
            try
            {
                var report = await _reportService.GetLowStockComponentsAsync();
                return Ok(new ApiResponseDTO<List<StockLevelDTO>> { Success = true, Data = report });
            }
            catch (Exception ex)
            {
                return BadRequest(new ApiResponseDTO<List<StockLevelDTO>> { Success = false, Message = ex.Message });
            }
        }

        [HttpGet("GetComponentUsageHistory/{componentId}")]
        public async Task<ActionResult<ApiResponseDTO<List<ComponentPopularityDTO>>>> GetComponentUsageHistory(
            int componentId,
            [FromQuery] int months = 12)
        {
            try
            {
                var report = await _reportService.GetComponentUsageHistoryAsync(componentId, months);
                return Ok(new ApiResponseDTO<List<ComponentPopularityDTO>> { Success = true, Data = report });
            }
            catch (Exception ex)
            {
                return BadRequest(new ApiResponseDTO<List<ComponentPopularityDTO>> { Success = false, Message = ex.Message });
            }
        }
    }
    }