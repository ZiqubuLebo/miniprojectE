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
        Task<CalculationResponseDTO<ComponentDTO>> GetComponentsAsync(int page, int pageSize, ComponentType? type, bool? lowStockOnly);
        Task<ComponentDTO> GetComponentAsync(int componentId);
        Task<ComponentDTO> CreateComponentAsync(CreateComponentDTO dto, Guid userId);
        Task<ComponentDTO> UpdateComponentAsync(int componentId, UpdateComponentDTO dto);
        Task<ComponentDTO> AdjustStockAsync(int componentId, StockAdjustmentDTO dto);
        Task<List<ComponentCompatibilityDTO>> GetComponentCompatibilityAsync(int componentId);
        Task<ComponentCompatibilityDTO> CreateCompatibilityAsync(CreateCompatibilityDTO dto);
        Task<ValidationResponseDTO> ValidateComponentsAsync(List<CreateItemComponentDTO> components);
        Task<List<ComponentDTO>> SearchComponentsAsync(ComponentSearchDTO searchDto);
        Task<List<ComponentDTO>> GetAllComponentsAsync();
    }
}
