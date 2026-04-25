using Microsoft.EntityFrameworkCore;
using NutriProj.Data;
using NutriProj.Models;
using NutriProj.Services_Interfaces;

namespace NutriProj.Services;

public class RecipeIngredientService : IRecipeIngredientService
{
    private readonly AppDbContext _context;

    public RecipeIngredientService(AppDbContext context)
    {
        _context = context;
    }

    // we include Ingredient AND Unit in the JOIN so the UI gets
    // the ingredient name, calories, and unit name in one query
    public async Task<List<RecipeIngredient>> GetByRecipe(int recipeId)
    {
        return await _context.RecipeIngredients
                             .Include(ri => ri.Ingredient)
                             .Include(ri => ri.Unit)        // NEW: join Unit table
                             .Where(ri => ri.IdRcp == recipeId)
                             .ToListAsync();
    }

    // Unit is now an int FK (IdUnit) instead of a plain string
    public async Task Add(int recipeId, int ingredientId, double quantity, int unitId)
    {
        // we check if this ingredient is already in the recipe to avoid duplicates
        bool exists = await _context.RecipeIngredients
                                    .AnyAsync(ri => ri.IdRcp == recipeId && ri.IdIng == ingredientId);
        if (exists)
            throw new Exception("This ingredient is already in the recipe — update its quantity instead");
        // verify the unit exists before saving
        bool unitExists = await _context.Units.AnyAsync(u => u.IdUnit == unitId);
        if (!unitExists)
            throw new Exception($"Unit with id {unitId} not found");

        _context.RecipeIngredients.Add(new RecipeIngredient
        {
            IdRcp= recipeId,
            IdIng = ingredientId,
            Quantity = quantity,
            IdUnit= unitId     
        });
        await _context.SaveChangesAsync();
    }
    public async Task UpdateQuantity(int recipeIngredientId, double newQuantity)
    {
        var ri = await _context.RecipeIngredients.FindAsync(recipeIngredientId);
        if (ri == null) return;

        ri.Quantity = newQuantity;
        await _context.SaveChangesAsync();
    }
    public async Task UpdateUnit(int recipeIngredientId, int newUnitId)
    {
        var ri = await _context.RecipeIngredients.FindAsync(recipeIngredientId);
        if (ri == null) return;

        bool unitExists = await _context.Units.AnyAsync(u => u.IdUnit == newUnitId);
        if (!unitExists)
            throw new Exception($"Unit with id {newUnitId} not found");

        ri.IdUnit = newUnitId;
        await _context.SaveChangesAsync();
    }

    public async Task Remove(int recipeIngredientId)
    {
        var ri = await _context.RecipeIngredients.FindAsync(recipeIngredientId);
        if (ri == null) return;

        _context.RecipeIngredients.Remove(ri);
        await _context.SaveChangesAsync();
    }

    public async Task RemoveAllFromRecipe(int recipeId)
    {
        var lines = await _context.RecipeIngredients
                                  .Where(ri => ri.IdRcp == recipeId)
                                  .ToListAsync();
        _context.RecipeIngredients.RemoveRange(lines);
        await _context.SaveChangesAsync();
    }
}