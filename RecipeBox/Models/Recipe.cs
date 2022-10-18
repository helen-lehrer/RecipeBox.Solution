using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace RecipeBox.Models
{
  public class Recipe
  {
    public Recipe() 
    {
      this.JoinEntities = new HashSet<CategoryRecipe>();
      this.JoinEntities2 = new HashSet<RecipeIngredient>();
    }

    public int RecipeId { get; set; }
    public string RecipeName { get; set; }
    public string Rating { get; set; }
    public string Instructions { get; set; }
    public virtual ApplicationUser User { get; set; }
    public virtual ICollection<CategoryRecipe> JoinEntities { get; } 
    public virtual ICollection<RecipeIngredient> JoinEntities2 { get; }
  }
}