using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Labb2LINQ.Models
{
    public class Class
    {
        [Key]
        [DatabaseGenerated(databaseGeneratedOption: DatabaseGeneratedOption.Identity)]
        public int ClassId { get; set; }
        [Required]
        [StringLength(30)]
        [DisplayName("Class")]
        public string ClassName { get; set; }
        public virtual ICollection<Student>? Students { get; set; }
    }
}
