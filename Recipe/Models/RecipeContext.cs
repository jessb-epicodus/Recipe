using Microsoft.EntityFrameworkCore;

namespace Recipe.Models {
  public class RecipeContext : DbContext {
    public DbSet<Category> Categories { get; set; }
    public DbSet<Recipe> Recipes { get; set; }
    public DbSet<CattegoryRecipe> CategoryRecipes { get; set; }
    public RecipeContext(DbContextOptions options) : base(options) { }
    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder) {
      optionsBuilder.UseLazyLoadingProxies();
    }
  }
}