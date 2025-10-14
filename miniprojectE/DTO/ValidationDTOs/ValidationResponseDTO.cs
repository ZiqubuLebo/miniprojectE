namespace miniprojectE.DTO.ValidationDTOs
{
    public class ValidationResponseDTO
    {
        public bool IsValid { get; set; }
        public List<string> ValidationErrors { get; set; } = new List<string>();
        public List<string> CompatibilityIssues { get; set; } = new List<string>();
        public List<string> StockIssues { get; set; } = new List<string>();
        public decimal EstimatedPrice { get; set; }
    }
}
