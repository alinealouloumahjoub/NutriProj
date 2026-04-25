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

    // this method loads ingredients + unit + categories in one query using SQL JOINs
    // we create it to avoid multiple DB calls when we need to calculate TotalCalories or display categories
    private IQueryable<Recipe> WithAll()
    {
        return _context.Recipes
                       .Include(r => r.RecipeIngredients).ThenInclude(ri => ri.Ingredient) // join Unit for GramEquivalent
                       .Include(r => r.RecipeIngredients).ThenInclude(ri => ri.Unit) //  join RecipeCategory         
                       .Include(r => r.RecipeCategories).ThenInclude(rc => rc.MealSlot);     //join MealSlot name
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
                                   .Include(r => r.RecipeCategories) // NEW: remove categories too
                                   .FirstOrDefaultAsync(r => r.IdRcp == id);
        if (recipe == null) return;

        // we need to remove related rows first to avoid foreign key constraint issues
        _context.RecipeIngredients.RemoveRange(recipe.RecipeIngredients);
        _context.RecipeCategories.RemoveRange(recipe.RecipeCategories); // NEW
        _context.Recipes.Remove(recipe);
        await _context.SaveChangesAsync();
    }

    //we assigned a MealSlot category to a recipe (a recipe can have multiple)
    public async Task AddCategory(int recipeId, int mealSlotId)
    {
        bool alreadyLinked = await _context.RecipeCategories.AnyAsync(rc => rc.IdRcp == recipeId && rc.IdMealSlot  == mealSlotId);
        if (alreadyLinked)
            throw new Exception("This recipe already has this category");

        _context.RecipeCategories.Add(new RecipeCategory
        {
            IdRcp= recipeId,
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