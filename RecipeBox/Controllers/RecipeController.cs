using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc;
using Factory.Models;
using System.Collections.Generic;
using System.Linq;
using System;

namespace Factory.Controllers
{
  public class RecipesController : Controller
  {
    private readonly FactoryContext _db;

    public RecipesController(FactoryContext db)
    {
      _db = db;
    }

    public ActionResult Index()
    {
      return View( _db.Recipes.ToList().OrderBy(model => model.RecipeName).ToList());
    }

    public ActionResult Create()
    {
      return View();
    }

    [HttpPost]
    public ActionResult Create(Recipe Recipe, int CategoryId)
    {
      _db.Recipes.Add(Recipe);
      _db.SaveChanges();
      if (CategoryId != 0)
      {
        _db.CategoryRecipe.Add(new CategoryRecipe() { CategoryId = CategoryId, RecipeId = Recipe.RecipeId });
        _db.SaveChanges();
      }
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
