using NutriProj.Models;
namespace NutriProj.Services_Interfaces;

public interface IMealSlotService
{
    Task<List<MealSlot>> GetAllMealSlots();
    Task<MealSlot> GetMealSlotById(int id);
    Task AddMealSlot(MealSlot slot);
    Task UpdateMealSlot(MealSlot slot);
    Task DeleteMealSlot(int id);
}