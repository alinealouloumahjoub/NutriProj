using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using NutriProj.Models;

namespace NutriProj.Data;

public class AppDbContext : IdentityDbContext<IdentityUser>
{
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

    public DbSet<Ingredient> Ingredients { get; set; }
    public DbSet<Recipe> Recipes { get; set; }
    public DbSet<Unit> Units { get; set; }
    public DbSet<MealSlot> MealSlots { get; set; }
    public DbSet<RecipeIngredient> RecipeIngredients { get; set; }
    public DbSet<RecipeCategory> RecipeCategories { get; set; }
    public DbSet<MealPlanner> MealPlanners { get; set; }
    public DbSet<MealPlannerDetail> MealPlannerDetails { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.Entity<RecipeCategory>()
            .HasOne(rc => rc.Recipe)
            .WithMany(r => r.RecipeCategories)
            .HasForeignKey(rc => rc.IdRcp);

        modelBuilder.Entity<RecipeCategory>()
            .HasOne(rc => rc.MealSlot)
            .WithMany()
            .HasForeignKey(rc => rc.IdMealSlot);

        modelBuilder.Entity<MealPlannerDetail>()
            .HasOne(d => d.Recipe)
            .WithMany(r => r.MealPlannerDetails)
            .HasForeignKey(d => d.IdRcp);

        modelBuilder.Entity<MealPlannerDetail>()
            .HasOne(d => d.MealSlot)
            .WithMany()
            .HasForeignKey(d => d.IdMealSlot);

        modelBuilder.Entity<MealPlannerDetail>()
            .HasOne(d => d.MealPlanner)
            .WithMany(p => p.Details)
            .HasForeignKey(d => d.IdPlanner);
    }
}