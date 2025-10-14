namespace miniprojectE.DTO.ReportingDTOs
{
    public class PopularityReportDTO
    {
        public int Year { get; set; }
        public int Month { get; set; }
        public List<ComponentPopularityDTO> ComponentPopularity { get; set; } = new List<ComponentPopularityDTO>();
        public decimal TotalRevenue { get; set; }
        public int TotalOrdersCount { get; set; }
        public DateTime GeneratedAt { get; set; }
    }
}
