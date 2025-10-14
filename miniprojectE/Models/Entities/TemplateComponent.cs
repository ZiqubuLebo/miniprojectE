using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace miniprojectE.Models.Entities
{
    public enum ComponentRole
    {
        Primary,
        Secondary,
        Optional
    }
    /* used in template*/
    public class TemplateComponent
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int TemplateID { get; set; }
        public required int FurnitureID { get; set; }
        public required int ComponentID { get; set; }
        public required bool isRequired { get; set; }
        public required int minLevel { get; set; }
        public required int maxLevel { get; set; }
        [Required]
        public ComponentRole ComponentRole { get; set; }

        [ForeignKey("FurnitureID")]
        public virtual Furniture Template { get; set; }

        [ForeignKey("ComponentID")]
        public virtual Component Component { get; set; }
    }
}
