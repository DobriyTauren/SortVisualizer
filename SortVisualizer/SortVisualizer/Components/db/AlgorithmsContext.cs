using Microsoft.EntityFrameworkCore;

public class AlgorithmsContext : DbContext
{
    public DbSet<SortAlgorithmModel> Algorithms { get; set; }

    public AlgorithmsContext()
    {
        Database.EnsureCreated();
    }

    public AlgorithmsContext(DbContextOptions<AlgorithmsContext> options) : base(options)
    {
        Database.EnsureCreated();
    }

    //protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    //{
    //    optionsBuilder.UseSqlite("Data Source=algorithms.db");
    //}
}