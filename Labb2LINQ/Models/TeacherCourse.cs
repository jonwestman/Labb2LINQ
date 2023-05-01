using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Labb2LINQ.Models
{
    public class TeacherCourse
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int TeacherCourseId { get; set; }
        [ForeignKey(nameof(Teachers))]
        [DisplayName("Teacher")]
        public int FK_TeacherId { get; set; }
        public virtual Teacher? Teachers { get; set; }
        [ForeignKey(nameof(Courses))]
        [DisplayName("Course")]
        public int FK_CourseId { get; set; }
        public Course? Courses { get; set; }
    }
}
