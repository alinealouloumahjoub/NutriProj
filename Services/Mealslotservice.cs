using Microsoft.EntityFrameworkCore;
using NutriProj.Data;
using NutriProj.Models;
using NutriProj.Services_Interfaces;

namespace NutriProj.Services;

public class MealSlotService : IMealSlotService
{
    private readonly AppDbContext _context;

    public MealSlotService(AppDbContext context)
    {
        _context = context;
    }

    public async Task<List<MealSlot>> GetAllMealSlots(){
        return await _context.MealSlots.OrderBy(m => m.Name).ToListAsync();}

    public async Task<MealSlot> GetMealSlotById(int id)
    {
        var slot = await _context.MealSlots.FindAsync(id);
        if (slot == null)
            throw new Exception($"MealSlot with id {id} not found");
        return slot;
    }
    public async Task AddMealSlot(MealSlot slot)
    {
        bool exists = await _context.MealSlots.AnyAsync(m => m.Name.ToLower() == slot.Name.ToLower());
        if (exists)
            throw new Exception($"The meal slot '{slot.Name}' already exists");
        _context.MealSlots.Add(slot);
        await _context.SaveChangesAsync();
    }

    public async Task UpdateMealSlot(MealSlot slot)
    {
        _context.MealSlots.Update(slot);
        await _context.SaveChangesAsync();
    }

    public async Task DeleteMealSlot(int id)
    {
        var slot = await _context.MealSlots.FindAsync(id);
        if (slot == null) return;
        // cannot delete if used in recipe categories or meal planner details
        bool usedInCategory = await _context.RecipeCategories.AnyAsync(rc => rc.IdMealSlot == id);
        bool usedInPlanner  = await _context.MealPlannerDetails.AnyAsync(d => d.IdMealSlot == id);
        if (usedInCategory || usedInPlanner)
            throw new Exception("Cannot delete this meal slot because it is currently in use");

        _context.MealSlots.Remove(slot);
        await _context.SaveChangesAsync();
    }
}