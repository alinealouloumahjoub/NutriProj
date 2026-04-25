// Models/Unit.cs
using System.ComponentModel.DataAnnotations;
namespace NutriProj.Models;

public class Unit
{
    [Key]
    public int IdUnit { get; set; }

    [Required, MaxLength(30)]
    // Examples: "g", "ml", "cup", "tbsp", "tsp", "piece", "slice"
    public string Name { get; set; } = string.Empty;
     // "weight", "volume", "count"
    [Required, MaxLength(20)]
    public string Type { get; set; } = string.Empty;

    // How many grams does 1 of this unit equal? (to calculate calories )
    [Range(0, 10000)]
    public double GramEquivalent { get; set; }

    public ICollection<RecipeIngredient> RecipeIngredients { get; set; } = new List<RecipeIngredient>();
}