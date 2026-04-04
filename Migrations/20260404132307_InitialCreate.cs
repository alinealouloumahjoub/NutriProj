using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace NutriProj.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Ingredients",
                columns: table => new
                {
                    IdIng = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    NameIng = table.Column<string>(type: "TEXT", maxLength: 150, nullable: false),
                    CaloriesPer100g = table.Column<double>(type: "REAL", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Ingredients", x => x.IdIng);
                });

            migrationBuilder.CreateTable(
                name: "Recipes",
                columns: table => new
                {
                    IdRcp = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    RecipeName = table.Column<string>(type: "TEXT", maxLength: 200, nullable: false),
                    TypeKitchen = table.Column<int>(type: "INTEGER", nullable: false),
                    Category = table.Column<int>(type: "INTEGER", nullable: false),
                    TimePreparation = table.Column<int>(type: "INTEGER", nullable: false),
                    TimeCooking = table.Column<int>(type: "INTEGER", nullable: false),
                    TimeRest = table.Column<int>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Recipes", x => x.IdRcp);
                });

            migrationBuilder.CreateTable(
                name: "Users",
                columns: table => new
                {
                    IdUser = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Username = table.Column<string>(type: "TEXT", maxLength: 100, nullable: false),
                    PasswordHash = table.Column<string>(type: "TEXT", nullable: false),
                    Role = table.Column<int>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Users", x => x.IdUser);
                });

            migrationBuilder.CreateTable(
                name: "RecipeIngredients",
                columns: table => new
                {
                    IdRecIng = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    IdRcp = table.Column<int>(type: "INTEGER", nullable: false),
                    IdIng = table.Column<int>(type: "INTEGER", nullable: false),
                    Quantity = table.Column<double>(type: "REAL", nullable: false),
                    Unit = table.Column<string>(type: "TEXT", maxLength: 20, nullable: false),
                    RecipeIdRcp = table.Column<int>(type: "INTEGER", nullable: false),
                    IngredientIdIng = table.Column<int>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RecipeIngredients", x => x.IdRecIng);
                    table.ForeignKey(
                        name: "FK_RecipeIngredients_Ingredients_IngredientIdIng",
                        column: x => x.IngredientIdIng,
                        principalTable: "Ingredients",
                        principalColumn: "IdIng",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_RecipeIngredients_Recipes_RecipeIdRcp",
                        column: x => x.RecipeIdRcp,
                        principalTable: "Recipes",
                        principalColumn: "IdRcp",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "MealPlanners",
                columns: table => new
                {
                    IdPlanner = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    WeekStartDate = table.Column<DateOnly>(type: "TEXT", nullable: false),
                    DailyCaloriesTarget = table.Column<int>(type: "INTEGER", nullable: false),
                    IdUser = table.Column<int>(type: "INTEGER", nullable: false),
                    UserIdUser = table.Column<int>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MealPlanners", x => x.IdPlanner);
                    table.ForeignKey(
                        name: "FK_MealPlanners_Users_UserIdUser",
                        column: x => x.UserIdUser,
                        principalTable: "Users",
                        principalColumn: "IdUser",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "MealPlannerDetails",
                columns: table => new
                {
                    IdDetail = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    IdPlanner = table.Column<int>(type: "INTEGER", nullable: false),
                    IdRcp = table.Column<int>(type: "INTEGER", nullable: false),
                    Day = table.Column<int>(type: "INTEGER", nullable: false),
                    MealSlot = table.Column<int>(type: "INTEGER", nullable: false),
                    MealPlannerIdPlanner = table.Column<int>(type: "INTEGER", nullable: false),
                    RecipeIdRcp = table.Column<int>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MealPlannerDetails", x => x.IdDetail);
                    table.ForeignKey(
                        name: "FK_MealPlannerDetails_MealPlanners_MealPlannerIdPlanner",
                        column: x => x.MealPlannerIdPlanner,
                        principalTable: "MealPlanners",
                        principalColumn: "IdPlanner",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_MealPlannerDetails_Recipes_RecipeIdRcp",
                        column: x => x.RecipeIdRcp,
                        principalTable: "Recipes",
                        principalColumn: "IdRcp",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_MealPlannerDetails_MealPlannerIdPlanner",
                table: "MealPlannerDetails",
                column: "MealPlannerIdPlanner");

            migrationBuilder.CreateIndex(
                name: "IX_MealPlannerDetails_RecipeIdRcp",
                table: "MealPlannerDetails",
                column: "RecipeIdRcp");

            migrationBuilder.CreateIndex(
                name: "IX_MealPlanners_UserIdUser",
                table: "MealPlanners",
                column: "UserIdUser");

            migrationBuilder.CreateIndex(
                name: "IX_RecipeIngredients_IngredientIdIng",
                table: "RecipeIngredients",
                column: "IngredientIdIng");

            migrationBuilder.CreateIndex(
                name: "IX_RecipeIngredients_RecipeIdRcp",
                table: "RecipeIngredients",
                column: "RecipeIdRcp");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "MealPlannerDetails");

            migrationBuilder.DropTable(
                name: "RecipeIngredients");

            migrationBuilder.DropTable(
                name: "MealPlanners");

            migrationBuilder.DropTable(
                name: "Ingredients");

            migrationBuilder.DropTable(
                name: "Recipes");

            migrationBuilder.DropTable(
                name: "Users");
        }
    }
}
