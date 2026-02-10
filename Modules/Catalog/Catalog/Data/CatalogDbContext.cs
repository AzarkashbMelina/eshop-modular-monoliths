namespace Catalog.Data;

public class CatalogDbContext : DbContext
{
    public CatalogDbContext(DbContextOptions<CatalogDbContext> options) 
        : base(options) { }

    public DbSet<Product> Products => Set<Product>(); //this will represent product table on the postgres

    protected override void OnModelCreating(ModelBuilder builder)
    {
        builder.HasDefaultSchema("catalog"); //for data isolation and schema separation
        builder.Entity<Product>().Property(c => c.Name).IsRequired().HasMaxLength(100); 
        base.OnModelCreating(builder);
    }
}
