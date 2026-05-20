using NutriProj.Components;
using NutriProj.Data;
using NutriProj.Models;
using NutriProj.Services;
using NutriProj.Services_Interfaces;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;
using Radzen;
using Microsoft.AspNetCore.Mvc;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddRazorComponents()
    .AddInteractiveServerComponents();

// Injection of the database context with SQLite
var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");
builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseSqlite(connectionString));

// Registering our services for dependency injection (DI)
builder.Services.AddScoped<IIngredientService, IngredientService>();
builder.Services.AddScoped<IRecipeService, RecipeService>();
builder.Services.AddScoped<IRecipeIngredientService, RecipeIngredientService>();
builder.Services.AddScoped<IMealPlannerService, MealPlannerService>();
builder.Services.AddScoped<IMealSlotService, MealSlotService>();
builder.Services.AddScoped<IUnitService, UnitService>();

// Radzen UI components
builder.Services.AddRadzenComponents();

// Identity — same pattern as professor
builder.Services.AddIdentity<IdentityUser, IdentityRole>(options =>
{
    options.Password.RequireDigit = false;
    options.Password.RequiredLength = 6;
    options.Password.RequireNonAlphanumeric = false;
    options.Password.RequireUppercase = false;
})
.AddEntityFrameworkStores<AppDbContext>()
.AddDefaultTokenProviders();

builder.Services.AddCascadingAuthenticationState();

var app = builder.Build();

// Seed initial data
using (var scope = app.Services.CreateScope())
{
    var context = scope.ServiceProvider.GetRequiredService<AppDbContext>();
    context.Database.EnsureCreated();

    if (!context.Units.Any())
    {
        context.Units.AddRange(
            new Unit { Name = "g",     Type = "weight", GramEquivalent = 1    },
            new Unit { Name = "kg",    Type = "weight", GramEquivalent = 1000 },
            new Unit { Name = "ml",    Type = "volume", GramEquivalent = 1    },
            new Unit { Name = "cup",   Type = "volume", GramEquivalent = 240  },
            new Unit { Name = "tbsp",  Type = "volume", GramEquivalent = 15   },
            new Unit { Name = "tsp",   Type = "volume", GramEquivalent = 5    },
            new Unit { Name = "piece", Type = "count",  GramEquivalent = 50   },
            new Unit { Name = "slice", Type = "count",  GramEquivalent = 30   }
        );
    }

    if (!context.MealSlots.Any())
    {
        context.MealSlots.AddRange(
            new MealSlot { Name = "Breakfast" },
            new MealSlot { Name = "Lunch"     },
            new MealSlot { Name = "Dinner"    },
            new MealSlot { Name = "Snack"     }
        );
    }

    context.SaveChanges();

    // Seed admin role and user (same as professor)
    var roleManager = scope.ServiceProvider.GetRequiredService<RoleManager<IdentityRole>>();
    var userManager = scope.ServiceProvider.GetRequiredService<UserManager<IdentityUser>>();

    if (!await roleManager.RoleExistsAsync("Admin"))
        await roleManager.CreateAsync(new IdentityRole("Admin"));

    if (await userManager.FindByEmailAsync("admin@nutri.com") == null)
    {
        var adminUser = new IdentityUser { UserName = "admin@nutri.com", Email = "admin@nutri.com" };
        var result = await userManager.CreateAsync(adminUser, "Admin123!");
        if (result.Succeeded)
            await userManager.AddToRoleAsync(adminUser, "Admin");
    }
}

if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error", createScopeForErrors: true);
    app.UseHsts();
}

app.UseStatusCodePagesWithReExecute("/not-found", createScopeForStatusCodePages: true);
app.UseHttpsRedirection();
app.UseAntiforgery();
app.MapStaticAssets();
app.MapRazorComponents<App>()
    .AddInteractiveServerRenderMode();

// Authentication endpoints (outside WebSocket, same as professor)
app.MapPost("/api/auth/login", async (
    [FromServices] SignInManager<IdentityUser> signInManager,
    [FromForm] string email,
    [FromForm] string password) =>
{
    var result = await signInManager.PasswordSignInAsync(email, password, isPersistent: false, lockoutOnFailure: false);
    if (result.Succeeded) return Results.Redirect("/ingredients");
    return Results.Redirect("/login?error=Invalid+credentials");
}).DisableAntiforgery();

app.MapPost("/api/auth/inscription", async (
    [FromServices] UserManager<IdentityUser> userManager,
    [FromForm] string UserName,
    [FromForm] string email,
    [FromForm] string password) =>
{
    var user = new IdentityUser { UserName = UserName, Email = email };
    var result = await userManager.CreateAsync(user, password);
    if (result.Succeeded)
        return Results.Redirect("/login?success=Account+created");
    return Results.Redirect("/inscription?error=Registration+failed");
}).DisableAntiforgery();

app.MapPost("/api/auth/logout", async ([FromServices] SignInManager<IdentityUser> signInManager) =>
{
    await signInManager.SignOutAsync();
    return Results.Redirect("/");
}).DisableAntiforgery();

app.Run(); 