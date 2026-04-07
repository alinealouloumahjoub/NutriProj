using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
 namespace NutriProj.Models;
public class RecipeCategory
{
    [Key]
    public int IdCategory { get; set; }
    public int IdRcp { get; set; }
    public int IdMealSlot { get; set; }

    public Recipe Recipe { get; set; } = null!;
    public MealSlot MealSlot { get; set; } = null!;
}