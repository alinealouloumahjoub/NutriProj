using System.ComponentModel.DataAnnotations;
using NutriProj.Enums;
namespace NutriProj.Models;

public class MealPlannerDetail
{
    [Key]
    public int IdDetail { get; set; }

    public int IdPlanner { get; set; }
    public int IdRcp { get; set; }

    public DayOfWeekEnum Day { get; set; }
    public MealSlot MealSlot { get; set; }

    public MealPlanner MealPlanner { get; set; } = null!;
    public Recipe Recipe { get; set; } = null!;
}
