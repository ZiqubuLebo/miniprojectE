using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using miniprojectE.DTO.AddressDTOs;
using miniprojectE.DTO.ResponseDTOs;
using miniprojectE.Services;

namespace miniprojectE.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AddressController : ControllerBase
    {
        private readonly IAddressService _addressService;

        public AddressController(IAddressService addressService)
        {
            _addressService = addressService;
        }

        [HttpGet("GetUserAddresses/{userId}")]
        //public async Task<ActionResult<ApiResponseDTO<List<AddressDTO>>>> GetUserAddresses(Guid userId)
        public async Task<ActionResult<ApiResponseDTO<List<AddressDTO>>>> GetUserAddresses(Guid userId)
        {
            try
            {
                var addresses = await _addressService.GetUserAddressesAsync(userId);
                return Ok(new ApiResponseDTO<List<AddressDTO>> { Success = true, Data = addresses });
            }
            catch (Exception ex)
            {
                return NotFound(new ApiResponseDTO<List<AddressDTO>> { Success = false, Message = ex.Message });
            }
        }

        [HttpPost("CreateAddress/{userId}")]
        public async Task<ActionResult<ApiResponseDTO<AddressDTO>>> CreateAddress(Guid userId, [FromBody] CreateAddressDTO dto)
        {
            try
            {
                var result = await _addressService.CreateAddressAsync(userId, dto);
                return Ok(new ApiResponseDTO<AddressDTO> { Success = true, Data = result });
            }
            catch (Exception ex)
            {
                return BadRequest(new ApiResponseDTO<AddressDTO> { Success = false, Message = ex.Message });
            }
        }

        [HttpPut("UpdateAddress/{id}")]
        public async Task<ActionResult<ApiResponseDTO<AddressDTO>>> UpdateAddress(int id, [FromBody] CreateAddressDTO dto)
        {
            try
            {
                var result = await _addressService.UpdateAddressAsync(id, dto);
                return Ok(new ApiResponseDTO<AddressDTO> { Success = true, Data = result });
            }
            catch (Exception ex)
            {
                return BadRequest(new ApiResponseDTO<AddressDTO> { Success = false, Message = ex.Message });
            }
        }

        [HttpDelete("DeleteAddress/{id}")]
        public async Task<ActionResult<ApiResponseDTO<bool>>> DeleteAddress(int id)
        {
            try
            {
                await _addressService.DeleteAddressAsync(id);
                return Ok(new ApiResponseDTO<bool> { Success = true, Data = true });
            }
            catch (Exception ex)
            {
                return BadRequest(new ApiResponseDTO<bool> { Success = false, Message = ex.Message });
            }
        }
    }
}
