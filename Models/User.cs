using System.ComponentModel.DataAnnotations;
namespace NutriProj.Models;
using NutriProj.Enums;
public class User
{
    [Key]
    public int IdUser { get; set; }

    [Required, MaxLength(100)]
    public string Username { get; set; } = string.Empty;

    [Required]
    public string PasswordHash { get; set; } = string.Empty;

    public UserRole Role { get; set; } = UserRole.User;

    public ICollection<MealPlanner> MealPlanners { get; set; } = new List<MealPlanner>();
}
