using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Factory.Models
{
  public class Recipe
  {
    public Recipe() 
    {
      this.JoinEntities = new HashSet<CategoryRecipe>();
    }

    public int RecipeId { get; set; }
    public string RecipeName { get; set; }
    public string Ingredients { get; set; }
    public string Instructions { get; set; }
    public virtual ICollection<CategoryRecipe> JoinEntities { get; } 
  }
}