using Microsoft.EntityFrameworkCore;

namespace Client.Data
{
    public class ComponentsContext : DbContext
    {
        public DbSet<CPU> CPU { get; set; }
        public DbSet<RAM> RAM { get; set; }
        public DbSet<StorageDevice> StorageDevice { get; set; }
        public DbSet<PowerSupply> PowerSupply { get; set; }
        public DbSet<Motherboard> Motherboard { get; set; }
        public DbSet<Shell> Shell { get; set; }
        public DbSet<CoolingSystem> CoolingSystem { get; set; }

        public ComponentsContext(DbContextOptions<ComponentsContext> options) : base(options)
        {
            Database.EnsureCreated();
        }
    }
}