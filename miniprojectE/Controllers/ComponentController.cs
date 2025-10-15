using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using miniprojectE.DTO.ComponentDTOs;
using miniprojectE.DTO.ResponseDTOs;
using miniprojectE.DTO.StockDTOs;
using miniprojectE.DTO.ValidationDTO;
using miniprojectE.DTO.ValidationDTOs;
using miniprojectE.Models.Entities;
using miniprojectE.Services;

namespace miniprojectE.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ComponentController : ControllerBase
    {
         private readonly IComponentService _componentService;

        public ComponentController(IComponentService componentService)
        {
            _componentService = componentService;
        }

        [HttpGet("GetComponent/{id}")]
        public async Task<ActionResult<ApiResponseDTO<ComponentDTO>>> GetComponent(int id)
        {
            try
            {
                var component = await _componentService.GetComponentAsync(id);
                return Ok(new ApiResponseDTO<ComponentDTO> { Success = true, Data = component });
            }
            catch (Exception ex)
            {
                return NotFound(new ApiResponseDTO<ComponentDTO> { Success = false, Message = ex.Message });
            }
        }

        [HttpPost("CreateComponent")]
        //[Authorize(Roles = "Admin,Manager")]
        public async Task<ActionResult<ApiResponseDTO<ComponentDTO>>> CreateComponent(Guid userId, [FromBody] CreateComponentDTO dto)
        {
            try
            {
                var result = await _componentService.CreateComponentAsync(dto, userId);
                return CreatedAtAction(nameof(GetComponent), new { id = result.ComponentId },
                    new ApiResponseDTO<ComponentDTO> { Success = true, Data = result });
            }
            catch (Exception ex)
            {
                return BadRequest(new ApiResponseDTO<ComponentDTO> { Success = false, Message = ex.Message });
            }
        }

        [HttpPut("UpdateComponent/{id}")]
        //[Authorize(Roles = "Admin,Manager")]
        public async Task<ActionResult<ApiResponseDTO<ComponentDTO>>> UpdateComponent(int id, [FromBody] UpdateComponentDTO dto)
        {
            try
            {
                var result = await _componentService.UpdateComponentAsync(id, dto);
                return Ok(new ApiResponseDTO<ComponentDTO> { Success = true, Data = result });
            }
            catch (Exception ex)
            {
                return BadRequest(new ApiResponseDTO<ComponentDTO> { Success = false, Message = ex.Message });
            }
        }

        [HttpPost("{id}/stock")]
        //[Authorize(Roles = "Admin,Manager,Clerk")]
        public async Task<ActionResult<ApiResponseDTO<ComponentDTO>>> AdjustStock(int id, [FromBody] StockAdjustmentDTO dto)
        {
            try
            {
                var result = await _componentService.AdjustStockAsync(id, dto);
                return Ok(new ApiResponseDTO<ComponentDTO> { Success = true, Data = result });
            }
            catch (Exception ex)
            {
                return BadRequest(new ApiResponseDTO<ComponentDTO> { Success = false, Message = ex.Message });
            }
        }

        [HttpGet("{id}/GetCompatibility")]
        public async Task<ActionResult<ApiResponseDTO<List<ComponentCompatibilityDTO>>>> GetCompatibility(int id)
        {
            try
            {
                var compatibility = await _componentService.GetComponentCompatibilityAsync(id);
                return Ok(new ApiResponseDTO<List<ComponentCompatibilityDTO>> { Success = true, Data = compatibility });
            }
            catch (Exception ex)
            {
                return NotFound(new ApiResponseDTO<List<ComponentCompatibilityDTO>> { Success = false, Message = ex.Message });
            }
        }

        [HttpPost("CreateCompatibility")]
        //[Authorize(Roles = "Admin,Manager")]
        public async Task<ActionResult<ApiResponseDTO<ComponentCompatibilityDTO>>> CreateCompatibility([FromBody] CreateCompatibilityDTO dto)
        {
            try
            {
                var result = await _componentService.CreateCompatibilityAsync(dto);
                return Ok(new ApiResponseDTO<ComponentCompatibilityDTO> { Success = true, Data = result });
            }
            catch (Exception ex)
            {
                return BadRequest(new ApiResponseDTO<ComponentCompatibilityDTO> { Success = false, Message = ex.Message });
            }
        }

        [HttpPost("validate")]
        public async Task<ActionResult<ValidationResponseDTO>> ValidateComponents([FromBody] ComponentValidationDTO request)
        {
            try
            {
                var result = await _componentService.ValidateComponentsAsync(request.Components);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(new ValidationResponseDTO { IsValid = false, ValidationErrors = { ex.Message } });
            }
        }

        [HttpGet("GetAllComponents")]
        public async Task<ActionResult<ApiResponseDTO<List<ComponentDTO>>>> GetAllComponents()
        {
            try
            {
                var component = await _componentService.GetAllComponentsAsync();
                return Ok(new ApiResponseDTO<List<ComponentDTO>> { Success = true, Data = component });
            }
            catch (Exception ex)
            {
                return NotFound(new ApiResponseDTO<ComponentDTO> { Success = false, Message = ex.Message });
            }
        }
    }
}
