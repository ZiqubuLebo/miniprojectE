namespace miniprojectE.Models.Entities
{
    public class Report
    {
        public int Id { get; set; }
        public required string Type { get; set; }
        public string? Description { get; set; }
        public required string Date {  get; set; }
    }
}
