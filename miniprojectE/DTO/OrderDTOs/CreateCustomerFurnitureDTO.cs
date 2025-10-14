//using FurnitureStore.DTOs;
using System.ComponentModel.DataAnnotations;

namespace miniprojectE.DTO.OrderDTOs
{
    public class CreateCustomerFurnitureDTO
    {
        [Required]
        public int TemplateId { get; set; }

        [Required]
        public string ItemName { get; set; }

        public string CustomDescription { get; set; }

        [Required]
        [Range(1, int.MaxValue)]
        public int Quantity { get; set; }

        [Required]
        public List<CreateItemComponentDTO> Components { get; set; } = new List<CreateItemComponentDTO>();
    }
}
