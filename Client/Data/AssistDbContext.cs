using Microsoft.EntityFrameworkCore;

namespace Client.Data
{
    public class AssistDbContext : DbContext
    {
        public DbSet<User> Users { get; set; }

        public AssistDbContext(DbContextOptions<AssistDbContext> options) : base(options)
        {
            Database.EnsureCreated();
        }
    }
}