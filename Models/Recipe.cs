using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using NutriProj.Enums;

namespace NutriProj.Models;

public class Recipe
{
    [Key]
    public int IdRcp { get; set; }

    [Required, MaxLength(200)]
    public string RecipeName { get; set; } = string.Empty;

    public KitchenType TypeKitchen { get; set; }
    

    [Range(0, 1440)]
    public int TimePreparation { get; set; }

    [Range(0, 1440)]
    public int TimeCooking { get; set; }

    [Range(0, 1440)]
    public int TimeRest { get; set; }
    // total time will not be stored in the recipe table 
    [NotMapped]

    public double TotalCalories => RecipeIngredients.Sum(ri => (ri.Quantity / 100.0) * (ri.Ingredient?.CaloriesPer100g ?? 0));
    

    public ICollection<RecipeIngredient> RecipeIngredients { get; set; } = new List<RecipeIngredient>();
    public ICollection<MealPlannerDetail> MealPlannerDetails { get; set; } = new List<MealPlannerDetail>();
   


   
}
