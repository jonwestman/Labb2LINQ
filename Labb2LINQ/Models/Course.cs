using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Labb2LINQ.Models
{
    public class Course
    {
        [Key]
        [DatabaseGenerated(databaseGeneratedOption: DatabaseGeneratedOption.Identity)]
        public int CourseId { get; set; }
        [Required]
        [StringLength(30)]
        [DisplayName("Course name")]
        public string Title { get; set; }
        public ICollection<TeacherCourse>? TeacherCourses { get;}
        public ICollection<StudentCourse>? StudentCourses { get; }
    }
}
