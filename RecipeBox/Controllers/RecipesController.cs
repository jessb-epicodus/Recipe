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

    public ActionResult Delete(int id, int ing1Id, int ing2Id, int ing3Id, int ing4Id, int ing5Id, int ing6Id, int ing7Id, int ing8Id, int ing9Id, int recIng1Id, int recIng2Id, int recIng3Id, int recIng4Id, int recIng5Id, int recIng6Id, int recIng7Id, int recIng8Id, int recIng9Id)
    {
      var thisRecipe = _db.Recipes
        .Include(recipe => recipe.JoinIngredients)
        .ThenInclude(join => join.Ingredient)
        .FirstOrDefault(recipe => recipe.RecipeId == id);
      var ing1 = _db.Ingredients.FirstOrDefault(ingredient => ingredient.IngredientId == ing1Id);
      var ing2 = _db.Ingredients.FirstOrDefault(ingredient => ingredient.IngredientId == ing2Id);
      var ing3 = _db.Ingredients.FirstOrDefault(ingredient => ingredient.IngredientId == ing3Id);
      var ing4 = _db.Ingredients.FirstOrDefault(ingredient => ingredient.IngredientId == ing4Id);
      var ing5 = _db.Ingredients.FirstOrDefault(ingredient => ingredient.IngredientId == ing5Id);
      var ing6 = _db.Ingredients.FirstOrDefault(ingredient => ingredient.IngredientId == ing6Id);
      var ing7 = _db.Ingredients.FirstOrDefault(ingredient => ingredient.IngredientId == ing7Id);
      var ing8 = _db.Ingredients.FirstOrDefault(ingredient => ingredient.IngredientId == ing8Id);
      var ing9 = _db.Ingredients.FirstOrDefault(ingredient => ingredient.IngredientId == ing9Id);
      var recIng1 = _db.RecipeIngredient.FirstOrDefault(recipeIngredient => recipeIngredient.RecipeIngredientId == recIng1Id);
      var recIng2 = _db.RecipeIngredient.FirstOrDefault(recipeIngredient => recipeIngredient.RecipeIngredientId == recIng2Id);
      var recIng3 = _db.RecipeIngredient.FirstOrDefault(recipeIngredient => recipeIngredient.RecipeIngredientId == recIng3Id);
      var recIng4 = _db.RecipeIngredient.FirstOrDefault(recipeIngredient => recipeIngredient.RecipeIngredientId == recIng4Id);
      var recIng5 = _db.RecipeIngredient.FirstOrDefault(recipeIngredient => recipeIngredient.RecipeIngredientId == recIng5Id);
      var recIng6 = _db.RecipeIngredient.FirstOrDefault(recipeIngredient => recipeIngredient.RecipeIngredientId == recIng6Id);
      var recIng7 = _db.RecipeIngredient.FirstOrDefault(recipeIngredient => recipeIngredient.RecipeIngredientId == recIng7Id);
      var recIng8 = _db.RecipeIngredient.FirstOrDefault(recipeIngredient => recipeIngredient.RecipeIngredientId == recIng8Id);
      var recIng9 = _db.RecipeIngredient.FirstOrDefault(recipeIngredient => recipeIngredient.RecipeIngredientId == recIng9Id);
      return View(thisRecipe);
    }

    [HttpPost, ActionName("Delete")]
    public ActionResult DeleteConfirmed(int id, int ing1Id, int ing2Id, int ing3Id, int ing4Id, int ing5Id, int ing6Id, int ing7Id, int ing8Id, int ing9Id, int recIng1Id, int recIng2Id, int recIng3Id, int recIng4Id, int recIng5Id, int recIng6Id, int recIng7Id, int recIng8Id, int recIng9Id)
    {
      var thisRecipe = _db.Recipes.FirstOrDefault(recipe => recipe.RecipeId == id);
      _db.Recipes.Remove(thisRecipe);
      _db.SaveChanges();
      var ing1 = _db.Ingredients.FirstOrDefault(ingredient => ingredient.IngredientId == ing1Id);
      var ing2 = _db.Ingredients.FirstOrDefault(ingredient => ingredient.IngredientId == ing2Id);
      var ing3 = _db.Ingredients.FirstOrDefault(ingredient => ingredient.IngredientId == ing3Id);
      var ing4 = _db.Ingredients.FirstOrDefault(ingredient => ingredient.IngredientId == ing4Id);
      var ing5 = _db.Ingredients.FirstOrDefault(ingredient => ingredient.IngredientId == ing5Id);
      var ing6 = _db.Ingredients.FirstOrDefault(ingredient => ingredient.IngredientId == ing6Id);
      var ing7 = _db.Ingredients.FirstOrDefault(ingredient => ingredient.IngredientId == ing7Id);
      var ing8 = _db.Ingredients.FirstOrDefault(ingredient => ingredient.IngredientId == ing8Id);
      var ing9 = _db.Ingredients.FirstOrDefault(ingredient => ingredient.IngredientId == ing9Id);
      _db.Ingredients.Remove(ing1);
      _db.Ingredients.Remove(ing2);
      _db.Ingredients.Remove(ing3);
      _db.Ingredients.Remove(ing4);
      _db.Ingredients.Remove(ing5);
      _db.Ingredients.Remove(ing6);
      _db.Ingredients.Remove(ing7);
      _db.Ingredients.Remove(ing8);
      _db.Ingredients.Remove(ing9);
      _db.SaveChanges();
      var recIng1 = _db.RecipeIngredient.FirstOrDefault(recipeIngredient => recipeIngredient.RecipeIngredientId == recIng1Id);
      var recIng2 = _db.RecipeIngredient.FirstOrDefault(recipeIngredient => recipeIngredient.RecipeIngredientId == recIng2Id);
      var recIng3 = _db.RecipeIngredient.FirstOrDefault(recipeIngredient => recipeIngredient.RecipeIngredientId == recIng3Id);
      var recIng4 = _db.RecipeIngredient.FirstOrDefault(recipeIngredient => recipeIngredient.RecipeIngredientId == recIng4Id);
      var recIng5 = _db.RecipeIngredient.FirstOrDefault(recipeIngredient => recipeIngredient.RecipeIngredientId == recIng5Id);
      var recIng6 = _db.RecipeIngredient.FirstOrDefault(recipeIngredient => recipeIngredient.RecipeIngredientId == recIng6Id);
      var recIng7 = _db.RecipeIngredient.FirstOrDefault(recipeIngredient => recipeIngredient.RecipeIngredientId == recIng7Id);
      var recIng8 = _db.RecipeIngredient.FirstOrDefault(recipeIngredient => recipeIngredient.RecipeIngredientId == recIng8Id);
      var recIng9 = _db.RecipeIngredient.FirstOrDefault(recipeIngredient => recipeIngredient.RecipeIngredientId == recIng9Id);
      _db.RecipeIngredient.Remove(recIng1);
      _db.RecipeIngredient.Remove(recIng2);
      _db.RecipeIngredient.Remove(recIng3);
      _db.RecipeIngredient.Remove(recIng4);
      _db.RecipeIngredient.Remove(recIng5);
      _db.RecipeIngredient.Remove(recIng6);
      _db.RecipeIngredient.Remove(recIng7);
      _db.RecipeIngredient.Remove(recIng8);
      _db.RecipeIngredient.Remove(recIng9);
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