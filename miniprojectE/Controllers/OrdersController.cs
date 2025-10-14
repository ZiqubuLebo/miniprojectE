using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using miniprojectE.DTO.AddressDTOs;
using miniprojectE.DTO.OrderDTOs;
using miniprojectE.DTO.ResponseDTOs;
using miniprojectE.Models.Entities;
using miniprojectE.Services;

namespace miniprojectE.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class OrdersController : ControllerBase
    {
         private readonly IOrderService _orderService;

        public OrdersController(IOrderService orderService)
        {
            _orderService = orderService;
        }

        [HttpGet("GetOrders")]
        //[Authorize(Roles = "Clerk,Manager,Admin")]
        public async Task<ActionResult<CalculationResponseDTO<OrderDTO>>> GetOrders(
            [FromQuery] int page = 1,
            [FromQuery] int pageSize = 20,
            [FromQuery] OrderStatus? status = null,
            [FromQuery] Guid? customerId = null,
            [FromQuery] Guid? clerkId = null)
        {
            try
            {
                var result = await _orderService.GetOrdersAsync(page, pageSize, status, customerId, clerkId);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(new CalculationResponseDTO<OrderDTO>());
            }
        }

        [HttpGet("GetOrder/{id}")]
        public async Task<ActionResult<ApiResponseDTO<OrderDTO>>> GetOrder(int id)
        {
            try
            {
                var order = await _orderService.GetOrderAsync(id);
                return Ok(new ApiResponseDTO<OrderDTO> { Success = true, Data = order });
            }
            catch (Exception ex)
            {
                return NotFound(new ApiResponseDTO<OrderDTO> { Success = false, Message = ex.Message });
            }
        }

        [HttpGet("GetCustomerOrders/{customerId}")]
        public async Task<ActionResult<CalculationResponseDTO<OrderDTO>>> GetCustomerOrders(
            Guid customerId)
        {
            try
            {
                var result = await _orderService.GetCustomerOrdersAsync(customerId);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(new CalculationResponseDTO<OrderDTO>());
            }
        }

        [HttpPost("CreateOrder")]
        public async Task<ActionResult<ApiResponseDTO<OrderDTO>>> CreateOrder([FromBody] CreateOrderDTO dto)
        {
            try
            {
                var result = await _orderService.CreateOrderAsync(dto);
                return CreatedAtAction(nameof(GetOrder), new { id = result.OrderId },
                    new ApiResponseDTO<OrderDTO> { Success = true, Data = result });
            }
            catch (Exception ex)
            {
                return BadRequest(new ApiResponseDTO<OrderDTO> { Success = false, Message = ex.Message });
            }
        }

        [HttpPut("{id}/status")]
        //[Authorize(Roles = "Clerk,Manager,Admin")]
        public async Task<ActionResult<ApiResponseDTO<OrderDTO>>> UpdateOrderStatus(int id, [FromBody] UpdateOrderStatusDTO dto)
        {
            try
            {
                var result = await _orderService.UpdateOrderStatusAsync(id, dto);
                return Ok(new ApiResponseDTO<OrderDTO> { Success = true, Data = result });
            }
            catch (Exception ex)
            {
                return BadRequest(new ApiResponseDTO<OrderDTO> { Success = false, Message = ex.Message });
            }
        }

        [HttpPut("{id}/AssignClerk")]
        //[Authorize(Roles = "Manager,Admin")]
        public async Task<ActionResult<ApiResponseDTO<OrderDTO>>> AssignClerk(int id, [FromBody] AssignClerkDTO dto)
        {
            try
            {
                var result = await _orderService.AssignClerkAsync(id, dto.ClerkId);
                return Ok(new ApiResponseDTO<OrderDTO> { Success = true, Data = result });
            }
            catch (Exception ex)
            {
                return BadRequest(new ApiResponseDTO<OrderDTO> { Success = false, Message = ex.Message });
            }
        }

        [HttpPost("{id}/StartAssembly")]
        //[Authorize(Roles = "Clerk,Manager,Admin")]
        public async Task<ActionResult<ApiResponseDTO<OrderDTO>>> StartAssembly(int id)
        {
            try
            {
                var result = await _orderService.StartAssemblyAsync(id);
                return Ok(new ApiResponseDTO<OrderDTO> { Success = true, Data = result });
            }
            catch (Exception ex)
            {
                return BadRequest(new ApiResponseDTO<OrderDTO> { Success = false, Message = ex.Message });
            }
        }

        [HttpPost("{id}/CompleteAssembly")]
        //[Authorize(Roles = "Clerk,Manager,Admin")]
        public async Task<ActionResult<ApiResponseDTO<OrderDTO>>> CompleteAssembly(int id)
        {
            try
            {
                var result = await _orderService.CompleteAssemblyAsync(id);
                return Ok(new ApiResponseDTO<OrderDTO> { Success = true, Data = result });
            }
            catch (Exception ex)
            {
                return BadRequest(new ApiResponseDTO<OrderDTO> { Success = false, Message = ex.Message });
            }
        }

        [HttpPost("{id}/ship")]
        //[Authorize(Roles = "Clerk,Manager,Admin")]
        public async Task<ActionResult<ApiResponseDTO<OrderDTO>>> ShipOrder(int id)
        {
            try
            {
                var result = await _orderService.ShipOrderAsync(id);
                return Ok(new ApiResponseDTO<OrderDTO> { Success = true, Data = result });
            }
            catch (Exception ex)
            {
                return BadRequest(new ApiResponseDTO<OrderDTO> { Success = false, Message = ex.Message });
            }
        }

        [HttpPost("{id}/complete")]
        //[Authorize(Roles = "Clerk,Manager,Admin")]
        public async Task<ActionResult<ApiResponseDTO<OrderDTO>>> CompleteOrder(int id)
        {
            try
            {
                var result = await _orderService.CompleteOrderAsync(id);
                return Ok(new ApiResponseDTO<OrderDTO> { Success = true, Data = result });
            }
            catch (Exception ex)
            {
                return BadRequest(new ApiResponseDTO<OrderDTO> { Success = false, Message = ex.Message });
            }
        }

        [HttpDelete("CancelOrder/{id}")]
        //[Authorize(Roles = "Manager,Admin")]
        public async Task<ActionResult<ApiResponseDTO<bool>>> CancelOrder(int id)
        {
            try
            {
                await _orderService.CancelOrderAsync(id);
                return Ok(new ApiResponseDTO<bool> { Success = true, Data = true });
            }
            catch (Exception ex)
            {
                return BadRequest(new ApiResponseDTO<bool> { Success = false, Message = ex.Message });
            }
        }

        [HttpGet("GetAllOrders")]
        //[Authorize(Roles = "Clerk,Manager,Admin")]
        public async Task<ActionResult<ApiResponseDTO<List<OrderDTO>>>> GetAllOrders()
        {
            try
            {
                var result = await _orderService.GetAllOrdersAsync();
                return Ok(new ApiResponseDTO<List<OrderDTO>> { Success = true, Data = result });
            }
            catch (Exception ex)
            {
                return BadRequest(new List<OrderDTO>());
            }
        }
    }
}
