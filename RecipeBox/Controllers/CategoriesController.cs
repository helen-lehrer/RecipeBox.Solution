using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc;
using Factory.Models;
using System.Collections.Generic;
using System.Linq;

namespace Factory.Controllers
{
  public class CategoriesController : Controller
  {
    private readonly FactoryContext _db;

    public CategoriesController(FactoryContext db)
    {
      _db = db;
    }
    
    public ActionResult Index()
    {
      return View(_db.Categories.ToList());
    }

    public ActionResult Create()
    {
      return View();
    }

    [HttpPost]
    public ActionResult Create(Category Category)
    {
      _db.Categories.Add(Category);
      _db.SaveChanges();
      return RedirectToAction("Index");
    }

    public ActionResult Details(int id)
    {
      var thisCategory = _db.Categories
          .Include(Category => Category.JoinEntities)
          .ThenInclude(join => join.Recipe)
          .FirstOrDefault(Category => Category.CategoryId == id);
      return View(thisCategory);
    }

    public ActionResult Edit(int id)
    {
      var thisCategory = _db.Categories.FirstOrDefault(Category => Category.CategoryId == id);
      return View(thisCategory);
    }

    [HttpPost]
    public ActionResult Edit(Category Category)
    {
      _db.Entry(Category).State = EntityState.Modified;
      _db.SaveChanges();
      return RedirectToAction("Index");
    }

    public ActionResult AddRecipe(int id)
    {
      var thisCategory = _db.Categories.FirstOrDefault(Category => Category.CategoryId == id);
      ViewBag.RecipeId = new SelectList(_db.Recipes, "RecipeId", "RecipeName");
      return View(thisCategory);
    }

    [HttpPost]
    public ActionResult AddRecipe(Category Category, int RecipeId)
    {
      if (RecipeId != 0)
      {
        _db.CategoryRecipe.Add(new CategoryRecipe() { RecipeId = RecipeId, CategoryId = Category.CategoryId });
        _db.SaveChanges();
      }
      return RedirectToAction("Index");
    }

    public ActionResult Delete(int id)
    {
      var thisCategory = _db.Categories.FirstOrDefault(Category => Category.CategoryId == id);
      return View(thisCategory);
    }

    [HttpPost, ActionName("Delete")]
    public ActionResult DeleteConfirmed(int id)
    {
      var thisCategory = _db.Categories.FirstOrDefault(Category => Category.CategoryId == id);
      _db.Categories.Remove(thisCategory);
      _db.SaveChanges();
      return RedirectToAction("Index");
    }

    public ActionResult RemoveRecipe(int id)
    {
      var thisCategory = _db.Categories.FirstOrDefault(Category => Category.CategoryId == id);
      ViewBag.RecipeId = new SelectList( _db.Recipes, "RecipeId", "RecipeName");
      return View(thisCategory);
    }

    [HttpPost]
    public ActionResult RemoveRecipe(Category Category, int RecipeId)
    {
        var joinEntry = _db.CategoryRecipe.Where(entry => entry.RecipeId == RecipeId && entry.CategoryId == Category.CategoryId).FirstOrDefault();
        _db.CategoryRecipe.Remove(joinEntry);
        _db.SaveChanges();
        return RedirectToAction("Index");
    }
  }
}