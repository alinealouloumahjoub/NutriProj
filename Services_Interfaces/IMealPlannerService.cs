using NutriProj.Enums;
using NutriProj.Models;
namespace NutriProj.Services_Interfaces;

public interface IMealPlannerService
{
    Task<MealPlanner> GetOrCreateWeekPlan(int userId, DateOnly weekStart, int dailyCalTarget);
    Task<List<MealPlanner>> GetAllPlansByUser(int userId);
    Task AddDetail(int plannerId, int recipeId, DayOfWeekEnum day, int mealSlotId);
    Task RemoveDetail(int detailId);
    Task<List<MealPlannerDetail>> GetWeekDetails(int plannerId);
    Task<double> GetDayCalories(int plannerId, DayOfWeekEnum day);
    Task<double> GetWeekTotalCalories(int plannerId);
}