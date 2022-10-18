using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace RecipeBox.Models
{
  public class Ingredient
  {
    public Ingredient()
    {
      this.JoinEntities2 = new HashSet<RecipeIngredient>();
    }

    public int IngredientId { get; set; }
    public string IngredientName { get; set; }
    public virtual ApplicationUser User { get; set; }
    public virtual ICollection<RecipeIngredient> JoinEntities2 { get; }
  }
}