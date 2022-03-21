using System.Collections.Generic;

namespace RecipeBox.Models {
  public class CategoryRecipe {
    public int CategoryRecipeId { get; set; }
    public int CategpryId { get; set; }
    public int RecipeId { get; set; }
    public virtual Category Category { get; set; }
    public virtual Recipe Recipe { get; set; }
  }
}