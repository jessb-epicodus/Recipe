using System.Collections.Generic;

namespace RecipeBox.Models {
  public class Recipe {
    public Recipe() {
      this.JoinCategories = new HashSet<CategoryRecipe>();
      this.JoinIngredients = new HashSet<IngredientRecipe>();
    }
    public int RecipeId { get; set; }
    public string Name { get; set; }
    public int Rating { get; set; }
    public string Instructions { get; set; }
    public int CookTime { get; set; }
    public int Yield { get; set; }
    public virtual ApplicationUser User { get; set; }
    public virtual ICollection<CategoryRecipe> JoinCategories { get; set; }
    public virtual ICollection<IngredientRecipe> JoinIngredients { get; set; }
  }
}