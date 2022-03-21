using Microsoft.EntityFrameworkCore.Migrations;

namespace RecipeBox.Migrations
{
    public partial class fixTypo : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_CategoryRecipes_Categories_CategoryId",
                table: "CategoryRecipes");

            migrationBuilder.DropForeignKey(
                name: "FK_CategoryRecipes_Recipes_RecipeId",
                table: "CategoryRecipes");

            migrationBuilder.DropPrimaryKey(
                name: "PK_CategoryRecipes",
                table: "CategoryRecipes");

            migrationBuilder.DropColumn(
                name: "CategpryId",
                table: "CategoryRecipes");

            migrationBuilder.RenameTable(
                name: "CategoryRecipes",
                newName: "CategoryRecipe");

            migrationBuilder.RenameIndex(
                name: "IX_CategoryRecipes_RecipeId",
                table: "CategoryRecipe",
                newName: "IX_CategoryRecipe_RecipeId");

            migrationBuilder.RenameIndex(
                name: "IX_CategoryRecipes_CategoryId",
                table: "CategoryRecipe",
                newName: "IX_CategoryRecipe_CategoryId");

            migrationBuilder.AlterColumn<int>(
                name: "CategoryId",
                table: "CategoryRecipe",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AddPrimaryKey(
                name: "PK_CategoryRecipe",
                table: "CategoryRecipe",
                column: "CategoryRecipeId");

            migrationBuilder.AddForeignKey(
                name: "FK_CategoryRecipe_Categories_CategoryId",
                table: "CategoryRecipe",
                column: "CategoryId",
                principalTable: "Categories",
                principalColumn: "CategoryId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_CategoryRecipe_Recipes_RecipeId",
                table: "CategoryRecipe",
                column: "RecipeId",
                principalTable: "Recipes",
                principalColumn: "RecipeId",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_CategoryRecipe_Categories_CategoryId",
                table: "CategoryRecipe");

            migrationBuilder.DropForeignKey(
                name: "FK_CategoryRecipe_Recipes_RecipeId",
                table: "CategoryRecipe");

            migrationBuilder.DropPrimaryKey(
                name: "PK_CategoryRecipe",
                table: "CategoryRecipe");

            migrationBuilder.RenameTable(
                name: "CategoryRecipe",
                newName: "CategoryRecipes");

            migrationBuilder.RenameIndex(
                name: "IX_CategoryRecipe_RecipeId",
                table: "CategoryRecipes",
                newName: "IX_CategoryRecipes_RecipeId");

            migrationBuilder.RenameIndex(
                name: "IX_CategoryRecipe_CategoryId",
                table: "CategoryRecipes",
                newName: "IX_CategoryRecipes_CategoryId");

            migrationBuilder.AlterColumn<int>(
                name: "CategoryId",
                table: "CategoryRecipes",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AddColumn<int>(
                name: "CategpryId",
                table: "CategoryRecipes",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddPrimaryKey(
                name: "PK_CategoryRecipes",
                table: "CategoryRecipes",
                column: "CategoryRecipeId");

            migrationBuilder.AddForeignKey(
                name: "FK_CategoryRecipes_Categories_CategoryId",
                table: "CategoryRecipes",
                column: "CategoryId",
                principalTable: "Categories",
                principalColumn: "CategoryId",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_CategoryRecipes_Recipes_RecipeId",
                table: "CategoryRecipes",
                column: "RecipeId",
                principalTable: "Recipes",
                principalColumn: "RecipeId",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
