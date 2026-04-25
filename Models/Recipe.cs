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
    //Example: 2 cups of flour => 2 × 120g (GramEquivalent) / 100 × 364 kcal/100g = 873.6 kcal
    [NotMapped]
    public double TotalCalories => RecipeIngredients.Sum(ri => (ri.Quantity * (ri.Unit?.GramEquivalent ?? 1) / 100.0) * (ri.Ingredient?.CaloriesPer100g ?? 0));
    public ICollection<RecipeIngredient> RecipeIngredients { get; set; } = new List<RecipeIngredient>();
    public ICollection<MealPlannerDetail> MealPlannerDetails { get; set; } = new List<MealPlannerDetail>();
     public ICollection<RecipeCategory> RecipeCategories { get; set; } = new List<RecipeCategory>();
   


   
}
