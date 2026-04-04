using NutriProj.Models;
using NutriProj.Enums;
namespace NutriProj.Services_Interfaces;
public interface IAuthService
{
    Task<User> Login(string username, string password);
    Task Register(string username, string password, UserRole role);
    bool IsAdmin(User user);
    Task<bool> UsernameExists(string username);
}