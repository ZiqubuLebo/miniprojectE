using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using miniprojectE.DTO.OrderDTOs;
using miniprojectE.Models.Entities;
using miniprojectE.Services;

namespace miniprojectE.Controllers
{
    [ApiController]
    [Route("api/purchase-orders")]
    [Authorize(Roles = "Manager,Admin")]
    public class PurchaseOrdersController : ControllerBase
    {
       /** private readonly IPurchaseOrderService _purchaseOrderService;

        public PurchaseOrdersController(IPurchaseOrderService purchaseOrderService)
        {
            _purchaseOrderService = purchaseOrderService;
        }

        [HttpGet]
        public async Task<ActionResult<PaginatedResponse<PurchaseOrderDto>>> GetPurchaseOrders(
            [FromQuery] int page = 1,
            [FromQuery] int pageSize = 20,
            [FromQuery] PurchaseOrderStatus? status = null)
        {
            try
            {
                var result = await _purchaseOrderService.GetPurchaseOrdersAsync(page, pageSize, status);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(new PaginatedResponse<PurchaseOrderDto>());
            }
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<ApiResponse<PurchaseOrderDto>>> GetPurchaseOrder(int id)
        {
            try
            {
                var purchaseOrder = await _purchaseOrderService.GetPurchaseOrderAsync(id);
                return Ok(new ApiResponse<PurchaseOrderDto> { Success = true, Data = purchaseOrder });
            }
            catch (Exception ex)
            {
                return NotFound(new ApiResponse<PurchaseOrderDto> { Success = false, Message = ex.Message });
            }
        }

        [HttpPost]
        public async Task<ActionResult<ApiResponse<PurchaseOrderDto>>> CreatePurchaseOrder([FromBody] CreatePurchaseOrderDto dto)
        {
            try
            {
                var result = await _purchaseOrderService.CreatePurchaseOrderAsync(dto);
                return CreatedAtAction(nameof(GetPurchaseOrder), new { id = result.PurchaseOrderId },
                    new ApiResponse<PurchaseOrderDto> { Success = true, Data = result });
            }
            catch (Exception ex)
            {
                return BadRequest(new ApiResponse<PurchaseOrderDto> { Success = false, Message = ex.Message });
            }
        }

        [HttpPost("{id}/receive")]
        public async Task<ActionResult<ApiResponse<PurchaseOrderDto>>> ReceivePurchaseOrder(int id, [FromBody] ReceivePurchaseOrderDto dto)
        {
            try
            {
                var result = await _purchaseOrderService.ReceivePurchaseOrderAsync(id, dto);
                return Ok(new ApiResponse<PurchaseOrderDto> { Success = true, Data = result });
            }
            catch (Exception ex)
            {
                return BadRequest(new ApiResponse<PurchaseOrderDto> { Success = false, Message = ex.Message });
            }
        }*/
    }
}
