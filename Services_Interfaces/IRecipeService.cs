using NutriProj.Models;
using NutriProj.Enums;
namespace NutriProj.Services_Interfaces;
 public interface IRecipeService
{
    Task <List<Recipe>> GetAllRecipes();
    Task <Recipe> GetRecipeById(int id);
    Task<List<Recipe>> GetByCategory( MealSlot category);
    Task<List<Recipe>> GetByKitchenType(KitchenType kitchenType);
    Task  AddRecipe (Recipe r) ;
    Task  UpdateRecipe (Recipe r) ;
    Task  DeleteRecipe (int id) ;
}