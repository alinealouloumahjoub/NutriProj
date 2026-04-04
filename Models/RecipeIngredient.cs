using System.ComponentModel.DataAnnotations;
namespace NutriProj.Models;

public class RecipeIngredient
{
    [Key]
    public int IdRecIng { get; set; }
    public int IdRcp { get; set; }
    public int IdIng { get; set; }

    [Range(0.1, 10000)]
    public double Quantity { get; set; }

    [MaxLength(20)]
    public string Unit { get; set; } = "g";

    public Recipe Recipe { get; set; } = null!;
    public Ingredient Ingredient { get; set; } = null!;
}
