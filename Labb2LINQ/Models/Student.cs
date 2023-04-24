using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Labb2LINQ.Models
{
    public class Student
    {
        [Key]
        [DatabaseGenerated(databaseGeneratedOption: DatabaseGeneratedOption.Identity)]
        public int StudentId { get; set; }
        [StringLength(50)]
        [DisplayName("First name")]
        public string FirstMidName { get; set; }
        [StringLength(50)]
        [DisplayName("Last name")]
        public string LastName { get; set; }
        [ForeignKey(nameof(Classes))]
        [DisplayName("Class")]
        public int FK_ClassId { get; set; }
        [DisplayName("Class")]
        public virtual Class Classes { get; set; }
        public virtual ICollection<Course>? Courses { get; set; }
    }
}
