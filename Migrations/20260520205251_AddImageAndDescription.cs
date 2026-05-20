using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace NutriProj.Migrations
{
    /// <inheritdoc />
    public partial class AddImageAndDescription : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ImageUrl",
                table: "Ingredients");

            migrationBuilder.AddColumn<string>(
                name: "Description",
                table: "Recipes",
                type: "TEXT",
                maxLength: 1000,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "ImagePath",
                table: "Recipes",
                type: "TEXT",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "ImagePath",
                table: "Ingredients",
                type: "TEXT",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Description",
                table: "Recipes");

            migrationBuilder.DropColumn(
                name: "ImagePath",
                table: "Recipes");

            migrationBuilder.DropColumn(
                name: "ImagePath",
                table: "Ingredients");

            migrationBuilder.AddColumn<string>(
                name: "ImageUrl",
                table: "Ingredients",
                type: "TEXT",
                maxLength: 500,
                nullable: true);
        }
    }
}
