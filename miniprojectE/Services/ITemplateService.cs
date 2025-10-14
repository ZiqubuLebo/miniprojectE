using miniprojectE.DTO.ComponentDTOs;
using miniprojectE.Models.Entities;

namespace miniprojectE.Services
{
    public interface ITemplateService
    {
        Task<List<FurnitureDTO>> GetTemplatesAsync(FurnitureType? furnitureType);
        Task<FurnitureDTO> GetTemplateAsync(int templateId);
        Task<FurnitureDTO> CreateTemplateAsync(CreateTemplateDTO dto);
        Task<FurnitureDTO> UpdateTemplateAsync(int templateId, CreateTemplateDTO dto);
        Task<TemplateComponentDTO> AddTemplateComponentAsync(int templateId, CreateTemplateComponentDTO dto);
        Task RemoveTemplateComponentAsync(int templateId, int componentId);
        Task<List<FurnitureDTO>> GetAllTemplatesAsync();
    }
}
