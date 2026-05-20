using NutriProj.Models;
using NutriProj.Enums;

namespace NutriProj.Services_Interfaces;

public interface IRecipeService
{
    Task<List<Recipe>> GetAllRecipes();
    Task<Recipe> GetRecipeById(int id);
    Task<List<Recipe>> GetByMealSlot(int mealSlotId);
    Task<List<Recipe>> GetByKitchenType(KitchenType kitchenType);
    Task<List<Recipe>> SearchRecipesAsync(string? searchText, KitchenType? kitchenType, int? mealSlotId, double? minCalories, double? maxCalories);
    Task AddRecipe(Recipe r);
    Task UpdateRecipe(Recipe r);
    Task DeleteRecipe(int id);
    // Category management
    Task AddCategory(int recipeId, int mealSlotId);
    Task RemoveCategory(int recipeCategoryId);
    Task<List<RecipeCategory>> GetCategories(int recipeId);
}