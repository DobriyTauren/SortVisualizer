using Microsoft.EntityFrameworkCore;

public class AlgorithmsContext : DbContext
{
    public DbSet<AlgorithmModel> Algorithms { get; set; }

    public AlgorithmsContext()
    {
        Database.EnsureCreated();
    }

    public AlgorithmsContext(DbContextOptions<AlgorithmsContext> options) : base(options)
    {
        Database.EnsureCreated();
    }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        if (!optionsBuilder.IsConfigured ) 
        {
            var config = new ConfigurationBuilder()
                .AddJsonFile("appsettings.json")
                .SetBasePath(Directory.GetCurrentDirectory())
                .Build();

            optionsBuilder.UseSqlServer(config.GetConnectionString("DefaultConnection"));
        }
    }
}