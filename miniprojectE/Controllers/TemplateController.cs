using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using miniprojectE.DTO.ComponentDTOs;
using miniprojectE.DTO.ResponseDTOs;
using miniprojectE.Models.Entities;
using miniprojectE.Services;

namespace miniprojectE.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TemplateController : ControllerBase
    {
        private readonly ITemplateService _templateService;

        public TemplateController(ITemplateService templateService)
        {
            _templateService = templateService;
        }

        [HttpGet("GetTemplates")]
        public async Task<ActionResult<ApiResponseDTO<List<FurnitureDTO>>>> GetTemplates(
            [FromQuery] FurnitureType? furnitureType = null)
        {
            try
            {
                var templates = await _templateService.GetTemplatesAsync(furnitureType);
                return Ok(new ApiResponseDTO<List<FurnitureDTO>> { Success = true, Data = templates });
            }
            catch (Exception ex)
            {
                return BadRequest(new ApiResponseDTO<List<FurnitureDTO>> { Success = false, Message = ex.Message });
            }
        }

        [HttpGet("GetTemplate/{id}")]
        public async Task<ActionResult<ApiResponseDTO<FurnitureDTO>>> GetTemplate(int id)
        {
            try
            {
                var template = await _templateService.GetTemplateAsync(id);
                return Ok(new ApiResponseDTO<FurnitureDTO> { Success = true, Data = template });
            }
            catch (Exception ex)
            {
                return NotFound(new ApiResponseDTO<FurnitureDTO> { Success = false, Message = ex.Message });
            }
        }

        [HttpPost("CreateTemplate")]
        public async Task<ActionResult<ApiResponseDTO<FurnitureDTO>>> CreateTemplate([FromBody] CreateTemplateDTO dto)
        {
            try
            {
                var result = await _templateService.CreateTemplateAsync(dto);
                return CreatedAtAction(nameof(GetTemplate), new { id = result.TemplateID },
                    new ApiResponseDTO<FurnitureDTO> { Success = true, Data = result });
            }
            catch (Exception ex)
            {
                return BadRequest(new ApiResponseDTO<FurnitureDTO> { Success = false, Message = ex.Message });
            }
        }

        [HttpPost("{id}/AddTemplateComponent")]
        public async Task<ActionResult<ApiResponseDTO<TemplateComponentDTO>>> AddTemplateComponent(int id, [FromBody] CreateTemplateComponentDTO dto)
        {
            try
            {
                var result = await _templateService.AddTemplateComponentAsync(id, dto);
                return Ok(new ApiResponseDTO<TemplateComponentDTO> { Success = true, Data = result });
            }
            catch (Exception ex)
            {
                return BadRequest(new ApiResponseDTO<TemplateComponentDTO> { Success = false, Message = ex.Message });
            }
        }

        [HttpDelete("{templateId}/components/{componentId}")]
        public async Task<ActionResult<ApiResponseDTO<bool>>> RemoveTemplateComponent(int templateId, int componentId)
        {
            try
            {
                await _templateService.RemoveTemplateComponentAsync(templateId, componentId);
                return Ok(new ApiResponseDTO<bool> { Success = true, Data = true });
            }
            catch (Exception ex)
            {
                return BadRequest(new ApiResponseDTO<bool> { Success = false, Message = ex.Message });
            }
        }

        [HttpGet("GetAllTemplates")]
        public async Task<ActionResult<ApiResponseDTO<List<FurnitureDTO>>>> GetAllTemplates()
        {
            try
            {
                var templates = await _templateService.GetAllTemplatesAsync();
                return Ok(new ApiResponseDTO<List<FurnitureDTO>> { Success = true, Data = templates });
            }
            catch (Exception ex)
            {
                return BadRequest(new ApiResponseDTO<List<FurnitureDTO>> { Success = false, Message = ex.Message });
            }
        }
    }
}
