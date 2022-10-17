using Microsoft.EntityFrameworkCore;

namespace Factory.Models
{
  public class FactoryContext : DbContext
  {
    public DbSet<Recipe> Recipes { get; set; }
    public DbSet<Category> Categories { get; set; }

    public DbSet<CategoryRecipe> CategoryRecipe { get; set; }

    public FactoryContext(DbContextOptions options) : base(options) { }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
      optionsBuilder.UseLazyLoadingProxies();
    }
  }
}