using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace miniprojectE.Models.Entities
{
    public class ComponentPopularity
    {
        [Key]
        public int PopularityID { get; set; }

        public required int ComponentID { get; set; }

        public required int PeriodYear { get; set; }

        [Range(1, 12)]
        public required int PeriodMonth { get; set; }

        public int? TimesOrdered { get; set; } 
        public int? TotalQuantitySold { get; set; }

        [Column(TypeName = "decimal(12,2)")]
        public decimal? TotalRevenue { get; set; }

        public DateTime CalculatedAt { get; set; } = DateTime.UtcNow;

        [ForeignKey("ComponentID")]
        public virtual Component Component { get; set; }
    }
}
