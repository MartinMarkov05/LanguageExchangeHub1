using LanguageExchangeHub1.Data.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace LanguageExchangeHub1.Data;

public class ApplicationDbContext : IdentityDbContext<User,Role,string>
{
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
        : base(options)
    {
    }

    public ApplicationDbContext(string connectionString) : base(new DbContextOptionsBuilder<ApplicationDbContext>().UseNpgsql(connectionString).Options)
    {

    }
    
    public DbSet<Language> Languages { get; set; }
    public DbSet<Course> Courses { get; set; }
   public DbSet<Request> Requests { get; set; }
    public DbSet<CourseUser> CourseUsers { get; set; }


}

