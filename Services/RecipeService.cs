using Microsoft.EntityFrameworkCore;
using NutriProj.Data;
using NutriProj.Enums;
using NutriProj.Models;
using NutriProj.Services_Interfaces;

namespace NutriProj.Services;

public class RecipeService : IRecipeService
{
    private readonly AppDbContext _context;
    public RecipeService(AppDbContext context)
    {
        _context = context;
    }

    private IQueryable<Recipe> WithAll()
    {
        return _context.Recipes
                       .Include(r => r.RecipeIngredients).ThenInclude(ri => ri.Ingredient)
                       .Include(r => r.RecipeIngredients).ThenInclude(ri => ri.Unit)
                       .Include(r => r.RecipeCategories).ThenInclude(rc => rc.MealSlot);
    }

    public async Task<List<Recipe>> GetAllRecipes()
    {
        return await WithAll().OrderBy(r => r.RecipeName).ToListAsync();
    }

    public async Task<Recipe> GetRecipeById(int id)
    {
        var recipe = await WithAll().FirstOrDefaultAsync(r => r.IdRcp == id);
        if (recipe == null)
            throw new Exception($"Recipe with id {id} not found");
        return recipe;
    }

    public async Task<List<Recipe>> GetByMealSlot(int mealSlotId)
    {
        return await WithAll()
                     .Where(r => r.RecipeCategories.Any(rc => rc.IdMealSlot == mealSlotId))
                     .ToListAsync();
    }

    public async Task<List<Recipe>> GetByKitchenType(KitchenType kitchenType)
    {
        return await WithAll()
                     .Where(r => r.TypeKitchen == kitchenType)
                     .ToListAsync();
    }

    // Dynamic search with LINQ (same pattern as professor's SearchSensorsAsync)
    public async Task<List<Recipe>> SearchRecipesAsync(string? searchText, KitchenType? kitchenType, int? mealSlotId, double? minCalories, double? maxCalories)
    {
        // AsQueryable() prepares a query without executing it
        IQueryable<Recipe> query = WithAll().AsQueryable();

        if (!string.IsNullOrEmpty(searchText))
            query = query.Where(r => r.RecipeName.Contains(searchText));

        if (kitchenType.HasValue)
            query = query.Where(r => r.TypeKitchen == kitchenType.Value);

        if (mealSlotId.HasValue)
            query = query.Where(r => r.RecipeCategories.Any(rc => rc.IdMealSlot == mealSlotId.Value));

        // SQL executes only here with ToListAsync()
        var results = await query.OrderBy(r => r.RecipeName).ToListAsync();

        // Calorie filter is done in memory (TotalCalories is [NotMapped])
        if (minCalories.HasValue)
            results = results.Where(r => r.TotalCalories >= minCalories.Value).ToList();

        if (maxCalories.HasValue)
            results = results.Where(r => r.TotalCalories <= maxCalories.Value).ToList();

        return results;
    }

    public async Task AddRecipe(Recipe r)
    {
        _context.Recipes.Add(r);
        await _context.SaveChangesAsync();
    }

    public async Task UpdateRecipe(Recipe r)
    {
        _context.Recipes.Update(r);
        await _context.SaveChangesAsync();
    }

    public async Task DeleteRecipe(int id)
    {
        var recipe = await _context.Recipes
                                   .Include(r => r.RecipeIngredients)
                                   .Include(r => r.RecipeCategories)
                                   .FirstOrDefaultAsync(r => r.IdRcp == id);
        if (recipe == null) return;

        _context.RecipeIngredients.RemoveRange(recipe.RecipeIngredients);
        _context.RecipeCategories.RemoveRange(recipe.RecipeCategories);
        _context.Recipes.Remove(recipe);
        await _context.SaveChangesAsync();
    }

    public async Task AddCategory(int recipeId, int mealSlotId)
    {
        bool alreadyLinked = await _context.RecipeCategories
            .AnyAsync(rc => rc.IdRcp == recipeId && rc.IdMealSlot == mealSlotId);
        if (alreadyLinked)
            throw new Exception("This recipe already has this category");

        _context.RecipeCategories.Add(new RecipeCategory
        {
            IdRcp = recipeId,
            IdMealSlot = mealSlotId
        });
        await _context.SaveChangesAsync();
    }

    public async Task RemoveCategory(int recipeCategoryId)
    {
        var rc = await _context.RecipeCategories.FindAsync(recipeCategoryId);
        if (rc == null) return;
        _context.RecipeCategories.Remove(rc);
        await _context.SaveChangesAsync();
    }

    public async Task<List<RecipeCategory>> GetCategories(int recipeId)
    {
        return await _context.RecipeCategories
                             .Include(rc => rc.MealSlot)
                             .Where(rc => rc.IdRcp == recipeId)
                             .ToListAsync();
    }
}