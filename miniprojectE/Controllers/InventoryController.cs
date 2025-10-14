using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace miniprojectE.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize(Roles = "Clerk,Manager,Admin")]
    public class InventoryController : ControllerBase
    {
        /**  private readonly IInventoryService _inventoryService;

        public InventoryController(IInventoryService inventoryService)
        {
            _inventoryService = inventoryService;
        }

        [HttpGet("movements")]
        public async Task<ActionResult<PaginatedResponse<StockMovementDto>>> GetStockMovements(
            [FromQuery] int page = 1,
            [FromQuery] int pageSize = 50,
            [FromQuery] int? componentId = null,
            [FromQuery] MovementType? movementType = null,
            [FromQuery] DateTime? startDate = null,
            [FromQuery] DateTime? endDate = null)
        {
            try
            {
                var result = await _inventoryService.GetStockMovementsAsync(page, pageSize, componentId, movementType, startDate, endDate);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(new PaginatedResponse<StockMovementDto>());
            }
        }

        [HttpGet("movements/component/{componentId}")]
        public async Task<ActionResult<ApiResponse<List<StockMovementDto>>>> GetComponentStockHistory(int componentId)
        {
            try
            {
                var movements = await _inventoryService.GetComponentStockHistoryAsync(componentId);
                return Ok(new ApiResponse<List<StockMovementDto>> { Success = true, Data = movements });
            }
            catch (Exception ex)
            {
                return BadRequest(new ApiResponse<List<StockMovementDto>> { Success = false, Message = ex.Message });
            }
        }
    }*/
    }
}
