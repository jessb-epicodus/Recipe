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
    public async Task<ActionResult> Create(Recipe recipe, string Ingredient1, string Ingredient2, string Ingredient3, string Ingredient4, string Ingredient5, string Ingredient6, string Ingredient7, string Ingredient8, string Ingredient9, string IngredientRecipe1, string IngredientRecipe2, string IngredientRecipe3, string IngredientRecipe4, string IngredientRecipe5, string IngredientRecipe6, string IngredientRecipe7, string IngredientRecipe8, string IngredientRecipe9, int RecipeId, int IngredientId, int RecipeIngredientId, int CategoryId)
    {
      var userId = this.User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
      var currentUser = await _userManager.FindByIdAsync(userId);
      recipe.User = currentUser;
      _db.Recipes.Add(recipe);
      Ingredient ing1 = new Ingredient() { Name = Ingredient1 };
      _db.Ingredients.Add(ing1);
      Ingredient ing2 = new Ingredient() { Name = Ingredient2 };
      _db.Ingredients.Add(ing2);
      Ingredient ing3 = new Ingredient() { Name = Ingredient3 };
      _db.Ingredients.Add(ing3);
      Ingredient ing4 = new Ingredient() { Name = Ingredient4 };
      _db.Ingredients.Add(ing4);
      Ingredient ing5 = new Ingredient() { Name = Ingredient5 };
      _db.Ingredients.Add(ing5);
      Ingredient ing6 = new Ingredient() { Name = Ingredient6 };
      _db.Ingredients.Add(ing6);
      Ingredient ing7 = new Ingredient() { Name = Ingredient7 };
      _db.Ingredients.Add(ing7);
      Ingredient ing8 = new Ingredient() { Name = Ingredient8 };
      _db.Ingredients.Add(ing8);
      Ingredient ing9 = new Ingredient() { Name = Ingredient9 };
      _db.Ingredients.Add(ing9);
      _db.SaveChanges();
      RecipeIngredient recIng1 = new RecipeIngredient() { IngredientId = ing1.IngredientId, RecipeId = recipe.RecipeId };       
      RecipeIngredient recIng2 = new RecipeIngredient() { IngredientId = ing2.IngredientId, RecipeId = recipe.RecipeId }; 
      RecipeIngredient recIng3 = new RecipeIngredient() { IngredientId = ing3.IngredientId, RecipeId = recipe.RecipeId }; 
      RecipeIngredient recIng4 = new RecipeIngredient() { IngredientId = ing4.IngredientId, RecipeId = recipe.RecipeId };       
      RecipeIngredient recIng5 = new RecipeIngredient() { IngredientId = ing5.IngredientId, RecipeId = recipe.RecipeId }; 
      RecipeIngredient recIng6 = new RecipeIngredient() { IngredientId = ing6.IngredientId, RecipeId = recipe.RecipeId }; 
      RecipeIngredient recIng7 = new RecipeIngredient() { IngredientId = ing7.IngredientId, RecipeId = recipe.RecipeId };       
      RecipeIngredient recIng8 = new RecipeIngredient() { IngredientId = ing8.IngredientId, RecipeId = recipe.RecipeId }; 
      RecipeIngredient recIng9 = new RecipeIngredient() { IngredientId = ing9.IngredientId, RecipeId = recipe.RecipeId }; 
      _db.RecipeIngredient.Add(recIng1);
      _db.RecipeIngredient.Add(recIng2);
      _db.RecipeIngredient.Add(recIng3);
      _db.RecipeIngredient.Add(recIng4);
      _db.RecipeIngredient.Add(recIng5);
      _db.RecipeIngredient.Add(recIng6);
      _db.RecipeIngredient.Add(recIng7);
      _db.RecipeIngredient.Add(recIng8);
      _db.RecipeIngredient.Add(recIng9);
      _db.SaveChanges();
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