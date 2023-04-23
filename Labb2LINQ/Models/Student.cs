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
        public string FirstMidName { get; set; }
        [StringLength(50)]
        public string LastName { get; set; }
    }
}
