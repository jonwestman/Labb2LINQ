using Labb2LINQ.Models;
using Microsoft.EntityFrameworkCore;
using NuGet.DependencyResolver;

namespace Labb2LINQ.Data
{
    public class SchoolDbContext : DbContext
    {
        private readonly SchoolDbContext _context;
        public SchoolDbContext(SchoolDbContext context)
        {

            _context = context;

        }
        public DbSet<Teacher> Teachers { get; set; }
        public DbSet<Student> Students { get; set; }
        public DbSet<Class> Classes { get; set; }
        public DbSet<Course> Courses { get; set; }
    }
}
