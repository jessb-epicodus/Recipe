using Microsoft.EntityFrameworkCore.Migrations;

namespace RecipeBox.Migrations
{
    public partial class moveAmount : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Ingredient_AspNetUsers_UserId",
                table: "Ingredient");

            migrationBuilder.DropForeignKey(
                name: "FK_IngredientRecipe_Ingredient_IngredientId",
                table: "IngredientRecipe");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Ingredient",
                table: "Ingredient");

            migrationBuilder.DropColumn(
                name: "Amount",
                table: "Ingredient");

            migrationBuilder.RenameTable(
                name: "Ingredient",
                newName: "Ingredients");

            migrationBuilder.RenameIndex(
                name: "IX_Ingredient_UserId",
                table: "Ingredients",
                newName: "IX_Ingredients_UserId");

            migrationBuilder.AddColumn<string>(
                name: "Amount",
                table: "IngredientRecipe",
                type: "longtext CHARACTER SET utf8mb4",
                nullable: true);

            migrationBuilder.AddPrimaryKey(
                name: "PK_Ingredients",
                table: "Ingredients",
                column: "IngredientId");

            migrationBuilder.AddForeignKey(
                name: "FK_IngredientRecipe_Ingredients_IngredientId",
                table: "IngredientRecipe",
                column: "IngredientId",
                principalTable: "Ingredients",
                principalColumn: "IngredientId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Ingredients_AspNetUsers_UserId",
                table: "Ingredients",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_IngredientRecipe_Ingredients_IngredientId",
                table: "IngredientRecipe");

            migrationBuilder.DropForeignKey(
                name: "FK_Ingredients_AspNetUsers_UserId",
                table: "Ingredients");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Ingredients",
                table: "Ingredients");

            migrationBuilder.DropColumn(
                name: "Amount",
                table: "IngredientRecipe");

            migrationBuilder.RenameTable(
                name: "Ingredients",
                newName: "Ingredient");

            migrationBuilder.RenameIndex(
                name: "IX_Ingredients_UserId",
                table: "Ingredient",
                newName: "IX_Ingredient_UserId");

            migrationBuilder.AddColumn<int>(
                name: "Amount",
                table: "Ingredient",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddPrimaryKey(
                name: "PK_Ingredient",
                table: "Ingredient",
                column: "IngredientId");

            migrationBuilder.AddForeignKey(
                name: "FK_Ingredient_AspNetUsers_UserId",
                table: "Ingredient",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_IngredientRecipe_Ingredient_IngredientId",
                table: "IngredientRecipe",
                column: "IngredientId",
                principalTable: "Ingredient",
                principalColumn: "IngredientId",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
