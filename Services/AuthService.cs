using Microsoft.EntityFrameworkCore;
using NutriProj.Data;
using NutriProj.Enums;
using NutriProj.Models;
using NutriProj.Services_Interfaces;
using Microsoft.AspNetCore.Identity;

namespace NutriProj.Services;

public class AuthService : IAuthService
{
    private readonly PasswordHasher<User> _passwordHasher = new();
    private readonly AppDbContext _context;
    public AuthService(AppDbContext context)
    {
        _context = context;
    }

    public async Task<User> Login(string username, string password)
    {
        username = username.Trim().ToLower();
        var user = await _context.Users.FirstOrDefaultAsync(u => u.Username == username);
        var result = _passwordHasher.VerifyHashedPassword(user, user.PasswordHash, password);
        if (user == null)
            throw new Exception("Username not found");

        if (result == PasswordVerificationResult.Failed)
            throw new Exception("Incorrect password");
        return user;
    }

    public async Task Register(string username, string password, UserRole role)
    {
        username = username.Trim().ToLower();
        ValidateUsername(username);
        ValidatePassword(password);
        if (await UsernameExists(username))
            throw new Exception($"Username '{username}' already exists");
        _context.Users.Add(new User
        {
            Username = username,
            PasswordHash = _passwordHasher.HashPassword(null, password),
            Role = role
        });
        await _context.SaveChangesAsync();
    }

    public bool IsAdmin(User user)
    {
        return user.Role == UserRole.Admin;    
    }
         
    public async Task<bool> UsernameExists(string username)
    {
        return  await _context.Users.AnyAsync(u => u.Username == username);
    }
    private void ValidatePassword(string password){
    if (password.Length < 8)
        throw new Exception("Password must be at least 8 characters");

    if (!password.Any(char.IsUpper))
        throw new Exception("Password must contain an uppercase letter");

    if (!password.Any(char.IsDigit))
        throw new Exception("Password must contain a number");
    }
    private void ValidateUsername(string username){
    if (string.IsNullOrWhiteSpace(username))
        throw new Exception("Username is required");

    if (username.Length < 3)
        throw new Exception("Username must be at least 3 characters");

    if (username.Contains(" "))
        throw new Exception("Username cannot contain spaces");
    }
    
}