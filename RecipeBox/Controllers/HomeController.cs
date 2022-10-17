using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc;
using RecipeBox.Models;
using System.Collections.Generic;
using System.Linq;

namespace RecipeBox.Controllers
{
    public class HomeController : Controller
    {
      private readonly RecipeBoxContext _db;

      public HomeController(RecipeBoxContext db)
      {
        _db = db;
      }

      public ActionResult Index()
      {
        ViewBag.Recipe = new List<Recipe>( _db.Recipes);
        return View( _db.Categories.ToList());
      }
    }
}