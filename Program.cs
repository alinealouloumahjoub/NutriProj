using NutriProj.Components;
using Microsoft.EntityFrameworkCore;
using NutriProj.Data;
using NutriProj.Services;
using NutriProj.Services_Interfaces;
using NutriProj.Models;
var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddRazorComponents()
    .AddInteractiveServerComponents();
//Injecting the DbContext with SQLite connection string from appsettings.json
var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");
builder.Services.AddDbContext<AppDbContext>(options =>options.UseSqlite(connectionString));
// Registering our services and their interfaces for dependency injection(DI)
builder.Services.AddScoped<IIngredientService, IngredientService>();
builder.Services.AddScoped<IRecipeService, RecipeService>();
builder.Services.AddScoped<IRecipeIngredientService, RecipeIngredientService>();
var app = builder.Build();
using (var scope = app.Services.CreateScope())
{
    var context = scope.ServiceProvider.GetRequiredService<AppDbContext>();
    context.Database.EnsureCreated();

    // Seed Units (only if empty to avoid duplicates)
    if (!context.Units.Any())
    {
        var gram  = new Unit { Name = "g", Type = "weight", GramEquivalent = 1 };
        var ml = new Unit { Name = "ml", Type = "volume", GramEquivalent = 1 };
        var cup = new Unit { Name = "cup", Type = "volume", GramEquivalent = 240 };
        var tbsp= new Unit { Name = "tbsp", Type = "volume", GramEquivalent = 15 };
        var tsp = new Unit { Name = "tsp", Type = "volume", GramEquivalent = 5 };
        var piece= new Unit { Name = "piece", Type = "count", GramEquivalent = 50 };
        var slice = new Unit { Name = "slice", Type = "count", GramEquivalent = 30 };
        context.Units.AddRange(gram, ml, cup, tbsp, tsp, piece, slice);
    }

    // Seed Meal Slots
    if (!context.MealSlots.Any())
    {
        var breakfast = new MealSlot { Name = "Breakfast" };
        var lunch= new MealSlot { Name = "Lunch" };
        var dinner= new MealSlot { Name = "Dinner" };
        var snack  = new MealSlot { Name = "Snack" };
        context.MealSlots.AddRange(breakfast, lunch, dinner, snack);
    }

    context.SaveChanges();
}
// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error", createScopeForErrors: true);
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}
app.UseStatusCodePagesWithReExecute("/not-found", createScopeForStatusCodePages: true);
app.UseHttpsRedirection();

app.UseAntiforgery();

app.MapStaticAssets();
app.MapRazorComponents<App>()
    .AddInteractiveServerRenderMode();

app.Run();
