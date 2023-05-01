using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Labb2LINQ.Models
{
    public class Teacher
    {
        [Key]
        [DatabaseGenerated(databaseGeneratedOption: DatabaseGeneratedOption.Identity)]
        public int TeacherId { get; set; }
        [StringLength(50)]
        [DisplayName("First name")]
        public string FirstMidName { get; set; }
        [StringLength(50)]
        [DisplayName("Last name")]
        public string LastName { get; set; }
        public ICollection<TeacherCourse>? TeacherCourses { get; set; }
    }
}
