namespace miniprojectE.DTO.ComponentDTOs
{
    public class TemplatePricingDTO
    {
        public int TemplateId { get; set; }
        public string TemplateName { get; set; }
        public decimal BasePrice { get; set; }
        public List<ComponentPricingDTO> ComponentPricing { get; set; } = new List<ComponentPricingDTO>();
        public decimal TotalPrice { get; set; }
    }
}
