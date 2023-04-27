using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace Labb2LINQ.Models
{
    public class StudentCourse
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int StudentCourseId { get; set; }
        [ForeignKey(nameof(Students))]
        public int FK_StudentId { get; set; }
        public virtual Student? Students { get; set; }
        [ForeignKey(nameof(Courses))]
        public int FK_CourseId { get; set; }
        public virtual Course? Courses { get; set; }
    }
}
