using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace miniprojectE.Models.Entities
{
    public class ComponentCompatibility
    {
        [Key]
        public int CompatibiltyID { get; set; }
        public  required int ComponentID1 { get; set; }
        public required int ComponentID2 { get; set; }
        public required bool IsCompatible { get; set; }
        public string? notes { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

        
        [ForeignKey("ComponentID1")]
        public virtual Component ComponentA { get; set; }

        [ForeignKey("ComponentID2")]
        public virtual Component ComponentB { get; set; }
    }
}
