using NutriProj.Models;
namespace NutriProj.Services_Interfaces;

public interface IUnitService
{
    Task<List<Unit>> GetAllUnits();
    Task<Unit> GetUnitById(int id);
    Task<List<Unit>> GetByType(string type); // "weight", "volume", "count"
    Task AddUnit(Unit unit);
    Task UpdateUnit(Unit unit);
    Task DeleteUnit(int id);
}