using Microsoft.EntityFrameworkCore;
using NutriProj.Models;

namespace NutriProj.Data;

public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
    {
    }
   

    public DbSet<User> Users { get; set; } 
    public DbSet<Ingredient> Ingredients { get; set; }
    public DbSet<Recipe> Recipes { get; set; }
    public DbSet<Unit> Units { get; set; }
    public DbSet<MealSlot> MealSlots {get; set;}
    public DbSet<RecipeIngredient> RecipeIngredients { get; set; }
    public DbSet<MealPlanner> MealPlanners { get; set; }
    public DbSet<MealPlannerDetail> MealPlannerDetails { get; set; }
    public DbSet<RecipeCategory> RecipeCategories { get; set; }


}
