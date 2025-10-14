using miniprojectE.DTO.OrderDTOs;
using System.ComponentModel.DataAnnotations;

namespace miniprojectE.DTO.ValidationDTO
{
    public class ComponentValidationDTO
    {
        [Required]
        public List<CreateItemComponentDTO> Components { get; set; } = new List<CreateItemComponentDTO>();
    }
}
