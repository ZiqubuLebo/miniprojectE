using miniprojectE.DTO.OrderDTOs;
using miniprojectE.DTO.ResponseDTOs;
using miniprojectE.DTO.StockDTOs;
using miniprojectE.Models.Entities;

namespace miniprojectE.Services
{
    public interface IInventoryService
    {
        Task<CalculationResponseDTO<StockMovementDTO>> GetStockMovementsAsync(int page, int pageSize, int? componentId, MovementType? movementType, DateTime? startDate, DateTime? endDate);
        Task<List<StockMovementDTO>> GetComponentStockHistoryAsync(int componentId);
        Task<StockMovementDTO> CreateStockMovementAsync(int componentId, int userId, MovementType type, int quantityChange, string referenceId, string notes);
        Task<bool> ReserveStockAsync(List<CreateItemComponentDTO> components, string referenceId);
        Task<bool> ReleaseStockReservationAsync(string referenceId);
    }
}
