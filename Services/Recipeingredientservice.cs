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
    //we used sql join (include) to get ingredient name and calories 
    
    public async Task<List<RecipeIngredient>> GetByRecipe(int recipeId){
        return  await _context.RecipeIngredients.Include(ri => ri.Ingredient).Where(ri => ri.IdRcp == recipeId).ToListAsync();}

    public async Task Add(int recipeId, int ingredientId, double qte)
    {
        // we check if this ingredient is already in the recipe to avoid duplicate ingredients in the same recipe
        bool exists = await _context.RecipeIngredients.AnyAsync(ri => ri.IdRcp == recipeId && ri.IdIng == ingredientId);
        if (exists)
            throw new Exception("This ingredient is already in the recipe!! Update its quantity instead");
        _context.RecipeIngredients.Add(new RecipeIngredient
        {
            IdRcp= recipeId,
            IdIng= ingredientId,
            Quantity= qte,
            Unit= "g"});
        await _context.SaveChangesAsync();
    }

    public async Task UpdateQuantity(int recipeIngredientId, double newQuantity)
    {
        var ri = await _context.RecipeIngredients.FindAsync(recipeIngredientId);
        if (ri == null) return;
        ri.Quantity = newQuantity;
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
        var ri= await _context.RecipeIngredients.Where(ri => ri.IdRcp == recipeId).ToListAsync();
        _context.RecipeIngredients.RemoveRange(ri);
        await _context.SaveChangesAsync();
    }

    
}