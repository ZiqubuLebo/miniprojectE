//using FurnitureStore.DTOs;
using miniprojectE.Models.Entities;
using System.ComponentModel.DataAnnotations;

namespace miniprojectE.DTO.OrderDTOs
{
    public class CustomFurnitureItemDTO
    {
        public int ItemId { get; set; }
        public int TemplateId { get; set; }
        public int OrderId { get; set; }
        public string TemplateName { get; set; }
        public FurnitureType FurnitureType { get; set; }
        public string ItemName { get; set; }
        public string CustomDescription { get; set; }
        [DisplayFormat(DataFormatString = "{0:C}", ApplyFormatInEditMode = false)]
        public decimal ItemTotalPrice { get; set; }
        public int Quantity { get; set; }
        [DisplayFormat(DataFormatString = "{0:C}", ApplyFormatInEditMode = false)]
        public decimal UnitPrice => Quantity > 0 ? ItemTotalPrice / Quantity : 0;
        public AssemblyStatus AssemblyStatus { get; set; }
        public DateTime? AssemblyStartedAt { get; set; }
        public DateTime? AssemblyCompletedAt { get; set; }
        public string AssemblyStatusDisplay
        {
            get
            {
                return AssemblyStatus switch
                {
                    AssemblyStatus.Pending => "Pending Assembly",
                    AssemblyStatus.InProgress => "Being Assembled",
                    AssemblyStatus.Completed => "Assembly Complete",
                    _ => "Unknown"
                };
            }
        }
        public List<ItemComponentDTO> Components { get; set; } = new List<ItemComponentDTO>();
    }
}
