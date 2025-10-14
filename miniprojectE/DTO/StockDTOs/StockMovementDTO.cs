using miniprojectE.Models.Entities;

namespace miniprojectE.DTO.StockDTOs
{
    public class StockMovementDTO
    {
        public int MovementId { get; set; }
        public int ComponentId { get; set; }
        public string ComponentName { get; set; }
        public string UserName { get; set; }
        public MovementType MovementType { get; set; }
        public int QuantityChange { get; set; }
        public int QuantityBefore { get; set; }
        public int QuantityAfter { get; set; }
        public string ReferenceId { get; set; }
        public string Notes { get; set; }
        public DateTime MovementDate { get; set; }
    }
}
