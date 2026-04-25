using Microsoft.EntityFrameworkCore;
using NutriProj.Data;
using NutriProj.Models;
using NutriProj.Services_Interfaces;

namespace NutriProj.Services;

public class UnitService : IUnitService
{
    private readonly AppDbContext _context;

    public UnitService(AppDbContext context)
    {
        _context = context;
    }

    public async Task<List<Unit>> GetAllUnits()
        => await _context.Units.OrderBy(u => u.Type).ThenBy(u => u.Name).ToListAsync();

    public async Task<Unit> GetUnitById(int id)
    {
        var unit = await _context.Units.FindAsync(id);
        if (unit == null)
            throw new Exception($"Unit with id {id} not found");
        return unit;
    }

    // filter by type: "weight", "volume", "count"
    // useful for the UI to show only relevant units when adding an ingredient
    public async Task<List<Unit>> GetByType(string type)
        => await _context.Units
                         .Where(u => u.Type.ToLower() == type.ToLower())
                         .OrderBy(u => u.Name)
                         .ToListAsync();

    public async Task AddUnit(Unit unit)
    {
        bool exists = await _context.Units
                                    .AnyAsync(u => u.Name.ToLower() == unit.Name.ToLower());
        if (exists)
            throw new Exception($"A unit named '{unit.Name}' already exists");

        _context.Units.Add(unit);
        await _context.SaveChangesAsync();
    }

    public async Task UpdateUnit(Unit unit)
    {
        _context.Units.Update(unit);
        await _context.SaveChangesAsync();
    }

    public async Task DeleteUnit(int id)
    {
        var unit = await _context.Units.FindAsync(id);
        if (unit == null) return;

        // cannot delete if used in recipe ingredients
        bool inUse = await _context.RecipeIngredients.AnyAsync(ri => ri.IdUnit == id);
        if (inUse)
            throw new Exception("Cannot delete this unit — it is used in one or more recipes");

        _context.Units.Remove(unit);
        await _context.SaveChangesAsync();
    }
}