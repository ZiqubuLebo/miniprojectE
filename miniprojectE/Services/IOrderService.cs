using miniprojectE.DTO.OrderDTOs;
using miniprojectE.DTO.ResponseDTOs;
using miniprojectE.Models.Entities;

namespace miniprojectE.Services
{
    public interface IOrderService
    {
        Task<CalculationResponseDTO<OrderDTO>> GetOrdersAsync(int page, int pageSize, OrderStatus? status, Guid? customerId, Guid? clerkId);
        Task<OrderDTO> GetOrderAsync(int orderId);
        Task<CalculationResponseDTO<OrderDTO>> GetCustomerOrdersAsync(Guid customerId);
        Task<OrderDTO> CreateOrderAsync(CreateOrderDTO dto);
        Task<OrderDTO> UpdateOrderStatusAsync(int orderId, UpdateOrderStatusDTO dto);
        Task<OrderDTO> AssignClerkAsync(int orderId, Guid clerkId);
        Task<OrderDTO> StartAssemblyAsync(int orderId);
        Task<OrderDTO> CompleteAssemblyAsync(int orderId);
        Task<OrderDTO> ShipOrderAsync(int orderId);
        Task<OrderDTO> CompleteOrderAsync(int orderId);
        Task CancelOrderAsync(int orderId);
        Task<OrderCalculationDTO> CalculateOrderPriceAsync(CreateOrderDTO dto);
        Task<List<OrderDTO>> GetAllOrdersAsync();
    }
}
