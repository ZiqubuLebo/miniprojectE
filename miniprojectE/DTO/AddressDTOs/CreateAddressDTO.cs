using System.ComponentModel.DataAnnotations;

namespace miniprojectE.DTO.AddressDTOs
{
    public class CreateAddressDTO
    {
        public required string Street { get; set; }

        public required string City { get; set; }

        public required string Province { get; set; }

        public required string Code { get; set; }

        public required string Country { get; set; }

        //public bool IsDefault { get; set; }
    }
}
