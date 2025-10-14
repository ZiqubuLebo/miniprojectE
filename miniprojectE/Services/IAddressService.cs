using miniprojectE.DTO.AddressDTOs;

namespace miniprojectE.Services
{
    public interface IAddressService
    {
        public Task<List<AddressDTO>> GetUserAddressesAsync(Guid userId);
        public Task<AddressDTO> GetAddressAsync(int addressId);
        public Task<AddressDTO> CreateAddressAsync(Guid userId, CreateAddressDTO dto);
        public Task<AddressDTO> UpdateAddressAsync(int addressId, CreateAddressDTO dto);
        public Task DeleteAddressAsync(int addressId);
    }
}
