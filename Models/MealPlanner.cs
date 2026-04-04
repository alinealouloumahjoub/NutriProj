using System.ComponentModel.DataAnnotations;
using NutriProj.Enums;
namespace NutriProj.Models;

public class MealPlanner
{
    [Key]
    public int IdPlanner { get; set; }
    [Required]
    public DateOnly WeekStartDate { get; set; }

    [Range(500, 5000)]
    public int DailyCaloriesTarget { get; set; }
    public int IdUser { get; set; }
    public User User { get; set; } = null!;

    public ICollection<MealPlannerDetail> Details { get; set; } = new List<MealPlannerDetail>();
}
