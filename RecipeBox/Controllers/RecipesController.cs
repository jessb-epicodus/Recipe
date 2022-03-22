using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc;
using RecipeBox.Models;
using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using System.Threading.Tasks;
using System.Security.Claims;

namespace RecipeBox.Controllers
{
  [Authorize]
  public class RecipesController : Controller
  {
    private readonly RecipeBoxContext _db;
    private readonly UserManager<ApplicationUser> _userManager;
    public RecipesController(UserManager<ApplicationUser> userManager, RecipeBoxContext db)
    {
      _userManager = userManager;
      _db = db;
    }

    public async Task<ActionResult> Index()
    {
      var userId = this.User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
      var currentUser = await _userManager.FindByIdAsync(userId);
      var userRecipes = _db.Recipes.Where(entry => entry.User.Id == currentUser.Id).OrderByDescending(userRecipes => userRecipes.Rating).ToList();
      return View(userRecipes);
    }

    public ActionResult Create(int id)
    {
      ViewBag.CategoryId = new SelectList(_db.Categories, "CategoryId", "Name");
      var thisRecipe = _db.Recipes
        .Include(recipe => recipe.JoinIngredients)
        .ThenInclude(join => join.Ingredient)
        .FirstOrDefault(recipe => recipe.RecipeId == id);
      return View();
    }

    [HttpPost]
    public async Task<ActionResult> Create(Recipe recipe, string Ingredient1, string Ingredient2, string Ingredient3, string IngredientRecipe1, string IngredientRecipe2, string IngredientRecipe3, int RecipeId, int IngredientId, int RecipeIngredientId, int CategoryId)
    {
      var userId = this.User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
      var currentUser = await _userManager.FindByIdAsync(userId);
      recipe.User = currentUser;

      Ingredient ing1 = new Ingredient() { Name = Ingredient1 };
      _db.Ingredients.Add(ing1);
      Ingredient ing2 = new Ingredient() { Name = Ingredient2 };
      _db.Ingredients.Add(ing2);
      Ingredient ing3 = new Ingredient() { Name = Ingredient3 };
      _db.Ingredients.Add(ing3);
      _db.Recipes.Add(recipe);
      _db.SaveChanges();
          RecipeIngredient recIng1 = new RecipeIngredient() { IngredientId = ing1.IngredientId, RecipeId = recipe.RecipeId };       
          RecipeIngredient recIng2 = new RecipeIngredient() { IngredientId = ing2.IngredientId, RecipeId = recipe.RecipeId }; 
          RecipeIngredient recIng3 = new RecipeIngredient() { IngredientId = ing3.IngredientId, RecipeId = recipe.RecipeId }; 
          _db.RecipeIngredient.Add(recIng1);
          _db.RecipeIngredient.Add(recIng2);
          _db.RecipeIngredient.Add(recIng3);

    //   public int RecipeIngredientId { get; set; }
    // public int IngredientId { get; set; }
    // public int RecipeId { get; set; }
    // public string Amount { get; set; }


      


      // _db.SaveChanges();
      // _db.Ingredients.Add(ingredient);
      _db.SaveChanges();
      // if (RecipeId != 0)
      // {
      //   if (IngredientId != 0)
      //   {
      //     _db.RecipeIngredient.Add(new RecipeIngredient() { RecipeId = RecipeId, IngredientId = Ingredient1.IngredientId });
      //     _db.SaveChanges();
      //   }
      // }
      
      // _db.SaveChanges();

      if (CategoryId != 0)
      {
        _db.CategoryRecipe.Add(new CategoryRecipe() { CategoryId = CategoryId, RecipeId = recipe.RecipeId });
        _db.SaveChanges();
      }
      return RedirectToAction("Index");
    }

    public ActionResult Details(int id)
    {
      var thisRecipe = _db.Recipes
          .Include(recipe => recipe.JoinCategories)
          .ThenInclude(join => join.Category)
          .FirstOrDefault(recipe => recipe.RecipeId == id);
      return View(thisRecipe);
    }

    public ActionResult Edit(int id)
    {
      var thisRecipe = _db.Recipes.FirstOrDefault(recipe => recipe.RecipeId == id);
      ViewBag.CategoryId = new SelectList(_db.Categories, "CategoryId", "Name");
      return View(thisRecipe);
    }

    [HttpPost]
    public ActionResult Edit(Recipe recipe, int CategoryId)
    {
      if (CategoryId != 0)
      {
        _db.CategoryRecipe.Add(new CategoryRecipe() { CategoryId = CategoryId, RecipeId = recipe.RecipeId });
      }
      _db.Entry(recipe).State = EntityState.Modified;
      _db.SaveChanges();
      return RedirectToAction("Index");
    }

    public ActionResult AddCategory(int id)
    {
      var thisRecipe = _db.Recipes.FirstOrDefault(recipe => recipe.RecipeId == id);
      ViewBag.CategoryId = new SelectList(_db.Categories, "CategoryId", "Name");
      return View(thisRecipe);
    }

    [HttpPost]
    public ActionResult AddCategory(Recipe recipe, int CategoryId)
    {
      if (CategoryId != 0)
      {
      _db.CategoryRecipe.Add(new CategoryRecipe() { CategoryId = CategoryId, RecipeId = recipe.RecipeId });
      }
      _db.SaveChanges();
      return RedirectToAction("Index");
    }

    public ActionResult Delete(int id)
    {
      var thisRecipe = _db.Recipes.FirstOrDefault(recipe => recipe.RecipeId == id);
      return View(thisRecipe);
    }

    [HttpPost, ActionName("Delete")]
    public ActionResult DeleteConfirmed(int id)
    {
      var thisRecipe = _db.Recipes.FirstOrDefault(recipe => recipe.RecipeId == id);
      _db.Recipes.Remove(thisRecipe);
      _db.SaveChanges();
      return RedirectToAction("Index");
    }

    [HttpPost]
    public ActionResult DeleteCategory(int joinId)
    {
      var joinEntry = _db.CategoryRecipe.FirstOrDefault(entry => entry.CategoryRecipeId == joinId);
      _db.CategoryRecipe.Remove(joinEntry);
      _db.SaveChanges();
      return RedirectToAction("Index");
    }
  }
}