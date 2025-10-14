using miniprojectE.DTO.OrderDTOs;
using miniprojectE.DTO.ResponseDTOs;
using miniprojectE.Models.Entities;

namespace miniprojectE.Services
{
    public interface IPurchaseOrderService
    {
        Task<CalculationResponseDTO<PurchaseOrderDTO>> GetPurchaseOrdersAsync(int page, int pageSize, PurchaseOrderStatus? status);
        Task<PurchaseOrderDTO> GetPurchaseOrderAsync(int purchaseOrderId);
        Task<PurchaseOrderDTO> CreatePurchaseOrderAsync(CreatePurchaseOrderDTO dto);
        Task<PurchaseOrderDTO> ReceivePurchaseOrderAsync(int purchaseOrderId, ReceivedPurchaseOrderDTO dto);
        Task<PurchaseOrderDTO> UpdatePurchaseOrderStatusAsync(int purchaseOrderId, PurchaseOrderStatus status);
    }
}
