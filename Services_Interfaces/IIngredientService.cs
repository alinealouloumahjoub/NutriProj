using NutriProj.Models;
namespace NutriProj.Services_Interfaces;
 public interface IIngredientService
{
    Task <List<Ingredient>> GetAllIngredients();
    Task <Ingredient> GetIngredientById(int id);
    Task  AddIngredient (Ingredient i) ;
    Task  UpdateIngredient (Ingredient i) ;
    Task  DeleteIngredient (int id) ;
}