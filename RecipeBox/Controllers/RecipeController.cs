using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc;
using RecipeBox.Models;
using System.Collections.Generic;
using System.Linq;
using System;
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
      var userRecipes = _db.Recipes.Where(entry => entry.User.Id == currentUser.Id).ToList().OrderByDescending(entry=> entry.Rating);
      return View(userRecipes);
    }

    public ActionResult Create()
    {
        var ingredients = _db.Ingredients.Select(c => new { 
        IngredientId = c.IngredientId, 
        IngredientName = c.IngredientName 
    }).ToList();

      List<SelectListItem> Rating = new List<SelectListItem>();
      ViewBag.IngredientSelection = new MultiSelectList(ingredients, "IngredientId", "IngredientName");

      // foreach (Ingredient ingredient in _db.Ingredients)
      // {
      //   IngredientSelection.Add(new SelectListItem { Text = ingredient.IngredientName, Value= ingredient.IngredientName});
      // }

      Rating.Add(new SelectListItem { Text = "5", Value = "5"});
      Rating.Add(new SelectListItem { Text = "4", Value = "4"});
      Rating.Add(new SelectListItem { Text = "3", Value = "3"});
      Rating.Add(new SelectListItem { Text = "2", Value = "2"});
      Rating.Add(new SelectListItem { Text = "1", Value = "1"});

      // ViewBag.IngredientSelection = IngredientSelection;
      ViewBag.Rating = Rating;

      return View();
    }

    [HttpPost]
    public async Task<ActionResult> Create(Recipe recipe, int CategoryId)
    {
      var userId = this.User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
      var currentUser = await _userManager.FindByIdAsync(userId);
      recipe.User = currentUser;
      _db.Recipes.Add(recipe);
      _db.SaveChanges();
      if (CategoryId != 0)
      {
        _db.CategoryRecipe.Add(new CategoryRecipe() { CategoryId = CategoryId, RecipeId = recipe.RecipeId });
      }
      _db.SaveChanges();
      return RedirectToAction("Index");
    }

    public ActionResult Details(int id)
    {
      var thisRecipe = _db.Recipes
          .Include(Recipe => Recipe.JoinEntities)
          .ThenInclude(join => join.Category)
          .FirstOrDefault(Recipe => Recipe.RecipeId == id);
      return View(thisRecipe);
    }

    public ActionResult Edit(int id)
    {
      var thisRecipe = _db.Recipes.FirstOrDefault(Recipe => Recipe.RecipeId == id);
      ViewBag.CategoryId = new SelectList( _db.Categories, "CategoryId", "CategoryName");
      return View(thisRecipe);
    }

    [HttpPost]
    public ActionResult Edit(Recipe recipe)
    {
      _db.Entry(recipe).State = EntityState.Modified;
      _db.SaveChanges();
      return RedirectToAction("Index");
    }

    public ActionResult AddCategory(int id)
    {
      var thisRecipe = _db.Recipes.FirstOrDefault(Recipe => Recipe.RecipeId == id);
      ViewBag.CategoryId = new SelectList( _db.Categories, "CategoryId", "CategoryName");
      return View(thisRecipe);
    }

    [HttpPost]
    public ActionResult AddCategory(Recipe Recipe, int CategoryId)
    {
      if (CategoryId != 0)
      {
        _db.CategoryRecipe.Add(new CategoryRecipe() { RecipeId = Recipe.RecipeId, CategoryId = CategoryId });
        _db.SaveChanges();
      }
      return RedirectToAction("Index");
    }

    public ActionResult AddIngredient(int id)
    {
      var thisRecipe = _db.Recipes.FirstOrDefault(Recipe => Recipe.RecipeId == id);
      ViewBag.IngredientId = new SelectList( _db.Ingredients, "IngredientId", "IngredientName");
      return View(thisRecipe);
    }

    [HttpPost]
    public ActionResult AddIngredient(Recipe Recipe, int IngredientId)
    {
      if (IngredientId != 0)
      {
        _db.RecipeIngredient.Add(new RecipeIngredient() { RecipeId = Recipe.RecipeId, IngredientId = IngredientId });
        _db.SaveChanges();
      }
      return RedirectToAction("Index");
    }

    public ActionResult Delete(int id)
    {
      var thisRecipe = _db.Recipes.FirstOrDefault(Recipe => Recipe.RecipeId == id);
      return View(thisRecipe);
    }

    [HttpPost, ActionName("Delete")]
    public ActionResult DeleteConfirmed(int id)
    {
      var thisRecipe = _db.Recipes.FirstOrDefault(Recipe => Recipe.RecipeId == id);
      _db.Recipes.Remove(thisRecipe);
      _db.SaveChanges();
      return RedirectToAction("Index");
    }

    [HttpPost]
    public ActionResult RemoveIngredient(int joinId)
    {
      var joinEntry = _db.RecipeIngredient.FirstOrDefault(entry => entry.RecipeIngredientId == joinId);
      _db.RecipeIngredient.Remove(joinEntry);
      _db.SaveChanges();
      return RedirectToAction("Index");
    }
  }
}
