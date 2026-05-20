using System.ComponentModel.DataAnnotations;

namespace NutriProj.Models;

public class MealPlanner
{
    [Key]
    public int IdPlanner { get; set; }

    [Required]
    public DateOnly WeekStartDate { get; set; }

    [Range(500, 5000)]
    public int DailyCaloriesTarget { get; set; }

    public string? UserId { get; set; }

    public ICollection<MealPlannerDetail> Details { get; set; } = new List<MealPlannerDetail>();
}