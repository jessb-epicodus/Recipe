using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;
using System.IO;

namespace Recipe.Models {
  public class RecipeContextFactory : IDesignTimeDbContextFactory<RecipeContext> {

    RecipeContext IDesignTimeDbContextFactory<RecipeContext>.CreateDbContext(string[] args) {
      IConfigurationRoot configuration = new ConfigurationBuilder()
        .SetBasePath(Directory.GetCurrentDirectory())
        .AddJsonFile("appsettings.json")
        .Build();
      var builder = new DbContextOptionsBuilder<RecipeContext>();
      builder.UseMySql(configuration["ConnectionStrings:DefaultConnection"], ServerVersion.AutoDetect(configuration["ConnectionStrings:DefaultConnection"]));
      return new RecipeContext(builder.Options);
    }
  }
}