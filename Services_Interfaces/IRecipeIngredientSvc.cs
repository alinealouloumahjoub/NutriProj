using NutriProj.Models;
using NutriProj.Enums;
namespace NutriProj.Services_Interfaces;

public interface IRecipeIngredientService
{
    Task<List<RecipeIngredient>> GetByRecipe(int recipeId);
    Task Add(int recipeId, int ingredientId, double quantity);
    Task UpdateQuantity(int recipeIngredientId, double newQuantity);
    Task Remove(int recipeIngredientId);
    Task RemoveAllFromRecipe(int recipeId); 
    
}