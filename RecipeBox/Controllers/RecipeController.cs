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
      var userRecipes = _db.Recipes.Where(entry => entry.User.Id == currentUser.Id).ToList();
      return View(userRecipes);
    }

    public ActionResult Create()
    {
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
    public ActionResult Edit(Recipe Recipe)
    {
        _db.Entry(Recipe).State = EntityState.Modified;
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

    public ActionResult RemoveCategory(int id)
    {
      var thisRecipe = _db.Recipes.FirstOrDefault(Recipe => Recipe.RecipeId == id);
      ViewBag.CategoryId = new SelectList( _db.Categories, "CategoryId", "CategoryName");
      return View(thisRecipe);
    }

    [HttpPost]
    public ActionResult RemoveCategory(Recipe Recipe, int CategoryId)
    {
        var joinEntry = _db.CategoryRecipe.Where(entry => entry.RecipeId == Recipe.RecipeId && entry.CategoryId == CategoryId).FirstOrDefault();
        _db.CategoryRecipe.Remove(joinEntry);
        _db.SaveChanges();
        return RedirectToAction("Index");
    }
  }
}
