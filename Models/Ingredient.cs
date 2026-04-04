using System.ComponentModel.DataAnnotations;
namespace NutriProj.Models;
public class Ingredient
{

    [Key]
    public int IdIng { get; set; }

    [Required, MaxLength(150)]
    public string NameIng { get; set; } = string.Empty;

    [Range(0, 9000)]
    public double CaloriesPer100g { get; set; }

    public ICollection<RecipeIngredient> RecipeIngredients { get; set; } = new List<RecipeIngredient>();
}
