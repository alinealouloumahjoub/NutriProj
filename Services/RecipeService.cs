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
    // this method is used to include the ingredients when fetching recipes, to avoid multiple database calls later
    private IQueryable<Recipe> WithIngredients()
    {
        return _context.Recipes.Include(r => r.RecipeIngredients)
                               .ThenInclude(ri => ri.Ingredient);
    }
    public async Task<List<Recipe>> GetAllRecipes()
    {
        return await WithIngredients().OrderBy(r => r.RecipeName).ToListAsync();
    }
    public async Task<Recipe> GetRecipeById(int id)
    {
        var recipe = await WithIngredients().FirstOrDefaultAsync(r => r.IdRcp == id);
        if (recipe == null)
            throw new Exception($"Recipe with id {id} not found");
        return recipe;
    }
    // we can filter by category or kitchen type using the same WithIngredients() method to avoid multiple DB calls
    public async Task<List<Recipe>> GetByCategory(MealSlot category)
    {
        return await WithIngredients().Where(r => r.Category == category).ToListAsync();
    }
    public async Task<List<Recipe>> GetByKitchenType(KitchenType kitchenType)
    {
        return await WithIngredients().Where(r => r.TypeKitchen == kitchenType).ToListAsync();
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
        var recipe = await _context.Recipes.Include(r => r.RecipeIngredients).FirstOrDefaultAsync(r => r.IdRcp == id);
        if (recipe == null) return;
        // we need to remove the related RecipeIngredients first to avoid foreign key constraint issues
        _context.RecipeIngredients.RemoveRange(recipe.RecipeIngredients);
        _context.Recipes.Remove(recipe);
        await _context.SaveChangesAsync();
    }
}