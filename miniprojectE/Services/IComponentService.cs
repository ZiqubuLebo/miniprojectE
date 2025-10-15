using miniprojectE.DTO.ComponentDTOs;
using miniprojectE.DTO.OrderDTOs;
using miniprojectE.DTO.ResponseDTOs;
using miniprojectE.DTO.StockDTOs;
using miniprojectE.DTO.ValidationDTOs;
using miniprojectE.Models.Entities;

namespace miniprojectE.Services
{
    public interface IComponentService
    {
        Task<ComponentDTO> GetComponentAsync(int componentId);
        Task<ComponentDTO> CreateComponentAsync(CreateComponentDTO dto, Guid userId);
        Task<ComponentDTO> UpdateComponentAsync(int componentId, UpdateComponentDTO dto);
        Task<ComponentDTO> AdjustStockAsync(int componentId, StockAdjustmentDTO dto);
        Task<List<ComponentCompatibilityDTO>> GetComponentCompatibilityAsync(int componentId);
        Task<ComponentCompatibilityDTO> CreateCompatibilityAsync(CreateCompatibilityDTO dto);
        Task<List<ComponentDTO>> GetAllComponentsAsync();
    }
}
