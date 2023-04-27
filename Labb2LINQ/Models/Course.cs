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
        [DisplayName("Course name")]
        public string Title { get; set; }
        [ForeignKey(nameof(Teachers))]
        [DisplayName("Teachers")]
        public int FK_TeacherId { get; set; }
        public virtual Teacher? Teachers { get; set; }
    }
}
