using System.Collections.Generic;

namespace Factory.Models
{
  public class Category
  {
    public Category() 
    {
      this.JoinEntities = new HashSet<CategoryRecipe>();
    }
    public int CategoryId { get; set; }
    public string CategoryName { get; set; }
    public virtual ICollection<CategoryRecipe> JoinEntities { get; }

  }
}