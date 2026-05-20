using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace NutriProj.Migrations
{
    /// <inheritdoc />
    public partial class Update : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_RecipeIngredients_Ingredients_IngredientIdIng",
                table: "RecipeIngredients");

            migrationBuilder.DropForeignKey(
                name: "FK_RecipeIngredients_Recipes_RecipeIdRcp",
                table: "RecipeIngredients");

            migrationBuilder.DropForeignKey(
                name: "FK_RecipeIngredients_Units_UnitIdUnit",
                table: "RecipeIngredients");

            migrationBuilder.DropIndex(
                name: "IX_RecipeIngredients_IngredientIdIng",
                table: "RecipeIngredients");

            migrationBuilder.DropIndex(
                name: "IX_RecipeIngredients_RecipeIdRcp",
                table: "RecipeIngredients");

            migrationBuilder.DropIndex(
                name: "IX_RecipeIngredients_UnitIdUnit",
                table: "RecipeIngredients");

            migrationBuilder.DropColumn(
                name: "IngredientIdIng",
                table: "RecipeIngredients");

            migrationBuilder.DropColumn(
                name: "RecipeIdRcp",
                table: "RecipeIngredients");

            migrationBuilder.DropColumn(
                name: "UnitIdUnit",
                table: "RecipeIngredients");

            migrationBuilder.CreateIndex(
                name: "IX_RecipeIngredients_IdIng",
                table: "RecipeIngredients",
                column: "IdIng");

            migrationBuilder.CreateIndex(
                name: "IX_RecipeIngredients_IdRcp",
                table: "RecipeIngredients",
                column: "IdRcp");

            migrationBuilder.CreateIndex(
                name: "IX_RecipeIngredients_IdUnit",
                table: "RecipeIngredients",
                column: "IdUnit");

            migrationBuilder.AddForeignKey(
                name: "FK_RecipeIngredients_Ingredients_IdIng",
                table: "RecipeIngredients",
                column: "IdIng",
                principalTable: "Ingredients",
                principalColumn: "IdIng",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_RecipeIngredients_Recipes_IdRcp",
                table: "RecipeIngredients",
                column: "IdRcp",
                principalTable: "Recipes",
                principalColumn: "IdRcp",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_RecipeIngredients_Units_IdUnit",
                table: "RecipeIngredients",
                column: "IdUnit",
                principalTable: "Units",
                principalColumn: "IdUnit",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_RecipeIngredients_Ingredients_IdIng",
                table: "RecipeIngredients");

            migrationBuilder.DropForeignKey(
                name: "FK_RecipeIngredients_Recipes_IdRcp",
                table: "RecipeIngredients");

            migrationBuilder.DropForeignKey(
                name: "FK_RecipeIngredients_Units_IdUnit",
                table: "RecipeIngredients");

            migrationBuilder.DropIndex(
                name: "IX_RecipeIngredients_IdIng",
                table: "RecipeIngredients");

            migrationBuilder.DropIndex(
                name: "IX_RecipeIngredients_IdRcp",
                table: "RecipeIngredients");

            migrationBuilder.DropIndex(
                name: "IX_RecipeIngredients_IdUnit",
                table: "RecipeIngredients");

            migrationBuilder.AddColumn<int>(
                name: "IngredientIdIng",
                table: "RecipeIngredients",
                type: "INTEGER",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "RecipeIdRcp",
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

            migrationBuilder.CreateIndex(
                name: "IX_RecipeIngredients_IngredientIdIng",
                table: "RecipeIngredients",
                column: "IngredientIdIng");

            migrationBuilder.CreateIndex(
                name: "IX_RecipeIngredients_RecipeIdRcp",
                table: "RecipeIngredients",
                column: "RecipeIdRcp");

            migrationBuilder.CreateIndex(
                name: "IX_RecipeIngredients_UnitIdUnit",
                table: "RecipeIngredients",
                column: "UnitIdUnit");

            migrationBuilder.AddForeignKey(
                name: "FK_RecipeIngredients_Ingredients_IngredientIdIng",
                table: "RecipeIngredients",
                column: "IngredientIdIng",
                principalTable: "Ingredients",
                principalColumn: "IdIng",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_RecipeIngredients_Recipes_RecipeIdRcp",
                table: "RecipeIngredients",
                column: "RecipeIdRcp",
                principalTable: "Recipes",
                principalColumn: "IdRcp",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_RecipeIngredients_Units_UnitIdUnit",
                table: "RecipeIngredients",
                column: "UnitIdUnit",
                principalTable: "Units",
                principalColumn: "IdUnit",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
