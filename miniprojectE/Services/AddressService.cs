using Microsoft.EntityFrameworkCore;
using miniprojectE.Data;
using miniprojectE.DTO.AddressDTOs;
using miniprojectE.Models.Entities;
using System.ComponentModel;

namespace miniprojectE.Services
{
    public class AddressService: IAddressService
    {
        private readonly AppDB _context;

        public AddressService(AppDB context)
        {
            _context = context;
        }

        // Implement all IAddressService methods
        public async Task<List<AddressDTO>> GetUserAddressesAsync(Guid userId)
        {
            var query = _context.Address.Where(a => a.UserID == userId);

            var address = await query.Select(a =>
                new AddressDTO
            {
                    AddressID = a.AddressID,
                    City = a.City,
                    Code = a.Code,
                    Country = a.Country,
                    Province = a.Province,
                    Street = a.Street,
            }).ToListAsync();

            return address;
        }
        public async Task<AddressDTO> GetAddressAsync(int addressId)
        {
            var query =await _context.Address.FirstOrDefaultAsync(a => a.AddressID == addressId);

            if (query == null)
                throw new NotFoundException("Address not found");

            return new AddressDTO
            {
                AddressID = query.AddressID,
                City = query.City,
                Code = query.Code,
                Country = query.Country,
                Province = query.Province,
                Street = query.Street,
            };
        }
        public async Task<AddressDTO> CreateAddressAsync(Guid userId, CreateAddressDTO dto)
        {
            var address = new Address
            {
                UserID = userId,
                City = dto.City,
                Country = dto.Country,
                Code = dto.Code,
                Province = dto.Province,
                Street = dto.Street,
            };

            _context.Address.Add(address);
            await _context.SaveChangesAsync();

            return await GetAddressAsync(address.AddressID);
        }
        public async Task<AddressDTO> UpdateAddressAsync(int addressId, CreateAddressDTO dto)
        {
            var address = await _context.Address.FindAsync(addressId);
            if (address == null)
                throw new NotFoundException("Component not found");

            address.City = dto.City;
            address.Country = dto.Country;
            address.Code = dto.Code;
            address.Province = dto.Province;
            address.Street = dto.Street;

            await _context.SaveChangesAsync();

            return await GetAddressAsync(addressId);
        }
        public Task DeleteAddressAsync(int addressId) => throw new NotImplementedException();
    }
}
