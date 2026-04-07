using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
namespace NutriProj.Models;
public class MealSlot
{
    [Key]
    public int IdMealSlot { get; set; }

    [Required, MaxLength(20)]
    public string Name { get; set; } 
}