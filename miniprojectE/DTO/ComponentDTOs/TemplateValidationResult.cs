namespace miniprojectE.DTO.ComponentDTOs
{
    public class TemplateValidationResult
    {
        public int TemplateId { get; set; }
        public string TemplateName { get; set; }
        public bool IsValid { get; set; }
        public List<string> Issues { get; set; } = new List<string>();
        public List<TemplateComponentDTO> RequiredComponents { get; set; } = new List<TemplateComponentDTO>();
        public List<TemplateComponentDTO> OptionalComponents { get; set; } = new List<TemplateComponentDTO>();
        public decimal EstimatedMinPrice { get; set; }
        public decimal EstimatedMaxPrice { get; set; }
    }
}
