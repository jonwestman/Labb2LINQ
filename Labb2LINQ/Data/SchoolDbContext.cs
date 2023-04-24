using Labb2LINQ.Models;
using Microsoft.EntityFrameworkCore;
using NuGet.DependencyResolver;

namespace Labb2LINQ.Data
{
    public class SchoolDbContext : DbContext
    {
        public SchoolDbContext(DbContextOptions<SchoolDbContext> options) :base(options)
        {


        }
        public DbSet<Teacher> Teachers { get; set; }
        public DbSet<Student> Students { get; set; }
        public DbSet<Class> Classes { get; set; }
        public DbSet<Course> Courses { get; set; }
    }
}
