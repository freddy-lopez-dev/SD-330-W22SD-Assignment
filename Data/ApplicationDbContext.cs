using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace SD_330_W22SD_Assignment.Data
{
    public class ApplicationDbContext : IdentityDbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        public DbSet<SD_330_W22SD_Assignment.Models.Question> Question { get; set; }
        public DbSet<SD_330_W22SD_Assignment.Models.Answer> Answer { get; set; }
    }
}