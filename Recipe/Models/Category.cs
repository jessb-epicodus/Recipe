using System.Collections.Generic;

namespace Recipe.Models {
  public class Category {
    public Category() {
      this.JoinEntities = new HashSet<CategoryRecipe>();
    }
    public int CategoryId { get; set; }
    public string Name { get; set; }
    public string Description { get; set; }
    public virtual ApplicationUser User { get; set; }
    public virtual ICollection<EngineerMachine> JoinEntities { get; set; }
  }
}