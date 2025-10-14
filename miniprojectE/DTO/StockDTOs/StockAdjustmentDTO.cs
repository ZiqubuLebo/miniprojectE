using miniprojectE.Models.Entities;
using System.ComponentModel.DataAnnotations;

namespace miniprojectE.DTO.StockDTOs
{
    public class StockAdjustmentDTO
    {
        //public required int ComponentId { get; set; }

        public required int QuantityChange { get; set; }

        public required MovementType MovementType { get; set; }
        //public required ComponentType ComponentType { get; set; }

        public string Notes { get; set; }
    }
}
