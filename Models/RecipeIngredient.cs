using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
namespace NutriProj.Models;

public class RecipeIngredient
{
    [Key]
    public int IdRecIng { get; set; }
    public int IdRcp { get; set; }
    public int IdIng { get; set; }
    public int IdUnit { get; set;}

    [Range(0.1, 10000)]
    public double Quantity { get; set; }
    
    [ForeignKey(nameof(IdRcp))]
    public Recipe Recipe { get; set; } = null!;
    [ForeignKey(nameof(IdIng))]
    public Ingredient Ingredient { get; set; } = null!;
    [ForeignKey(nameof(IdUnit))]
    public Unit Unit { get; set; } = null!;
}
