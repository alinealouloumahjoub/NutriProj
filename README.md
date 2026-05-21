# GreenPlate 

A full-stack **Nutrition & Meal Planning** web application built with **Blazor Server** (.NET), **SQLite**, **Entity Framework Core**, **ASP.NET Identity**, and **Radzen UI** components.

GreenPlate lets users browse recipes, manage ingredients, scale servings, and plan weekly meals — all from a clean, responsive dashboard.



## Features

- **Authentication** — Register, login, and logout via ASP.NET Identity
- **Role-based access** — Admin dashboard vs. regular user views
- **Recipe management** — Create, edit, view, and delete recipes with images, prep/cook/rest times, and kitchen type (Tunisian, Italian, Asian, etc.)
- **Calorie calculation** — Automatic per-serving and total calorie computation from ingredients
- **Recipe Scaler** — Dynamically adjust ingredient quantities for any number of servings
- **Ingredient management** — CRUD for ingredients with calories per 100g and unit support
- **Meal Planner** — Weekly meal planner with daily calorie targets and meal slots (Breakfast, Lunch, Dinner, Snack)
- **Admin Dashboard** — KPI cards showing total recipes, highest/lowest/average calories, and charts by category

---

## Tech Stack

| Layer | Technology |
|---|---|
| Framework | ASP.NET Core 8 — Blazor Server |
| ORM | Entity Framework Core |
| Database | SQLite |
| Auth | ASP.NET Identity |
| UI Components | Radzen Blazor |
| Language | C# 12 |

---

## Project Structure

```
proj/
├── Components/
│   ├── Layout/
│   │   ├── MainLayout.razor        # Main shell with nav
│   │   ├── NavMenu.razor           # Sidebar navigation
│   │   └── EmptyLayout.razor       # Used for login/register pages
│   ├── Pages/
│   │   ├── Dashboard.razor         # Admin KPI dashboard
│   │   ├── Recipes.razor           # Recipe list
│   │   ├── RecipeDetail.razor      # Single recipe view
│   │   ├── EditRecipes.razor       # Create / edit recipe
│   │   ├── ScaleRecipe.razor       # Ingredient scaler tool
│   │   ├── Ingredients.razor       # Ingredient list
│   │   ├── EditIngredient.razor    # Create / edit ingredient
│   │   ├── MealPlannerPage.razor   # Weekly meal planner
│   │   ├── Login.razor             # Login page
│   │   ├── Inscription.razor       # Registration page
│   │   └── NotFound.razor          # 404 page
│   └── UI/
│       └── KpiCard.razor           # Reusable KPI card component
├── Data/
│   └── AppDbContext.cs             # EF Core DbContext + Identity
├── Enums/
│   └── Enums.cs                    # DayOfWeekEnum, KitchenType
├── Models/
│   ├── Ingredient.cs
│   ├── Recipe.cs
│   ├── RecipeIngredient.cs
│   ├── RecipeCategory.cs
│   ├── MealPlanner.cs
│   ├── MealPlannerDetail.cs
│   ├── MealSlot.cs
│   └── Unit.cs
├── Services/
│   ├── IngredientService.cs
│   ├── RecipeService.cs
│   ├── Recipeingredientservice.cs
│   ├── Mealplannerservice.cs
│   ├── Mealslotservice.cs
│   └── UnitService.cs
├── Services_Interfaces/
│   ├── IIngredientService.cs
│   ├── IRecipeService.cs
│   ├── IRecipeIngredientSvc.cs
│   ├── IMealPlannerService.cs
│   ├── IMealSlotService.cs
│   └── IUnitService.cs
└── Program.cs                      # App bootstrap, DI, seed data, auth endpoints
```

---

### Installation

```bash
# 1. Clone the repository
git clone https://github.com/alinealouloumahjoub/NutriProj.git
cd NutriProj

# 2. Restore dependencies
dotnet restore

# 3. Run the app (database is created automatically on first run)
dotnet run
```

The app will be available at `https://localhost:5254`

No migrations needed

---

## Default Credentials

| Role | Email | Password |
|---|---|---|
| Admin | `admin@nutri.com` | `Admin123!` |
| User | *(register via `/inscription`)* 

Admins are redirected to `/dashboard` after login. Regular users go to `/recipes`.

---

## Database & Models

The SQLite database is configured in `appsettings.json` under `ConnectionStrings:DefaultConnection`.

### Core Models

| Model | Description |
|---|---|
| `Recipe` | Name, kitchen type, description, image, servings, prep/cook/rest time, calorie helpers |
| `Ingredient` | Name, calories per 100g |
| `RecipeIngredient` | Junction: recipe ↔ ingredient with quantity and unit |
| `Unit` | Name (g, kg, ml, cup…), type, gram equivalent for calorie math |
| `MealPlanner` | Week start date, daily calorie target, user ID |
| `MealPlannerDetail` | Junction: planner ↔ recipe ↔ meal slot ↔ day of week |
| `MealSlot` | Breakfast, Lunch, Dinner, Snack |
| `RecipeCategory` | Links a recipe to a meal slot category |

### Enums

```csharp
// Day of week for meal planning
DayOfWeekEnum { Monday, Tuesday, Wednesday, Thursday, Friday, Saturday, Sunday }

// Cuisine style for recipes
KitchenType { Tunisian, Mediterranean, French, Italian, Asian, American, Other }
```

---

## Pages & Routes

| Route | Page | Access |
|---|---|---|
| `/login` | Login | Public |
| `/inscription` | Register | Public |
| `/dashboard` | Admin KPI Dashboard | Admin |
| `/recipes` | Recipe list | Authenticated |
| `/recipe/{id}` | Recipe detail | Authenticated |
| `/edit-recipe/{id}` | Edit recipe | Authenticated |
| `/scale-recipe/{id}` | Ingredient scaler | Authenticated |
| `/ingredients` | Ingredient list | Authenticated |
| `/edit-ingredient/{id}` | Edit ingredient | Authenticated |
| `/meal-planner` | Weekly meal planner | Authenticated |
| `/not-found` | 404 | Public |

---

## Authentication

Authentication is handled by **ASP.NET Identity** with three minimal API endpoints:

```
POST /api/auth/login        — Sign in and redirect by role
POST /api/auth/inscription  — Register a new user account
POST /api/auth/logout       — Sign out and redirect to /

Password policy:
- Minimum length: **6 characters**
- No digit, uppercase, or special character requirement

---


**Units:**

| Name | Type | Gram Equivalent |
|---|---|---|
| g | weight | 1 |
| kg | weight | 1000 |
| ml | volume | 1 |
| cup | volume | 240 |
| tbsp | volume | 15 |
| tsp | volume | 5 |
| piece | count | 50 |
| slice | count | 30 |

**Meal Slots:** Breakfast, Lunch, Dinner, Snack

**Admin user:** `admin@nutri.com` / `Admin123!` with the `Admin` role

---

## Creators
Aline Aloulou mahjoub: ingredients & recpies
Rahma Essaiem : Meal Planner & Dashboard 
Achraf Allegui : Project structure & Authentification
