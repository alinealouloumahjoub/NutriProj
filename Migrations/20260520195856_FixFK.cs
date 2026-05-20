using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace NutriProj.Migrations
{
    /// <inheritdoc />
    public partial class FixFK : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_MealPlannerDetails_MealPlanners_MealPlannerIdPlanner",
                table: "MealPlannerDetails");

            migrationBuilder.DropForeignKey(
                name: "FK_MealPlannerDetails_MealSlots_MealSlotIdMealSlot",
                table: "MealPlannerDetails");

            migrationBuilder.DropForeignKey(
                name: "FK_MealPlannerDetails_Recipes_RecipeIdRcp",
                table: "MealPlannerDetails");

            migrationBuilder.DropForeignKey(
                name: "FK_RecipeCategories_MealSlots_MealSlotIdMealSlot",
                table: "RecipeCategories");

            migrationBuilder.DropForeignKey(
                name: "FK_RecipeCategories_Recipes_RecipeIdRcp",
                table: "RecipeCategories");

            migrationBuilder.DropIndex(
                name: "IX_RecipeCategories_MealSlotIdMealSlot",
                table: "RecipeCategories");

            migrationBuilder.DropIndex(
                name: "IX_RecipeCategories_RecipeIdRcp",
                table: "RecipeCategories");

            migrationBuilder.DropIndex(
                name: "IX_MealPlannerDetails_MealPlannerIdPlanner",
                table: "MealPlannerDetails");

            migrationBuilder.DropIndex(
                name: "IX_MealPlannerDetails_MealSlotIdMealSlot",
                table: "MealPlannerDetails");

            migrationBuilder.DropIndex(
                name: "IX_MealPlannerDetails_RecipeIdRcp",
                table: "MealPlannerDetails");

            migrationBuilder.DropColumn(
                name: "MealSlotIdMealSlot",
                table: "RecipeCategories");

            migrationBuilder.DropColumn(
                name: "RecipeIdRcp",
                table: "RecipeCategories");

            migrationBuilder.DropColumn(
                name: "MealPlannerIdPlanner",
                table: "MealPlannerDetails");

            migrationBuilder.DropColumn(
                name: "MealSlotIdMealSlot",
                table: "MealPlannerDetails");

            migrationBuilder.DropColumn(
                name: "RecipeIdRcp",
                table: "MealPlannerDetails");

            migrationBuilder.CreateIndex(
                name: "IX_RecipeCategories_IdMealSlot",
                table: "RecipeCategories",
                column: "IdMealSlot");

            migrationBuilder.CreateIndex(
                name: "IX_RecipeCategories_IdRcp",
                table: "RecipeCategories",
                column: "IdRcp");

            migrationBuilder.CreateIndex(
                name: "IX_MealPlannerDetails_IdMealSlot",
                table: "MealPlannerDetails",
                column: "IdMealSlot");

            migrationBuilder.CreateIndex(
                name: "IX_MealPlannerDetails_IdPlanner",
                table: "MealPlannerDetails",
                column: "IdPlanner");

            migrationBuilder.CreateIndex(
                name: "IX_MealPlannerDetails_IdRcp",
                table: "MealPlannerDetails",
                column: "IdRcp");

            migrationBuilder.AddForeignKey(
                name: "FK_MealPlannerDetails_MealPlanners_IdPlanner",
                table: "MealPlannerDetails",
                column: "IdPlanner",
                principalTable: "MealPlanners",
                principalColumn: "IdPlanner",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_MealPlannerDetails_MealSlots_IdMealSlot",
                table: "MealPlannerDetails",
                column: "IdMealSlot",
                principalTable: "MealSlots",
                principalColumn: "IdMealSlot",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_MealPlannerDetails_Recipes_IdRcp",
                table: "MealPlannerDetails",
                column: "IdRcp",
                principalTable: "Recipes",
                principalColumn: "IdRcp",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_RecipeCategories_MealSlots_IdMealSlot",
                table: "RecipeCategories",
                column: "IdMealSlot",
                principalTable: "MealSlots",
                principalColumn: "IdMealSlot",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_RecipeCategories_Recipes_IdRcp",
                table: "RecipeCategories",
                column: "IdRcp",
                principalTable: "Recipes",
                principalColumn: "IdRcp",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_MealPlannerDetails_MealPlanners_IdPlanner",
                table: "MealPlannerDetails");

            migrationBuilder.DropForeignKey(
                name: "FK_MealPlannerDetails_MealSlots_IdMealSlot",
                table: "MealPlannerDetails");

            migrationBuilder.DropForeignKey(
                name: "FK_MealPlannerDetails_Recipes_IdRcp",
                table: "MealPlannerDetails");

            migrationBuilder.DropForeignKey(
                name: "FK_RecipeCategories_MealSlots_IdMealSlot",
                table: "RecipeCategories");

            migrationBuilder.DropForeignKey(
                name: "FK_RecipeCategories_Recipes_IdRcp",
                table: "RecipeCategories");

            migrationBuilder.DropIndex(
                name: "IX_RecipeCategories_IdMealSlot",
                table: "RecipeCategories");

            migrationBuilder.DropIndex(
                name: "IX_RecipeCategories_IdRcp",
                table: "RecipeCategories");

            migrationBuilder.DropIndex(
                name: "IX_MealPlannerDetails_IdMealSlot",
                table: "MealPlannerDetails");

            migrationBuilder.DropIndex(
                name: "IX_MealPlannerDetails_IdPlanner",
                table: "MealPlannerDetails");

            migrationBuilder.DropIndex(
                name: "IX_MealPlannerDetails_IdRcp",
                table: "MealPlannerDetails");

            migrationBuilder.AddColumn<int>(
                name: "MealSlotIdMealSlot",
                table: "RecipeCategories",
                type: "INTEGER",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "RecipeIdRcp",
                table: "RecipeCategories",
                type: "INTEGER",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "MealPlannerIdPlanner",
                table: "MealPlannerDetails",
                type: "INTEGER",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "MealSlotIdMealSlot",
                table: "MealPlannerDetails",
                type: "INTEGER",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "RecipeIdRcp",
                table: "MealPlannerDetails",
                type: "INTEGER",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_RecipeCategories_MealSlotIdMealSlot",
                table: "RecipeCategories",
                column: "MealSlotIdMealSlot");

            migrationBuilder.CreateIndex(
                name: "IX_RecipeCategories_RecipeIdRcp",
                table: "RecipeCategories",
                column: "RecipeIdRcp");

            migrationBuilder.CreateIndex(
                name: "IX_MealPlannerDetails_MealPlannerIdPlanner",
                table: "MealPlannerDetails",
                column: "MealPlannerIdPlanner");

            migrationBuilder.CreateIndex(
                name: "IX_MealPlannerDetails_MealSlotIdMealSlot",
                table: "MealPlannerDetails",
                column: "MealSlotIdMealSlot");

            migrationBuilder.CreateIndex(
                name: "IX_MealPlannerDetails_RecipeIdRcp",
                table: "MealPlannerDetails",
                column: "RecipeIdRcp");

            migrationBuilder.AddForeignKey(
                name: "FK_MealPlannerDetails_MealPlanners_MealPlannerIdPlanner",
                table: "MealPlannerDetails",
                column: "MealPlannerIdPlanner",
                principalTable: "MealPlanners",
                principalColumn: "IdPlanner",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_MealPlannerDetails_MealSlots_MealSlotIdMealSlot",
                table: "MealPlannerDetails",
                column: "MealSlotIdMealSlot",
                principalTable: "MealSlots",
                principalColumn: "IdMealSlot",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_MealPlannerDetails_Recipes_RecipeIdRcp",
                table: "MealPlannerDetails",
                column: "RecipeIdRcp",
                principalTable: "Recipes",
                principalColumn: "IdRcp",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_RecipeCategories_MealSlots_MealSlotIdMealSlot",
                table: "RecipeCategories",
                column: "MealSlotIdMealSlot",
                principalTable: "MealSlots",
                principalColumn: "IdMealSlot",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_RecipeCategories_Recipes_RecipeIdRcp",
                table: "RecipeCategories",
                column: "RecipeIdRcp",
                principalTable: "Recipes",
                principalColumn: "IdRcp",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
