using Microsoft.EntityFrameworkCore;
using NutriProj.Data;
using NutriProj.Models;
using NutriProj.Services_Interfaces;
namespace NutriProj.Services;

public class IngredientService : IIngredientService
{
    private readonly AppDbContext _context;
    public IngredientService(AppDbContext context){
        _context = context;
    }

    public async Task<List<Ingredient>> GetAllIngredients()
    {
        return await _context.Ingredients.OrderBy(i => i.NameIng).ToListAsync();
    }

    public async Task<Ingredient> GetIngredientById(int id)
    {
        var ingredient = await _context.Ingredients.FindAsync(id);
        if (ingredient == null)
            throw new Exception($"Ingredient with id {id} not found");
        return ingredient;
    }

    public async Task AddIngredient(Ingredient i)
    {
        // we check if an ingredient with the same name already exists to avoid duplicates
        bool exists = await _context.Ingredients.AnyAsync(x => x.NameIng.ToLower() == i.NameIng.ToLower());
        if (exists)
            throw new Exception($"An ingredient named '{i.NameIng}' already exists");
        _context.Ingredients.Add(i);
        await _context.SaveChangesAsync(); 
    }

    public async Task UpdateIngredient(Ingredient i)
    {
        _context.Ingredients.Update(i);
        await _context.SaveChangesAsync(); 
    }

    public async Task DeleteIngredient(int id)
    {
        var ingredient = await _context.Ingredients.FindAsync(id);
        if (ingredient == null) return;
        // we cannot delete an ingredient if it is used in a recipe
        bool usedInRecipe = await _context.RecipeIngredients.AnyAsync(ri => ri.IdIng == id);
        if (usedInRecipe)
            throw new Exception("Cannot delete this ingredient !! it is used in one or more recipes");

        _context.Ingredients.Remove(ingredient);
        await _context.SaveChangesAsync(); 
    }
}