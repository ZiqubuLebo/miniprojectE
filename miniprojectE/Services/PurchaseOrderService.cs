using miniprojectE.Data;
using miniprojectE.DTO.OrderDTOs;
using miniprojectE.DTO.ResponseDTOs;
using miniprojectE.Models.Entities;

namespace miniprojectE.Services
{
    public class PurchaseOrderService: IPurchaseOrderService
    {
        private readonly AppDB _context;
        public PurchaseOrderService(AppDB context) { _context = context; }

        public Task<CalculationResponseDTO<PurchaseOrderDTO>> GetPurchaseOrdersAsync(int page, int pageSize, PurchaseOrderStatus? status) => throw new NotImplementedException();
        public Task<PurchaseOrderDTO> GetPurchaseOrderAsync(int purchaseOrderId) => throw new NotImplementedException();
        public Task<PurchaseOrderDTO> CreatePurchaseOrderAsync(CreatePurchaseOrderDTO dto) => throw new NotImplementedException();
        public Task<PurchaseOrderDTO> ReceivePurchaseOrderAsync(int purchaseOrderId, ReceivedPurchaseOrderDTO dto) => throw new NotImplementedException();
        public Task<PurchaseOrderDTO> UpdatePurchaseOrderStatusAsync(int purchaseOrderId, PurchaseOrderStatus status) => throw new NotImplementedException();
    }
}
