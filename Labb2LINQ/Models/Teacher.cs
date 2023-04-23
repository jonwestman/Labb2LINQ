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
        public string FirstMidName { get; set; }
        [StringLength(50)]
        public string LastName { get; set; }
    }
}
