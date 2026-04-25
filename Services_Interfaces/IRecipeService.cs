using NutriProj.Models;
using NutriProj.Enums;
namespace NutriProj.Services_Interfaces;
 public interface IRecipeService
{
    Task <List<Recipe>> GetAllRecipes();
    Task <Recipe> GetRecipeById(int id);
    Task<List<Recipe>> GetByMealSlot(int mealSlotId);
    Task<List<Recipe>> GetByKitchenType(KitchenType kitchenType);
    Task  AddRecipe (Recipe r) ;
    Task  UpdateRecipe (Recipe r) ;
    Task  DeleteRecipe (int id) ;
    //Category Management
    Task AddCategory(int recipeId, int mealSlotId);
    Task RemoveCategory(int recipeCategoryId);
    Task<List<RecipeCategory>> GetCategories(int recipeId);
}