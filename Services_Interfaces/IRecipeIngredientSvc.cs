using NutriProj.Models;
using NutriProj.Enums;
namespace NutriProj.Services_Interfaces;

public interface IRecipeIngredientService
{
    Task<List<RecipeIngredient>> GetByRecipe(int recipeId);
    Task Add(int recipeId, int ingredientId, double quantity , int unitId);
    Task UpdateQuantity(int recipeIngredientId, double newQuantity);
     Task UpdateUnit(int recipeIngredientId, int newUnitId);
    Task Remove(int recipeIngredientId);
    Task RemoveAllFromRecipe(int recipeId); 
    
}