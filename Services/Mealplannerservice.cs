using Microsoft.EntityFrameworkCore;
using NutriProj.Data;
using NutriProj.Enums;
using NutriProj.Models;
using NutriProj.Services_Interfaces;

namespace NutriProj.Services;

public class MealPlannerService : IMealPlannerService
{
    private readonly AppDbContext _context;
    public MealPlannerService(AppDbContext context)
    {
        _context = context;
    }

    public async Task<MealPlanner> GetOrCreateWeekPlan(string userId, DateOnly weekStart, int dailyCalTarget)
    {
        var plan = await _context.MealPlanners
            .FirstOrDefaultAsync(p => p.UserId == userId && p.WeekStartDate == weekStart);
        if (plan != null) return plan;

        plan = new MealPlanner
        {
            UserId = userId,
            WeekStartDate = weekStart,
            DailyCaloriesTarget = dailyCalTarget
        };
        _context.MealPlanners.Add(plan);
        await _context.SaveChangesAsync();
        return plan;
    }

    public async Task<List<MealPlanner>> GetAllPlansByUser(string userId)
    {
        return await _context.MealPlanners
                         .Where(p => p.UserId == userId)
                         .OrderByDescending(p => p.WeekStartDate)
                         .ToListAsync();
    }

    public async Task AddDetail(int plannerId, int recipeId, DayOfWeekEnum day, int mealSlotId)
    {
        bool slotTaken = await _context.MealPlannerDetails
            .AnyAsync(d => d.IdPlanner == plannerId && d.Day == day && d.IdMealSlot == mealSlotId);
        if (slotTaken)
            throw new Exception($"A recipe is already planned for that slot on {day} — remove it first");

        _context.MealPlannerDetails.Add(new MealPlannerDetail
        {
            IdPlanner = plannerId,
            IdRcp = recipeId,
            Day = day,
            IdMealSlot = mealSlotId
        });
        await _context.SaveChangesAsync();
    }

    public async Task RemoveDetail(int detailId)
    {
        var detail = await _context.MealPlannerDetails.FindAsync(detailId);
        if (detail == null) return;
        _context.MealPlannerDetails.Remove(detail);
        await _context.SaveChangesAsync();
    }

    public async Task<List<MealPlannerDetail>> GetWeekDetails(int plannerId)
    {
        return await _context.MealPlannerDetails
                         .Include(d => d.MealSlot)
                         .Include(d => d.Recipe).ThenInclude(r => r.RecipeIngredients).ThenInclude(ri => ri.Ingredient)
                         .Include(d => d.Recipe).ThenInclude(r => r.RecipeIngredients).ThenInclude(ri => ri.Unit)
                         .Where(d => d.IdPlanner == plannerId)
                         .OrderBy(d => d.Day)
                         .ThenBy(d => d.MealSlot.Name)
                         .ToListAsync();
    }

    public async Task<double> GetDayCalories(int plannerId, DayOfWeekEnum day)
    {
        var details = await _context.MealPlannerDetails
                                    .Include(d => d.Recipe).ThenInclude(r => r.RecipeIngredients).ThenInclude(ri => ri.Ingredient)
                                    .Include(d => d.Recipe).ThenInclude(r => r.RecipeIngredients).ThenInclude(ri => ri.Unit)
                                    .Where(d => d.IdPlanner == plannerId && d.Day == day)
                                    .ToListAsync();
        return details.Sum(d => d.Recipe.TotalCalories);
    }

    public async Task<double> GetWeekTotalCalories(int plannerId)
    {
        var details = await GetWeekDetails(plannerId);
        return details.Sum(d => d.Recipe.TotalCalories);
    }
}