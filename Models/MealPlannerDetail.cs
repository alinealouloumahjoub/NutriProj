using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using NutriProj.Enums;
namespace NutriProj.Models;

public class MealPlannerDetail
{
    [Key]
    public int IdDetail { get; set; }
    public int IdPlanner { get; set; }
    public int IdRcp { get; set; }
    public int IdMealSlot { get; set; }
    public DayOfWeekEnum Day { get; set; }

    [ForeignKey(nameof(IdPlanner))]
    public MealPlanner MealPlanner { get; set; } = null!;
    [ForeignKey(nameof(IdRcp))]
    public Recipe Recipe { get; set; } = null!;
    [ForeignKey(nameof(IdMealSlot))]
    public MealSlot MealSlot { get; set; } = null!;
}