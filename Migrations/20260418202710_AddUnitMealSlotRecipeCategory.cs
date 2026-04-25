using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace NutriProj.Migrations
{
    /// <inheritdoc />
    public partial class AddUnitMealSlotRecipeCategory : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Category",
                table: "Recipes");

            migrationBuilder.DropColumn(
                name: "Unit",
                table: "RecipeIngredients");

            migrationBuilder.RenameColumn(
                name: "MealSlot",
                table: "MealPlannerDetails",
                newName: "MealSlotIdMealSlot");

            migrationBuilder.AddColumn<int>(
                name: "IdUnit",
                table: "RecipeIngredients",
                type: "INTEGER",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "UnitIdUnit",
                table: "RecipeIngredients",
                type: "INTEGER",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "IdMealSlot",
                table: "MealPlannerDetails",
                type: "INTEGER",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateTable(
                name: "MealSlots",
                columns: table => new
                {
                    IdMealSlot = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Name = table.Column<string>(type: "TEXT", maxLength: 20, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MealSlots", x => x.IdMealSlot);
                });

            migrationBuilder.CreateTable(
                name: "Units",
                columns: table => new
                {
                    IdUnit = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Name = table.Column<string>(type: "TEXT", maxLength: 30, nullable: false),
                    Type = table.Column<string>(type: "TEXT", maxLength: 20, nullable: false),
                    GramEquivalent = table.Column<double>(type: "REAL", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Units", x => x.IdUnit);
                });

            migrationBuilder.CreateTable(
                name: "RecipeCategories",
                columns: table => new
                {
                    IdCategory = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    IdRcp = table.Column<int>(type: "INTEGER", nullable: false),
                    IdMealSlot = table.Column<int>(type: "INTEGER", nullable: false),
                    RecipeIdRcp = table.Column<int>(type: "INTEGER", nullable: false),
                    MealSlotIdMealSlot = table.Column<int>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RecipeCategories", x => x.IdCategory);
                    table.ForeignKey(
                        name: "FK_RecipeCategories_MealSlots_MealSlotIdMealSlot",
                        column: x => x.MealSlotIdMealSlot,
                        principalTable: "MealSlots",
                        principalColumn: "IdMealSlot",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_RecipeCategories_Recipes_RecipeIdRcp",
                        column: x => x.RecipeIdRcp,
                        principalTable: "Recipes",
                        principalColumn: "IdRcp",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_RecipeIngredients_UnitIdUnit",
                table: "RecipeIngredients",
                column: "UnitIdUnit");

            migrationBuilder.CreateIndex(
                name: "IX_MealPlannerDetails_MealSlotIdMealSlot",
                table: "MealPlannerDetails",
                column: "MealSlotIdMealSlot");

            migrationBuilder.CreateIndex(
                name: "IX_RecipeCategories_MealSlotIdMealSlot",
                table: "RecipeCategories",
                column: "MealSlotIdMealSlot");

            migrationBuilder.CreateIndex(
                name: "IX_RecipeCategories_RecipeIdRcp",
                table: "RecipeCategories",
                column: "RecipeIdRcp");

            migrationBuilder.AddForeignKey(
                name: "FK_MealPlannerDetails_MealSlots_MealSlotIdMealSlot",
                table: "MealPlannerDetails",
                column: "MealSlotIdMealSlot",
                principalTable: "MealSlots",
                principalColumn: "IdMealSlot",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_RecipeIngredients_Units_UnitIdUnit",
                table: "RecipeIngredients",
                column: "UnitIdUnit",
                principalTable: "Units",
                principalColumn: "IdUnit",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_MealPlannerDetails_MealSlots_MealSlotIdMealSlot",
                table: "MealPlannerDetails");

            migrationBuilder.DropForeignKey(
                name: "FK_RecipeIngredients_Units_UnitIdUnit",
                table: "RecipeIngredients");

            migrationBuilder.DropTable(
                name: "RecipeCategories");

            migrationBuilder.DropTable(
                name: "Units");

            migrationBuilder.DropTable(
                name: "MealSlots");

            migrationBuilder.DropIndex(
                name: "IX_RecipeIngredients_UnitIdUnit",
                table: "RecipeIngredients");

            migrationBuilder.DropIndex(
                name: "IX_MealPlannerDetails_MealSlotIdMealSlot",
                table: "MealPlannerDetails");

            migrationBuilder.DropColumn(
                name: "IdUnit",
                table: "RecipeIngredients");

            migrationBuilder.DropColumn(
                name: "UnitIdUnit",
                table: "RecipeIngredients");

            migrationBuilder.DropColumn(
                name: "IdMealSlot",
                table: "MealPlannerDetails");

            migrationBuilder.RenameColumn(
                name: "MealSlotIdMealSlot",
                table: "MealPlannerDetails",
                newName: "MealSlot");

            migrationBuilder.AddColumn<int>(
                name: "Category",
                table: "Recipes",
                type: "INTEGER",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<string>(
                name: "Unit",
                table: "RecipeIngredients",
                type: "TEXT",
                maxLength: 20,
                nullable: false,
                defaultValue: "");
        }
    }
}
