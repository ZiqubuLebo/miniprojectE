using miniprojectE.Data;
using miniprojectE.DTO.OrderDTOs;
using miniprojectE.DTO.ResponseDTOs;
using miniprojectE.DTO.StockDTOs;
using miniprojectE.Models.Entities;

namespace miniprojectE.Services
{
    public class InventoryService: IInventoryService
    {
        private readonly AppDB _context;
        public InventoryService(AppDB context) { _context = context; }

        public Task<CalculationResponseDTO<StockMovementDTO>> GetStockMovementsAsync(int page, int pageSize, int? componentId, MovementType? movementType, DateTime? startDate, DateTime? endDate) => throw new NotImplementedException();
        public Task<List<StockMovementDTO>> GetComponentStockHistoryAsync(int componentId) => throw new NotImplementedException();
        public Task<StockMovementDTO> CreateStockMovementAsync(int componentId, int userId, MovementType type, int quantityChange, string referenceId, string notes) => throw new NotImplementedException();
        public Task<bool> ReserveStockAsync(List<CreateItemComponentDTO> components, string referenceId) => throw new NotImplementedException();
        public Task<bool> ReleaseStockReservationAsync(string referenceId) => throw new NotImplementedException();
    }
}
